namespace Street.Healing.API.Context
{
    public class UserLogin
    {
        /// <summary>
        /// User's email
        /// </summary>
        public required string emailValue { get; set; }

        /// <summary>
        /// User's password
        /// </summary>
        public required string passwordValue { get; set; }
    }
}
