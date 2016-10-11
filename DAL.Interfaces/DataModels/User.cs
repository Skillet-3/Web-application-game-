using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DataModels
{
    /// <summary>
    /// Please use this class just when you need Roles or Password for user
    /// </summary>
    public class User : IEntity
    {
        public string ID { get; set; }
        
        public string UserName { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public IEnumerable<string> Roles { get; set; }
    }
}
