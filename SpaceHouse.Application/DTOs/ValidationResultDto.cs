namespace SpaceHouse.Application.DTOs
{
    public class ValidationResultDto
    {
        public bool IsValid { get; set; }
        public string Message { get; set; } = string.Empty;
        public string? ZoneId { get; set; } // ID de la zone concernée, si applicable
    }
}
