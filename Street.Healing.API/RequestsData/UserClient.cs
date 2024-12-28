namespace Street.Healing.API.RequestsData
{
    public class UserClient
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
    }
}
