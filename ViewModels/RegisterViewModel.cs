using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ChatRoom.ViewModels
{
    public class RegisterViewModel
    {
        public String FirstName { get; set; }
        public String LastName { get; set; }
        [DataType(DataType.Password)]
        public String Password { get; set; }
        [DataType(DataType.Password)]
        public String ConfirmPassword { get; set; }

    }
}
