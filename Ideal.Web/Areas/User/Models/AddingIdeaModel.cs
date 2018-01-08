using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Ideal.Data;

namespace Ideal.Web.Areas.User.Models
{
    using static DataConstants;
    public class AddingIdeaModel 
    {
        [Required]
        [MaxLength(IdeaNameMaxLength)]
        public string Name { get; set; }

        [Required]
        [MaxLength(IdeaDescriptionMaxLength)]
        public string Description { get; set; }

        [Required]
        [MaxLength(TeamNameMaxLength)]
        public string TeamName { get; set; }

        public int Teamsize { get; set; }
    }
}

