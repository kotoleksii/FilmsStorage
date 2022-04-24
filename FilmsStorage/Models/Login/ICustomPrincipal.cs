using System.Security.Principal;

namespace FilmsStorage.Models.Login
{
    public interface ICustomPrincipal:IPrincipal
    {
        int UserID { get; set; }
    }
}
