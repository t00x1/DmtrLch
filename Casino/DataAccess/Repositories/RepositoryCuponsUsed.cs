using Domain.Models;
using Domain.Interfaces.Repository;



namespace DataAccess.Repository
{
    public class RepositoryCuponsUsed : RepositoryBase<CuponsUsed>, IRepositoryCuponsUsed
    {
        public RepositoryCuponsUsed(CasinoContext context) : base(context){}
    }
}