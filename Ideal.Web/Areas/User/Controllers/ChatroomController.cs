using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ideal.Data;
using Ideal.Data.Models;
using Ideal.Web.Areas.User.Models;
using Ideal.Web.Controllers;
using Ideal.Web.Hubs;
using Ideal.Web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Ideal.Web.Areas.User.Controllers
{
    [Produces("application/json")]
    public class ChatroomController : ApiHubController<Broadcaster>
    {
        private readonly UserManager<Ideal.Data.Models.User> _userManager;
        private IdealDbContext _context;

        public ChatroomController(UserManager<Ideal.Data.Models.User> userManager, IdealDbContext ctx, IConnectionManager connectionManager)
        : base(connectionManager)
        {
            _userManager = userManager;
            _context = ctx;
        }

        private Task<Ideal.Data.Models.User> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        [HttpGet]
        [Route("[controller]")]
        public async Task<IActionResult> Get(string teamname)
        {
            Message[] messages = await _context.Messages.Where(m=>m.Team.Name==teamname).Include(m => m.User).ToArrayAsync();
            List<MessageViewModel> model = new List<MessageViewModel>();
            foreach (Message msg in messages)
            {
                model.Add(new MessageViewModel(msg));
            }
            return Json(model);
        }

        //[HttpPost]
        //[Route("[controller]")]
        //public async Task<IActionResult> Post([FromBody]NewMessageViewModel message)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        // Get the current user
        //        var user = await GetCurrentUserAsync();
        //        if (user == null) return Forbid();
        //        var team = await this._context.Teams.FirstOrDefaultAsync(t => t.Name == message.TeamName);

        //        // Create a new message to save to the database
        //        Message newMessage = new Message()
        //        {
        //            Content = message.Content,
        //            UserId = user.Id,
        //            User = user,
        //            DateCreated = DateTime.Now,
        //            Team = team,
        //            TeamId = team.Id
        //        };

        //        // Save the new message
        //        await _context.AddAsync(newMessage);
        //        await _context.SaveChangesAsync();

        //        MessageViewModel model = new MessageViewModel(newMessage);

        //        // Call the client method 'addChatMessage' on all clients in the
        //        // "MainChatroom" group.
        //        this.Clients.Group(team.Name).AddChatMessage(model);
        //        return new NoContentResult();
        //    }
        //    return BadRequest(ModelState);
        //}
    }
}