﻿using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.DataTransferObjects
{
    class UserDTO
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
