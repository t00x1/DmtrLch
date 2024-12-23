using Domain.Models;
using Domain.Interfaces.Repository;



namespace DataAccess.Repository
{
    public class RepositoryEmail : RepositoryBase<EmailConfirmation>, IRepositoryEmail
    {
        public RepositoryEmail(CasinoContext context) : base(context){}
    }
}