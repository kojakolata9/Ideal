using System;
using System.Collections.Generic;
using System.Text;

namespace Ideal.Data.Models
{
    using Microsoft.AspNetCore.Identity;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static DataConstants;

    public class User : IdentityUser
    {
        [Required]
        [MinLength(UserNameMinLength)]
        [MaxLength(UserNameMaxLength)]
        public string Name { get; set; }

        public DateTime Birthdate { get; set; }

        public List<TeamUser> Teams { get; set; } = new List<TeamUser>();

        public List<Idea> Ideas { get; set; } = new List<Idea>();
    }
}
