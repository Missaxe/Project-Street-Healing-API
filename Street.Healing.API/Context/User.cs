namespace Street.Healing.API.Context
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
        public  string FirstName { get; set; }

        /// <summary>
        /// User's last name
        /// </summary>
        public  string LastName { get; set; }



        /// <summary>
        /// User's phone Number
        /// </summary>
        public  string PhoneNumber { get; set; }

        /// <summary>
        /// User's email
        /// </summary>
        public  string Email { get; set; }

        /// <summary>
        /// User's hash password
        /// </summary>
        public  string HashPassword { get; set; }

        /// <summary>
        /// User's salt password
        /// </summary>
        public  string SaltPassword { get; set; }


        /// <summary>
        /// User's date creation
        /// </summary>
        public  DateTime DateCreated { get; set; }
    }
}

