using Core.Dtos;
using System.Security.Claims;

namespace Core.Extentions
{
    public static class ClaimPrinciplasExtentions
    {
        public static void AddName(this ICollection<Claim> claims, string name)
        {
            claims.Add(new Claim(ClaimTypes.Name, name));
        }
        public static void AddNameIdentifier(this ICollection<Claim> claims, string nameIdentifier)
        {
            claims.Add(new Claim(ClaimTypes.NameIdentifier, nameIdentifier));
        }
        public static void AddEmail(this ICollection<Claim> claims, string email)
        {
            claims.Add(new Claim(ClaimTypes.Email, email));
        }
        public static void AddRoles(this ICollection<Claim> claims, List<OperationClaimDto> operationClaims)
        {
            operationClaims.ForEach(x => claims.Add(new Claim(ClaimTypes.Role, x.Name)));
        }
    }
}
