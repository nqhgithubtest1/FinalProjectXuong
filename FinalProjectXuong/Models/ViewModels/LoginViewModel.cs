﻿using System.ComponentModel.DataAnnotations;

namespace FinalProjectXuong.Models.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        [StringLength(50)]
        public string Username { get; set; }

        [Required]
        [StringLength(50)]
        public string Password { get; set; }
    }
}
