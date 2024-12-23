using Domain.Common.Generic.Validation;
namespace Domain.Common.Generic.Validation
{
    public class BaseValidation : IBaseValidation
    {
        public bool Validate<T>(T entity)
        {
            if (entity == null)
            {
                return false;
            }

            var properties = typeof(T).GetProperties();
            if (properties.Length == 0)
            {
                return false;
            }

            foreach (var property in properties)
            {
                var propValue = property.GetValue(entity);
                if (propValue == null)
                {
                    continue;
                }

                if (property.PropertyType == typeof(string) && propValue is string stringValue)
                {
                
                    var trimmedValue = stringValue.Trim();
                    if (trimmedValue != stringValue)
                    {
                        property.SetValue(entity, trimmedValue);
                    }
                }
            }
            return true;
        }
    }
}
