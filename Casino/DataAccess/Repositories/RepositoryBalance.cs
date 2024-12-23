using Domain.Models;
using Domain.Interfaces.Repository;



namespace DataAccess.Repository
{
    public class RepositoryBalance : RepositoryBase<Balance>, IRepositoryBalance
    {
        public RepositoryBalance(CasinoContext context) : base(context){}
    }
}