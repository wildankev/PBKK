namespace SistemAkademik.Models
{
    public class MataKuliah
    {
        public string Kode { get; set; } = string.Empty;
        public string Nama { get; set; } = string.Empty;
        public int Sks { get; set; } = 3;
        public string NilaiHuruf { get; set; } = "A";

        public double Bobot => NilaiHuruf.ToUpper() switch
        {
            "A" => 4.0,
            "AB" => 3.5,
            "B" => 3.0,
            "BC" => 2.5,
            "C" => 2.0,
            "D" => 1.0,
            _ => 0.0
        };

        public double TotalBobotSks => Bobot * Sks;
    }
}
