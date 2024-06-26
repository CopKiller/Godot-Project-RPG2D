﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonRunes.Models
{
    public class AccountModel
    {
        public int Id { get; set; }
        public string User { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public DateTime BirthDate { get; set; } = DateTime.MinValue;
        public string Email { get; set; } = string.Empty;
        public string Salt { get; set; } = string.Empty;
        public int PlayerId { get; set; }

        public AccountModel()
        {
        }
    }
}
