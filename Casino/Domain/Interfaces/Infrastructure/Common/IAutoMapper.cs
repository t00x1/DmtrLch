namespace Domain.Interfaces.Common.Generic
{
     public interface IAutoMapper
    {
        public void CopyPropertiesTo<TSource, TDest>(TSource source, TDest dest);
    }
}