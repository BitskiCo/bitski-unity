namespace Bitski.Auth
{
    using System;
    
    /// <summary>
    /// A Bitski user.
    /// </summary>
    public class User
    {
        public string AccessToken { get; set; }
        public string UserId { get; set; }
        public DateTime ExpiresAt { get; set; }

        public bool IsExpired
        {
            get
            {
                return ExpiresAt > DateTime.Now;
            }
        }
    }
}