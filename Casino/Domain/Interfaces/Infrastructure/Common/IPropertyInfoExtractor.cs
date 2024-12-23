using System.Reflection;
namespace Domain.Interfaces.Common.Generic
{
    public interface IPropertyInfoExtractor
    {
        List<PropertyInfo> GetPropertiesWithNonNullValues<T>(T entity) where T : class;
    }
}