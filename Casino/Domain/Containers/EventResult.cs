namespace Domain.Containers
{

    public class EventResult<T>
    {
        public string Event {get;set;} = "";
        public T Data {get;set;}
    }
}