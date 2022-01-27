using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AgencyBackend.Entities
{
    public class Portfolio
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="Bu sahe bos ola bilmez")]
        public string Image { get; set; }

        [Required(ErrorMessage = "Bu sahe bos ola bilmez"),MaxLength(50, ErrorMessage = "Maksimum uzunluq 50 simvol ola bilər!")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Bu sahe bos ola bilmez"), MaxLength(200, ErrorMessage = "Maksimum uzunluq 50 simvol ola bilər!")]
        public string Info { get; set; }

        [NotMapped,Required]
        public IFormFile Photo { get; set; }

        public bool IsDeleted { get; set; }
        public DateTime CreatedAt { get; set; }

    }
}
