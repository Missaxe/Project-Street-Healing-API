using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Street.Healing.DAO.Models
{
    public class User
    {
        /// <summary>
        /// User's ID
        /// </summary>
        public int Id { get; set; }

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
        /// User's hash password
        /// </summary>
        public required string HashPassword { get; set; }

        /// <summary>
        /// User's salt password
        /// </summary>
        public required string SaltPassword { get; set; }


        /// <summary>
        /// User's date creation
        /// </summary>
        public DateTime DateCreated { get; set; }
    }
}
