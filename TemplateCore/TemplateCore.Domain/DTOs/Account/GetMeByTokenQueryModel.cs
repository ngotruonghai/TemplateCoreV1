﻿namespace TemplateCore.Domain.DTOs.Account
{
    public class GetMeByTokenQueryModel
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public string Fullname { get; set; }
        public string[] Roles { get; set; }
        public string Uid { get; set; }
        public string AvatarUrl { get; set; }
        public string AvatarUid { get; set; }
    }
}
