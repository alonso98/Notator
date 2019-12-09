using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using UpdateNotator.Application.ApplicationServices.User;
using UpdateNotator;
using UpdateNotator.Application.ApplicationServices.Topic;
using UpdateNotator.Application.ApplicationServices.Entry;

namespace UpdateNotator.Application.ApplicationServices
{
    public class Map : Profile
    {
        public Map()
        {
            CreateMap<Domain.Core.Users.User, UserDto>();
            CreateMap<Domain.Core.Topics.Topic, TopicDto>();
            CreateMap<Domain.Core.Entries.Entry, EntryDto>();
            CreateMap<Domain.Core.Entries.Link, LinkDto>();
        }
    }
}
