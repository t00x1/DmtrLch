using Domain.Containers;
using Domain.Interfaces.Response;

namespace CasinoApi.Router
{
    public interface IRouter
    {
       Task<IResponse<RouterResult>> Route(string jsonParam, string UserId);
    }
}