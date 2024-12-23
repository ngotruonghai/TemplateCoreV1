using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using TemplateCore.Domain.DTOs.Account;

namespace TemplateCore.Infrastructure.Identity.Models
{
    public class ApplicationUser: IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [NotMapped]
        public string MaNhanVien {  get; set; }
    }
}
