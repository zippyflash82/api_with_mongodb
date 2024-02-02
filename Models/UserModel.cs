using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace api_with_mongodb.Models
{
    public class UserModel
    {
        [Key]
        public int Id { get; set; }
        public string USERNAME { get; set; }

        public string EmailAddress { get; set; }

        public string Password { get; set; }
    }
}