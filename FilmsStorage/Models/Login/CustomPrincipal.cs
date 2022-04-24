using System.Security.Principal;

namespace FilmsStorage.Models.Login
{
    public class CustomPrincipal : ICustomPrincipal
    {
        public int UserID { get; set; }

        public IIdentity Identity { get; private set; }

        //Завжди false, тому що не використовуємо систему ролей
        public bool IsInRole(string role)
        {
            return false;
        }

        public CustomPrincipal(string userName, int userID)
        {
            Identity = new GenericIdentity(userName);
            UserID = userID;
        }

    }
}