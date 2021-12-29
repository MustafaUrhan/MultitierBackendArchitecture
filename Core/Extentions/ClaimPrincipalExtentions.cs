using System.Security.Claims;

namespace Core.Extentions
{
    public static class ClaimPrincipalExtentions
    {
        public static List<string>? Claims(this ClaimsPrincipal claimPrincipals, string claimType)
        {
            return claimPrincipals?.FindAll(claimType)?.Select(x => x.Value).ToList();
        }
        public static List<string>? ClaimRoles(this ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal?.Claims(ClaimTypes.Role);
        }
    }
}
