using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ideal.Services;
using Ideal.Web.Areas.User.Models;
using Ideal.Web.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace Ideal.Web.Areas.Admin.Controllers
{
    public class AdminController : BaseAdminController
    {
        private readonly IIdeaService ideas;
        private readonly IAdminService admin;
        private const int IdeaPageSize =12;

        public AdminController(IIdeaService ideas, IAdminService admin)
        {
            this.ideas = ideas;
            this.admin = admin;
        }

        public async Task<IActionResult> Index(int page=1)
        {
            
            return View(new IdeaListingViewModel()
            {
                Ideas = await this.ideas.ActiveAsyncNotApproved(page, IdeaPageSize),
                CurrentPage = page,
                TotalPages = (int)Math.Ceiling(this.ideas.TotalNotApproved() / (double)IdeaPageSize)
            });
        }

        public async Task<IActionResult> Approve(int id)
        {
            this.admin.Approve(id);

            return RedirectToAction(
                nameof(AdminController.Index),
                "Admin",
                new { area = "Admin" });
        }
    }
}