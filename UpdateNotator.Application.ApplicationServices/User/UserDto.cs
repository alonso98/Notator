using System;
using System.Collections.Generic;
using System.Text;
using UpdateNotator.Application.ApplicationServices.Topic;
using UpdateNotator.Domain.Core.Users;

namespace UpdateNotator.Application.ApplicationServices.User
{
    public class UserDto
    {
        public string UserName { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public DateTime CreatingTime { get; set; }

        public IEnumerable<TopicDto> Topics { get; set; }

        public Roles Role { get; set; }

    }
}
