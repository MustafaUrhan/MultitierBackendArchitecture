using Core.Dtos;
using Core.Entities;

namespace Core.Utilities.Security.JWT
{
    public interface ITokenHelper
    {
        AccessToken CreateToken(User user, List<OperationClaimDto> operationClaims);
    }
}
