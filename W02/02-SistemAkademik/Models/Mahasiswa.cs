using System.Collections.Generic;
using System.Linq;

namespace SistemAkademik.Models
{
    public class Mahasiswa
    {
        public string Nrp { get; set; } = string.Empty;
        public string Nama { get; set; } = string.Empty;
        public string Kelas { get; set; } = "C";
        public string Angkatan { get; set; } = "2024";
        public List<MataKuliah> DaftarNilai { get; set; } = new();

        public int TotalSks => DaftarNilai.Sum(m => m.Sks);

        public double Ipk
        {
            get
            {
                if (DaftarNilai == null || !DaftarNilai.Any()) return 0.0;
                var totalSks = TotalSks;
                if (totalSks == 0) return 0.0;
                return DaftarNilai.Sum(m => m.TotalBobotSks) / totalSks;
            }
        }

        public string Predikat => Ipk switch
        {
            >= 3.51 => "Dengan Pujian (Cumlaude)",
            >= 3.00 => "Sangat Memuaskan",
            >= 2.50 => "Memuaskan",
            >= 2.00 => "Cukup",
            _ => "Perlu Peningkatan"
        };

        public int MaxSksBerikutnya => Ipk switch
        {
            >= 3.00 => 24,
            >= 2.50 => 22,
            >= 2.00 => 20,
            _ => 18
        };
    }
}
