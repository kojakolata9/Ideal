using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Ideal.Web.Areas.User.Models
{
    public class NewMessageViewModel
    {
        [Required]
        public string Content { get; set; }

        [Required]
        public string TeamName { get; set; }
    }
}
