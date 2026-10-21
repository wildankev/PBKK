namespace StudentRegistrationApp.Models
{
    public class ProgramStudy
    {
        public int ProgramId { get; set; }
        public string ProgramCode { get; set; } = string.Empty;
        public string ProgramName { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;

        public string DisplayName => $"{ProgramCode} - {ProgramName}";
    }
}
