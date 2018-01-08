using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ideal.Data.Models
{
    using static DataConstants;

    public class Idea
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(IdeaNameMaxLength)]
        public string Name { get; set; }

        [Required]
        [MaxLength(IdeaDescriptionMaxLength)]
        public string Description { get; set; }

        [DefaultValue(false)]
        public bool IsApproved { get; set; }

        public string IdeaOwnerId { get; set; }

        public User IdeaOwner { get; set; }

        public int TeamId { get; set; }

        public Team Team { get; set; }

    }
}

