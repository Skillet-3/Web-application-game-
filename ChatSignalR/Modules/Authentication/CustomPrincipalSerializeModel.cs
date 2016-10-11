using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChatSignalR.Modules.Authentication
{
    public class CustomPrincipalSerializeModel
    {
        public string UserId { get; set; }
        public string[] Roles { get; set; }
    }
}