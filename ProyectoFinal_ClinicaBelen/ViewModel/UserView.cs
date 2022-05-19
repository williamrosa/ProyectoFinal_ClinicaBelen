using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProyectoFinal_ClinicaBelen.ViewModel
{
    public class UserView
    {
        public string UserID { get; set; }

        public string UserName { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public List<RoleView> Roles { get; set; }
    }
}