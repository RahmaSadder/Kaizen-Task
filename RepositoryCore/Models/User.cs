using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace CoreRepository.Models
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

    public class UserPass
    {
        public Guid Id { get; set; }
        public string? Password { get; set; }
    }
    public class UserLogin
    {
        [Required]
        public string username { get; set; }
        [Required]
        public string password { get; set; }
    }

    public class UserDTO
    {
        public int count { set; get; }
        public List<User>? users { set; get; }
    }
}
