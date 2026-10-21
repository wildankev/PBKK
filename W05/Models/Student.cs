namespace StudentManager.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string NIM { get; set; } = string.Empty;
        public string Nama { get; set; } = string.Empty;
        public string Jurusan { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        public Student Clone()
        {
            return new Student
            {
                Id = this.Id,
                NIM = this.NIM,
                Nama = this.Nama,
                Jurusan = this.Jurusan,
                Gender = this.Gender,
                Email = this.Email
            };
        }
    }
}
