using Domain.Models;
using Domain.Interfaces.Repository;



namespace DataAccess.Repository
{
    public class RepositoryStatistic : RepositoryBase<Statistic>, IRepositoryStatistic
    {
        public RepositoryStatistic(CasinoContext context) : base(context){}
    }
}