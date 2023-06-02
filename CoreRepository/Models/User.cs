using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace RepositoryCore.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
        [Required]
        public string? FullName { get; set; }
        [Required]
        public string? Role { get; set; }
        [Required]
        public bool Active { get; set; }
    }

    public class UserLogin
    {
        [Required]
        public string username { get; set; }
        [Required]
        public string password { get; set; }
    }
}
