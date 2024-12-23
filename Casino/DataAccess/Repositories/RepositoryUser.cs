using Domain.Models;
using Domain.Interfaces.Repository;


namespace DataAccess.Repository
{
    public class RepositoryUser : RepositoryBase<User>, IRepositoryUser
    {
        public RepositoryUser(CasinoContext context) : base(context){}
    }
}