namespace Domain.Containers
{
    // public enum BetTypesWheel
    // {
    //     white,
    //     black,
    //     pink
    // }
    public class BetData
    {
        public string UserID{get;set;} = "";
        public string BetOn{get;set;} = "";
        public int Bet{get;set;}
        public string UserName{get;set;}
    }
}