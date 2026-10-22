namespace TutorMate.Models
{
    public class TutorApplication
    {
        public int ApplicationId { get; set; }
        public int TutorId { get; set; }
        public string NamaTutor { get; set; }
        public int SubjectId { get; set; }
        public string NamaMataKuliah { get; set; }
        public string NilaiHuruf { get; set; }
        public string HariJadwal { get; set; }
        public string TopikKeahlian { get; set; }
    }
}