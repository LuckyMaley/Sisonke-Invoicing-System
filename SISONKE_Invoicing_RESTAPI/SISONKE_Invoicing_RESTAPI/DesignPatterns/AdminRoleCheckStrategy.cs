using SISONKE_Invoicing_RESTAPI.Services;

namespace SISONKE_Invoicing_RESTAPI.DesignPatterns
{
    public class AdminRoleCheckStrategy : IRoleCheckStrategy
    {
        private readonly IdentityHelper _identityHelper;

        public AdminRoleCheckStrategy(IdentityHelper identityHelper)
        {
            _identityHelper = identityHelper;
        }

        public async Task<bool> CheckRole(string userId)
        {
            return await _identityHelper.IsSuperUserRole(userId);
        }
    }
}
