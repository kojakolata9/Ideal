using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Ideal.Data.Models
{
    public class TeamUser
    {
        
        public string ParticipantId { get; set; }

        public User Participant { get; set; }

        
        public int TeamId { get; set; }

        public Team Team { get; set; }

        
    }
}
