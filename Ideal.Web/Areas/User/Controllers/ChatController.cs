using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ideal.Data;
using Ideal.Data.Models;
using Ideal.Services;
using Ideal.Web.Areas.User.Models;
using Ideal.Web.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace Ideal.Web.Areas.User.Controllers
{
    [Area("User")]
    [Microsoft.AspNetCore.Authorization.Authorize]
    public class ChatController : Controller
    {
        private readonly ITeamService teams;
        private readonly UserManager<Ideal.Data.Models.User> _userManager;
        private IdealDbContext _context;

        public ChatController(ITeamService teams, UserManager<Ideal.Data.Models.User> userManager, IdealDbContext ctx)

        {
            this._userManager = userManager;
            this.teams = teams;
            this._context = ctx;
        }


        public async Task<IActionResult> Index(string name)
        {
            if (name == null)
            {
                var userTeams = await this.teams.ActiveAsyncUserTeams(this._userManager.GetUserId(HttpContext.User));
                Message[] messages = await _context.Messages.Where(m => m.Team.Name == userTeams.First().Name)
                    .Include(m => m.User).ToArrayAsync();
                List<MessageViewModel> model = new List<MessageViewModel>();
                foreach (Message msg in messages)
                {
                    model.Add(new MessageViewModel(msg));
                }
                return View(new TeamViewModel()
                {
                    Teams = userTeams,
                    TeamName = userTeams.First().Name.ToString(),
                    Messages = model
                });
            }
            else
            {
                var userTeams = await this.teams.ActiveAsyncUserTeams(this._userManager.GetUserId(HttpContext.User));
                Message[] messages = await _context.Messages.Where(m => m.Team.Name == name).Include(m => m.User)
                    .ToArrayAsync();
                List<MessageViewModel> model = new List<MessageViewModel>();
                foreach (Message msg in messages)
                {
                    model.Add(new MessageViewModel(msg));
                }
                return View(new TeamViewModel()
                {
                    Teams = userTeams,
                    TeamName = name,
                    Messages = model
                });
            }

        }

        private Task<Ideal.Data.Models.User> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

    [HttpPost]
    public async Task<IActionResult> Index(string content, string teamName)
    {
        try
        {

            // Get the current user
            var user = await GetCurrentUserAsync();
            if (user == null) return Forbid();
            var team = this._context.Teams.FirstOrDefault(t => t.Name == teamName);

            // Create a new message to save to the database
            Message newMessage = new Message()
            {
                Content = content,
                UserId = user.Id,
                User = user,
                DateCreated = DateTime.Now,
                Team = team,
                TeamId = team.Id
            };
            await _context.AddAsync(newMessage);
            await _context.SaveChangesAsync();

            Message[] messages = await _context.Messages.Where(m => m.Team.Name == teamName)
                .Include(m => m.User)
                .ToArrayAsync();
            List<MessageViewModel> model = new List<MessageViewModel>();
            foreach (Message msg in messages)
            {
                model.Add(new MessageViewModel(msg));
            }
            return View(new TeamViewModel()
            {
                Teams =
                    await this.teams.ActiveAsyncUserTeams(this._userManager.GetUserId(HttpContext.User)),
                TeamName = teamName,
                Messages = model
            });
        }
        catch (Exception e)
        {
            Message[] messages = await _context.Messages.Where(m => m.Team.Name == teamName)
                .Include(m => m.User)
                .ToArrayAsync();
            List<MessageViewModel> model = new List<MessageViewModel>();
            foreach (Message msg in messages)
            {
                model.Add(new MessageViewModel(msg));
            }
            return View(new TeamViewModel()
            {
                Teams =
                    await this.teams.ActiveAsyncUserTeams(this._userManager.GetUserId(HttpContext.User)),
                TeamName = teamName,
                Messages = model
            });
        }
     }
   }
}
    
