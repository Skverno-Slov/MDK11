using AuthLib.Models;

namespace LabWork15Wpf
{
    public class UserSession
    {
        private UserSession() { }

        private static readonly UserSession _instance = new();
        public static UserSession Instance => _instance;

        public CinemaUser? CurrentUser { get; private set; }

        public void SetCurrentUser(CinemaUser user)
            => CurrentUser = user;

        public void Clear()
            => CurrentUser = null;
    }
}
