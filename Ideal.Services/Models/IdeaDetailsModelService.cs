using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using Ideal.Common;
using Ideal.Data;
using Ideal.Data.Models;

namespace Ideal.Services.Models
{
    public class IdeaDetailsModelService : IMapFrom<Idea>,IHaveCustomMapping
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string IdeaOwner { get; set; }

        public string TeamName { get; set; }

        public int Teamsize { get; set; }

        public List<string> Participants { get; set; } = new List<string>();

        public bool SignUp { get; set; }


        public void ConfigureMapping(Profile mapper)
        {

            mapper
                .CreateMap<Idea, IdeaDetailsModelService>()
                .ForMember(id => id.IdeaOwner, cfg => cfg.MapFrom(i => i.IdeaOwner.Name))
                .ForMember(id => id.TeamName, cfg => cfg.MapFrom(i => i.Team.Name))
                .ForMember(id => id.Teamsize, cfg => cfg.MapFrom(i => i.Team.Teamsize))
                .ForMember(id => id.Participants,
                    cfg => cfg.MapFrom(i => i.Team.Participants.Where(tu => tu.TeamId == i.Team.Id).Select(t=>t.Participant.Name).ToList()));



        }
    }
}
