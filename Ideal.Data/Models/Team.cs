using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ideal.Data.Models
{
    using static DataConstants;

    public class Team
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(TeamNameMaxLength)]
        public string Name { get; set; }

        public int Teamsize { get; set; }

        public Idea Idea { get; set; }

        public List<TeamUser> Participants { get; set; } = new List<TeamUser>();
    }
}
