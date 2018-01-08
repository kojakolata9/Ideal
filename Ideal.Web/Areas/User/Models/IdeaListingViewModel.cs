using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ideal.Services.Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Ideal.Web.Areas.User.Models
{
    public class IdeaListingViewModel
    {
        public IEnumerable<IdeaListingModelService> Ideas { get; set; }

        public  int CurrentPage { get; set; }

        public int TotalPages { get; set; }

        public int PreviousPage => this.CurrentPage == 1 ? 1 : this.CurrentPage - 1;

        public int NextPage => this.CurrentPage == this.TotalPages ? this.TotalPages : this.CurrentPage + 1;
    }
}
