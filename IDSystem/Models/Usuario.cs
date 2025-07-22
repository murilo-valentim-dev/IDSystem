using System;
using System.ComponentModel.DataAnnotations;

namespace IDSystem.Models
{
    public class Usuario
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Login { get; set; }

        [Required]
        public string SenhaHash { get; set; }

        public bool IsMaster { get; set; } = false;

        public DateTime DataCriacao { get; set; } = DateTime.Now;
    }
}
