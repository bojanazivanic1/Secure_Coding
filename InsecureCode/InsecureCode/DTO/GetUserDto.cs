﻿using InsecureCode.Models;
using System.ComponentModel.DataAnnotations;

namespace InsecureCode.DTO
{
    public class GetUserDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public DateTime? VerifiedAt { get; set; } = null;
        public EUserRole? UserRole { get; set; }
        public DateTime? ModeratorVerifiedAt { get; set; } = null;
        public List<Post>? Posts { get; set; }
    }
}
