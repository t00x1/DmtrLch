using Domain.Models;
using Domain.Interfaces.Repository;



namespace DataAccess.Repository
{
    public class RepositoryPictures : RepositoryBase<Image>, IRepositoryPictures
    {
        public RepositoryPictures(CasinoContext context) : base(context){}
    }
}