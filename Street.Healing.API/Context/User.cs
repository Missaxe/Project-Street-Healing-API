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
        public required string FirstName { get; set; }

        /// <summary>
        /// User's last name
        /// </summary>
        public required string LastName { get; set; }

        /// <summary>
        /// User's email
        /// </summary>
        public required string Email { get; set; }

        /// <summary>
        /// User's password
        /// </summary>
        public required string Password { get; set; }


        /// <summary>
        /// User's date creation
        /// </summary>
        public required DateTime DateCreated { get; set; }
    }
}

