using System;

namespace StudentRegistrationApp.Models
{
    public class Student
    {
        public int StudentId { get; set; }
        public string NIM { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;
        public DateTime? BirthDate { get; set; } = DateTime.Today.AddYears(-20);
        public string PhoneNumber { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public int ProgramId { get; set; }
        public string ProgramName { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public Student Clone() => new()
        {
            StudentId = this.StudentId,
            NIM = this.NIM,
            FullName = this.FullName,
            Email = this.Email,
            Gender = this.Gender,
            BirthDate = this.BirthDate,
            PhoneNumber = this.PhoneNumber,
            Address = this.Address,
            ProgramId = this.ProgramId,
            ProgramName = this.ProgramName,
            CreatedAt = this.CreatedAt
        };
    }
}
