using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Conventions.Internal;


namespace Ideal.Data.Models
{
    public class Message
    {
        [Key]
        public int MessageId { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        [ForeignKeyAttribute("User")]
        public string UserId { get; set; }

        public User User { get; set; }

        [Required]
        [ForeignKeyAttribute("Team")]
        public int TeamId { get; set; }

        public Team Team { get; set; }

        [Required]
        public DateTime DateCreated { get; set; }
    }
}
