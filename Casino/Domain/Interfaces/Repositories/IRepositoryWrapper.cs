namespace Domain.Interfaces.Repository
{
    public interface IRepositoryWrapper
    {
        IRepositoryUser User { get; }
        IRepositoryEmail Email { get; }
        IRepositoryPictures Image { get; }
        IRepositoryCupons Cupons { get; }
        IRepositoryBalance Balance { get; }
        IRepositoryCuponsUsed CuponsUsed { get; }
        IRepositoryStatistic Statistic { get; }

        Task SaveChangesAsync();
    }
}