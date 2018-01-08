using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ideal.Services.Models;

namespace Ideal.Web.Areas.User.Models
{
    public class TeamViewModel
    {
        public IEnumerable<TeamListingModelService > Teams { get; set; }

        public string TeamName { get; set; }

        public NewMessageViewModel NewMessage { get; set; }

        public IEnumerable<MessageViewModel> Messages { get; set; }

    }
}
