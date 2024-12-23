namespace Domain.Common.Generic.Validation
{
    public interface IBaseValidation
    {
         bool Validate<T>(T entity);
    }
}