namespace Domain.DTO 
{
    public class TokenDecodeResult
    {
        public Dictionary<string, string>? Claims { get; set; }
        public bool IsValid { get; set; }
        public string? ErrorMessage { get; set; }




    } 
}