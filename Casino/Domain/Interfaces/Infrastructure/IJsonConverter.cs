using System;

namespace Domain.Interfaces.Infrastructure.JsonDeserializer
{
    public interface IJsonConverter
    {
        T FromJson<T>(string json) where T : class;
        public string ToJson<T>(T obj) where T : class;
    }

}