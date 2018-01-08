using System;
using System.Collections.Generic;
using System.Text;
using Ideal.Common;
using Ideal.Data.Models;

namespace Ideal.Services.Models
{
    public class TeamListingModelService : IMapFrom<Team>
    {
        public int Id { get; set; }

        public string Name { get; set; }

    }
}
