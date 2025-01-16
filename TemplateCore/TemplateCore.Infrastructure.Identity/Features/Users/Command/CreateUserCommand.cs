using MediatR;
using TemplateCore.Application.Interfaces;
using IAuthenticatedUserService = TemplateCore.Application.Interfaces.IAuthenticatedUserService;

namespace TemplateCore.Infrastructure.Identity.Features.Users.Command
{
    public class CreateUserCommand : IRequest<Response<int>>
    {
        public string? RoleId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? UserName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? MaPhongBan { get; set; }
        public string? ParentUserId { get; set; }
        public int PhongBanId {  get; set; }

        public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Response<int>>
        {
            private readonly RoleManager<IdentityRole> _roleManager;
            private readonly UserManager<ApplicationUser> _userManager;
            private readonly IAuthenticatedUserService _authenticatedUserService;
            private readonly IPhongBanRepositoryAsync _phongBanRepositoryAsync;

            public CreateUserCommandHandler(
                RoleManager<IdentityRole> roleManager,
                UserManager<ApplicationUser> userManager,
                IAuthenticatedUserService authenticatedUserService,
                IPhongBanRepositoryAsync phongBanRepositoryAsync)
            {
                _roleManager = roleManager;
                _userManager = userManager;
                _authenticatedUserService = authenticatedUserService;
                _phongBanRepositoryAsync = phongBanRepositoryAsync;
            }
            public async Task<Response<int>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    request.ParentUserId = _authenticatedUserService.parentUserId == "" ? _authenticatedUserService.UserId : _authenticatedUserService.parentUserId;
                    string maphongbanUser = _authenticatedUserService.maphongban;
                    
                    CheckValid();

                    /* Kiểm tra người tạo là ai SupperAdmin không tạo được  */
                    var listUser = await _userManager.Users.Where(x => x.ParentUserId == request.ParentUserId && x.LockoutEnabled == true).ToListAsync();
                    if (_authenticatedUserService.CheckUserSupperAdmin == false)
                    {
                        var userDangKy = listUser.Where(x => (x.TypeUser == TypeAccount.UserDangKy || x.TypeUser == TypeAccount.UserHeThong)
                                                 && x.Id == request.ParentUserId 
                                                 ).FirstOrDefault();
                        if (userDangKy == null) throw new Exception("Không tồn tại user đăng ký nên không tạo tài khoản được");
                        if (_authenticatedUserService.permission.ToLower() != "ADMIN") throw new Exception("Không có quyền tạo tài khoản");
                    }

                    var phongban = await _phongBanRepositoryAsync.GetPhongBanId(request.PhongBanId);

                    if (listUser.Any() == false) throw new Exception("Không tồn tại mã phòng ban này");
                    if(phongban == null) throw new Exception("Không tồn tại phòng ban này");
                    request.MaPhongBan = phongban.MaPhongBan;

                    ApplicationUser applicationUser = new ApplicationUser()
                    {
                        FirstName = request.FirstName,
                        LastName = request.LastName,
                        LockoutEnabled = true,
                        Email = request.Email,
                        TypeUser = TypeAccount.UserTao,
                        ParentUserId = request.ParentUserId,
                        EmailConfirmed = true,
                        MaNhanVien = "EMP" + (listUser.Count() + 1).ToString("D5"),
                        MaPhongBan = request.MaPhongBan,
                        UserName = request.UserName,
                        PhoneNumber = request.PhoneNumber,
                        PhongBanId = request.PhongBanId,
                    };

                    var result = await _userManager.CreateAsync(applicationUser, "123Pa$$word!");
                    if (result.Succeeded)
                    {
                        var role = await _roleManager.FindByIdAsync(request.RoleId);
                        await _userManager.AddToRoleAsync(applicationUser, role.Name);
                        return new Response<int>(1);
                    }
                    else
                        throw new Exception("Tài khoản đã tồn tại vui lòng đổi lại tài khoản khác");


                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }

                #region Local function

                void CheckValid()
                {
                    if (string.IsNullOrEmpty(request.UserName)) throw new Exception("Vui lòng nhập UserName để đăng nhập");
                    if (string.IsNullOrEmpty(request.FirstName)) throw new Exception("Vui lòng nhập FirstName");
                    if (string.IsNullOrEmpty(request.LastName)) throw new Exception("Vui lòng nhập LastName");
                    if (string.IsNullOrEmpty(request.ParentUserId)) throw new Exception("Vui lòng nhập ParentUserId");
                }

                #endregion
            }
        }
    }
}
