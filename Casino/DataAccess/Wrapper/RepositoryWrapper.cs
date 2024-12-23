using Domain.Models;
using DataAccess.Repository;
using Domain.Interfaces.Repository;

namespace DataAccess.Wrapper
{
    public class RepositoryWrapper : IRepositoryWrapper
    {   
        private readonly CasinoContext _context;
        private IRepositoryUser? _user;
        private IRepositoryEmail? _email;
        private IRepositoryPictures? _image;
        private IRepositoryCupons? _cupons;
        private IRepositoryCuponsUsed? _cuponsUsed;
        private IRepositoryStatistic? _statistic;
        private IRepositoryBalance _repositoryBalance;

        public RepositoryWrapper(CasinoContext casinoContext)
        {
            _context = casinoContext;
        }

        public IRepositoryUser User
        {
            get
            {
                _user ??= new RepositoryUser(_context);
                return _user;
            }
        }

        public IRepositoryEmail Email
        {
            get
            {
                _email ??= new RepositoryEmail(_context);
                return _email;
            }
        }

        public IRepositoryPictures Image
        {
            get
            {
                _image ??= new RepositoryPictures(_context);
                return _image;
            }
        }

        public IRepositoryBalance Balance
        {
            get
            {
                _repositoryBalance ??= new RepositoryBalance(_context);
                return _repositoryBalance;
            }
        }
        public IRepositoryCupons Cupons
        {
            get
            {
                _cupons ??= new RepositoryCupons(_context);
                return _cupons;
            }
        }

        public IRepositoryCuponsUsed CuponsUsed
        {
            get
            {
                _cuponsUsed ??= new RepositoryCuponsUsed(_context);
                return _cuponsUsed;
            }
        }

        public IRepositoryStatistic Statistic
        {
            get
            {
                _statistic ??= new RepositoryStatistic(_context);
                return _statistic;
            }
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}