using System;
using System.Collections.Generic;
using System.Text;
using Ideal.Common;
using Ideal.Data.Models;

namespace Ideal.Services.Models
{
    public class IdeaListingModelService : IMapFrom<Idea>
    {
        public int Id { get; set; }

        public string Name { get; set; }

    }
}
