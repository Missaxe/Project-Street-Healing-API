﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Street.Healing.DTO.ModelsDTO
{
    public class UserDTO
    {
        /// <summary>
        /// User's first name
        /// </summary>
        public required string FirstName { get; set; }

        /// <summary>
        /// User's last name
        /// </summary>
        public required string LastName { get; set; }


        /// <summary>
        /// User's phone Number
        /// </summary>
        public required string PhoneNumber { get; set; }

        /// <summary>
        /// User's email
        /// </summary>
        public required string Email { get; set; }

        /// <summary>
        /// User's password
        /// </summary>
        public required string Password { get; set; }


        /// <summary>
        /// User's password confirmation
        /// </summary>
        public required string ConfirmPassword { get; set; }


        /// <summary>
        /// User's email confirmation
        /// </summary>
        public bool IsEmailValid { get; set; }



        /// <summary>
        /// User's token
        /// </summary>
        public string? Token { get; set; }


        /// <summary>
        /// User's date creation
        /// </summary>
        public DateTime DateCreated { get; set; }

        /// <summary>
        /// HashedPass
        /// </summary>
        public Tuple<string, string>? HashedPass { get; set; }
    }
}
