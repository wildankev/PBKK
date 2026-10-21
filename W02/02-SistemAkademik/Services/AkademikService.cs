using System;
using System.Collections.Generic;
using System.Linq;
using SistemAkademik.Models;

namespace SistemAkademik.Services
{
    public class AkademikService
    {
        private readonly List<Mahasiswa> _daftarMahasiswa = new();

        public AkademikService()
        {
            SeedInitialData();
        }

        private void SeedInitialData()
        {
            // Seed initial data dari kelas PBKK-C
            var m1 = new Mahasiswa
            {
                Nrp = "5025241265",
                Nama = "M. Wildan Kevin A.",
                Kelas = "PBKK-C",
                Angkatan = "2024",
                DaftarNilai = new List<MataKuliah>
                {
                    new() { Kode = "IF4401", Nama = "Pemrograman Berbasis Kerangka Kerja", Sks = 3, NilaiHuruf = "A" },
                    new() { Kode = "IF4402", Nama = "Kecerdasan Komputasional", Sks = 3, NilaiHuruf = "A" },
                    new() { Kode = "IF4403", Nama = "Jaringan Komputer", Sks = 4, NilaiHuruf = "AB" },
                    new() { Kode = "IF4404", Nama = "Rekayasa Perangkat Lunak", Sks = 3, NilaiHuruf = "A" }
                }
            };

            var m2 = new Mahasiswa
            {
                Nrp = "5025241033",
                Nama = "Ferdian Ardra Hafizhan",
                Kelas = "PBKK-C",
                Angkatan = "2024",
                DaftarNilai = new List<MataKuliah>
                {
                    new() { Kode = "IF4401", Nama = "Pemrograman Berbasis Kerangka Kerja", Sks = 3, NilaiHuruf = "A" },
                    new() { Kode = "IF4402", Nama = "Kecerdasan Komputasional", Sks = 3, NilaiHuruf = "AB" },
                    new() { Kode = "IF4403", Nama = "Jaringan Komputer", Sks = 4, NilaiHuruf = "B" }
                }
            };

            var m3 = new Mahasiswa
            {
                Nrp = "5025241150",
                Nama = "Nabila Shafa Rahayu",
                Kelas = "PBKK-C",
                Angkatan = "2024",
                DaftarNilai = new List<MataKuliah>
                {
                    new() { Kode = "IF4401", Nama = "Pemrograman Berbasis Kerangka Kerja", Sks = 3, NilaiHuruf = "A" },
                    new() { Kode = "IF4403", Nama = "Jaringan Komputer", Sks = 4, NilaiHuruf = "A" }
                }
            };

            _daftarMahasiswa.AddRange(new[] { m1, m2, m3 });
        }

        public IReadOnlyList<Mahasiswa> GetAll() => _daftarMahasiswa.AsReadOnly();

        public Mahasiswa? GetByNrp(string nrp)
        {
            return _daftarMahasiswa.FirstOrDefault(m => m.Nrp.Equals(nrp.Trim(), StringComparison.OrdinalIgnoreCase));
        }

        public List<Mahasiswa> Search(string keyword)
        {
            var q = keyword.Trim().ToLowerInvariant();
            return _daftarMahasiswa.Where(m =>
                m.Nrp.ToLowerInvariant().Contains(q) ||
                m.Nama.ToLowerInvariant().Contains(q) ||
                m.Kelas.ToLowerInvariant().Contains(q)
            ).ToList();
        }

        public bool Add(Mahasiswa mahasiswa, out string errorMessage)
        {
            if (GetByNrp(mahasiswa.Nrp) != null)
            {
                errorMessage = $"Mahasiswa dengan NRP '{mahasiswa.Nrp}' sudah terdaftar!";
                return false;
            }

            _daftarMahasiswa.Add(mahasiswa);
            errorMessage = string.Empty;
            return true;
        }

        public bool Update(string nrpLama, string namaBaru, string kelasBaru, string angkatanBaru, out string errorMessage)
        {
            var mhs = GetByNrp(nrpLama);
            if (mhs == null)
            {
                errorMessage = $"Mahasiswa dengan NRP '{nrpLama}' tidak ditemukan!";
                return false;
            }

            mhs.Nama = namaBaru;
            mhs.Kelas = kelasBaru;
            mhs.Angkatan = angkatanBaru;
            errorMessage = string.Empty;
            return true;
        }

        public bool Delete(string nrp, out string errorMessage)
        {
            var mhs = GetByNrp(nrp);
            if (mhs == null)
            {
                errorMessage = $"Mahasiswa dengan NRP '{nrp}' tidak ditemukan!";
                return false;
            }

            _daftarMahasiswa.Remove(mhs);
            errorMessage = string.Empty;
            return true;
        }

        public (int TotalMahasiswa, double RataRataIpk, Mahasiswa? MahasiswaTertinggi) GetStatistik()
        {
            if (!_daftarMahasiswa.Any())
            {
                return (0, 0.0, null);
            }

            var total = _daftarMahasiswa.Count;
            var avgIpk = _daftarMahasiswa.Average(m => m.Ipk);
            var top = _daftarMahasiswa.OrderByDescending(m => m.Ipk).FirstOrDefault();

            return (total, avgIpk, top);
        }
    }
}
