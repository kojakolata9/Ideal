using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Ideal.Common;
using Ideal.Data.Models;

namespace Ideal.Web.Areas.User.Models
{
    public class MessageViewModel : IMapFrom<Message>
    {
        public MessageViewModel() { }
        public MessageViewModel(Message message)
        {
            Content = message.Content;
            Author = message.User.UserName;
            Timestamp = message.DateCreated;
            TeamName = message.Team.Name;
        }
        [Required]
        public string Content { get; set; }

        [Required]
        public string Author { get; set; }

        [Required]
        public string TeamName { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime Timestamp { get; set; }

        public void ConfigureMapping(Profile mapper)
        {
            throw new NotImplementedException();
        }
    }
}
