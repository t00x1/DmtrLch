using Domain.Models;
using Domain.Interfaces.Repository;



namespace DataAccess.Repository
{
    public class RepositoryCupons : RepositoryBase<Cupon>, IRepositoryCupons
    {
        public RepositoryCupons(CasinoContext context) : base(context){}
    }
}