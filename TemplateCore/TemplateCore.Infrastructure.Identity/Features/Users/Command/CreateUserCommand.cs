using MediatR;

namespace TemplateCore.Infrastructure.Identity.Features.Users.Command
{
    public class CreateUserCommand: IRequest<Response<int>>
    {
        public string RoleId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
        public string MaPhongBan { get; set; }
        public string ParentUserId { get; set; }

        public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Response<int>>
        {
            private readonly RoleManager<IdentityRole> _roleManager;
            private readonly UserManager<ApplicationUser> _userManager;
            public CreateUserCommandHandler(
                RoleManager<IdentityRole> roleManager,
                UserManager<ApplicationUser> userManager)
            {
                _roleManager = roleManager;
                _userManager = userManager;
            }
            public async Task<Response<int>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    CheckValid();
                    var listUser = await _userManager.Users.Where(x => x.MaPhongBan == request.MaPhongBan && x.LockoutEnabled == true).ToListAsync();
                    var userDangKy = listUser.Where(x => (x.TypeUser == TypeAccount.UserDangKy || x.TypeUser == TypeAccount.UserHeThong)  
                                                    && x.Id == request.ParentUserId
                                                    ).FirstOrDefault();

                    if(listUser.Any() == false) throw new Exception("Không tồn tại mã phòng ban này");
                    if (userDangKy == null) throw new Exception("Không tồn tại user đăng ký nên không tạo tài khoản được");

                    ApplicationUser applicationUser = new ApplicationUser()
                    {
                        FirstName = request.FirstName,
                        LastName = request.LastName,
                        LockoutEnabled = true,
                        Email = request.Email,
                        TypeUser = TypeAccount.UserTao,
                        ParentUserId = request.ParentUserId,
                        EmailConfirmed = true,
                        MaNhanVien = (listUser.Count() + 1).ToString("D5"),
                        MaPhongBan = request.MaPhongBan,
                        UserName = request.UserName,
                        PhoneNumber = request.PhoneNumber,
                    };

                    var result = await _userManager.CreateAsync(applicationUser, "123Pa$$word!");
                    if (result.Succeeded)
                    {
                        return new Response<int>(1);
                    }
                    else
                        throw new Exception("Lỗi tạo user");


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
