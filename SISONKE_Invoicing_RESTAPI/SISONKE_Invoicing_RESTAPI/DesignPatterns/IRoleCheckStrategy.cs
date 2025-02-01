namespace SISONKE_Invoicing_RESTAPI.DesignPatterns
{
    public interface IRoleCheckStrategy
    {
        Task<bool> CheckRole(string userId);
    }
}
