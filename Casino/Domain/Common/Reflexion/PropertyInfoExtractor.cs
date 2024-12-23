using System.Reflection;
using System.Linq;
using Domain.Interfaces.Common.Generic;
namespace Domain.Common.Generic
{
    public class PropertyInfoExtractor : IPropertyInfoExtractor
    {
        public List<PropertyInfo> GetPropertiesWithNonNullValues<T>(T entity) where T : class
        {
            if (entity == null)
            {
                return new List<PropertyInfo>();
            }

            return typeof(T)
                .GetProperties()
                .Where(property => property.CanRead && property.GetValue(entity) != null)
                .ToList();
        }
    }
}
