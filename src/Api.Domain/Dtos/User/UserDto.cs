using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Domain.Dtos.User
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public String Name { get; set; }
        public String Email { get; set; }
        public DateTime CreateAt { get; set; }
    }
}