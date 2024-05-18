using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Domain.Models
{
    public class UserModel : BaseModel
    {
        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        
        private string _email;
        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }


    
    }
}