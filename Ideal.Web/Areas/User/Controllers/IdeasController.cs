using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ideal.Services;
using Ideal.Services.Implementations;
using Ideal.Services.Models;
using Ideal.Web.Areas.User.Models;
using Ideal.Web.Controllers;
using Ideal.Web.Infrastructure.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;


namespace Ideal.Web.Areas.User.Controllers
{
    [Area("User")]
    [Authorize]
    public class IdeasController : Controller
    {
        private readonly UserManager<Ideal.Data.Models.User> _userManager;
        private readonly IIdeaService ideas;
        private readonly ITeamService teams;
        private readonly IUserService users;
        private const int IdeaPageSize = 8;

        public IdeasController(UserManager<Ideal.Data.Models.User> userManager,IIdeaService ideas,
            ITeamService teams, IUserService users)
        {
            this._userManager = userManager;
            this.ideas = ideas;
            this.teams = teams;
            this.users = users;
        }

        public async Task<IActionResult> Index(int page=1)
        {
            return View(new IdeaListingViewModel()
            {
                Ideas = await this.ideas.ActiveAsyncApproved(page,IdeaPageSize),
                CurrentPage = page,
                TotalPages = (int)Math.Ceiling(this.ideas.TotalApproved()/(double)IdeaPageSize)
            });
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(AddingIdeaModel model)
        {
            if (!ModelState.IsValid)
            {                
                return View(model);
            }
            try
            {
                await this.teams.CreateAsync(
                    model.TeamName,
                    model.Teamsize,
                    _userManager.GetUserId(HttpContext.User)
                );

                await this.ideas.CreateAsync(
                    model.Name,
                    model.Description,
                    model.TeamName,
                    model.Teamsize,
                    _userManager.GetUserId(HttpContext.User)
                );


                TempData.AddSuccessMessage($"Idea {model.Name} and Team {model.TeamName} created successfully!");

                return RedirectToAction(
                    nameof(HomeController.Index),
                    "Home",
                    new { area = string.Empty });
            }
            catch (Exception e)
            {
                TempData.AddErrorMessage($"Team Name or Idea Name already exist");
                return View(model);
            }
            
        }

        public async Task<IActionResult> Details(int id)
        {
            var idea = new IdeaDetailsViewModel()
            {
                Idea = await this.ideas.ByIdAsync(id)
            };
            if (idea.Idea.Participants.Any(p => p == _userManager.GetUserName(HttpContext.User)) ||
                idea.Idea.Participants.Count == idea.Idea.Teamsize)
            {
                idea.Idea.SignUp = false;
            }
            else
            {
                idea.Idea.SignUp = true;

            }

            return View(idea);
        }

        public async Task<IActionResult> SignUp(string name)
        {
            await this.users.CreateAsync(
                name,
                _userManager.GetUserId(HttpContext.User)
            );

            
            TempData.AddSuccessMessage($"SignUp was  successfully!");

            return RedirectToAction(
                nameof(HomeController.Index),
                "Home",
                new { area = string.Empty });
        }
    }
}