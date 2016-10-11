using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DataModels
{
    public class Role : IEntity
    {
        public string ID { get; set; }
        public string RoleName { get; set; }
    }
}
