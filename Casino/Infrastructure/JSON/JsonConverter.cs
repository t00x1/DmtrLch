using Newtonsoft.Json;
using Domain.Interfaces.Infrastructure.JsonDeserializer;
using System;

namespace Infrastructure.Services
{
    public class JsonConverter : IJsonConverter
    {
        // Метод для десериализации JSON в объект типа T
        public T FromJson<T>(string json) where T : class
        {
            if (string.IsNullOrEmpty(json))
            {
                throw new ArgumentException("JSON string cannot be null or empty.");
            }

            try
            {
                // Десериализация JSON в объект типа T
                return JsonConvert.DeserializeObject<T>(json);
            }
            catch (JsonException ex)
            {
                throw new Exception($"Failed to deserialize JSON to type {typeof(T).Name}. Error: {ex.Message}");
            }
        }


        public string ToJson<T>(T obj) where T : class
        {
            if (obj == null)
            {
                throw new ArgumentNullException(nameof(obj), "Object cannot be null.");
            }

            try
            {

                return JsonConvert.SerializeObject(obj, Formatting.Indented);
            }
            catch (JsonException ex)
            {
                throw new Exception($"Failed to serialize object of type {typeof(T).Name} to JSON. Error: {ex.Message}");
            }
        }
    }
}
