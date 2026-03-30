using LibrarieModele;

namespace NivelStocareDate
{
    public class AdministrareApartamenteFisierText : IStocareData
    {
        private string fisierApartamente;
        private string fisierChiriasi;

        public AdministrareApartamenteFisierText(string caleFisier)
        {
            fisierApartamente = caleFisier + "_apartamente.txt";
            fisierChiriasi = caleFisier + "_chiriasi.txt"

            // cream fisierele daca nu exista
            Stream s1 = File.Open(fisierApartamente, FileMode.OpenOrCreate);
            s1.Close();
            Stream s2 = File.Open(fisierChiriasi, FileMode.OpenOrCreate);
            s2.Close();
        }

        public void AddApartament(Apartament ap)
        {
            //scriere in fisier in modul append
            using(StreamWriter sw = new StreamWriter(fisierApartamente, true))
            {
                sw.WriteLine(ap.ConversieLaSirPentruFisier());
            }
        }

        public List<Apartament> GetApartamente()
        {
            List<Apartament> apartamente = new List<Apartament>();

            using(StreamReader sr = new StreamReader(fisierApartamente))
            {
                string linie;
                while((linie = sr.ReadLine()) != null)
                {
                    if(linie.Trim().Length > 0)
                        apartamente.Add(new Apartament(linie));
                }
            }
            return apartamente;
        }

        public void AddChirias(Chirias ch)
        {
            using(StreamWriter sw = new StreamWriter(fisierChiriasi, true))
            {
                sw.WriteLine(ch.ConversieLaSirPentruFisier());
            }
        }

        public List<Chirias> GetChiriasi()
        {
            List<Chirias> chiriasi = new List<Chirias>();

            using (StreamReader sr = new StreamReader(fisierChiriasi))
            {
                string linie;
                while((linie = sr.ReadLine())!=null)
                {
                    if(linie.Trim().Length>0)
                        chiriasi.Add(new Chirias(linie));
                }
            }
           return chiriasi;
        }

        // cautarile citesc din fisier si filtreaza
        public Apartament CautaDupaNumar(int numar)
        {
            var apartamente = GetApartamente();
            foreach(Apartament ap in apartamente)
            {
                if(ap.Numar == numar)
                    return ap;
            }
            return null;
        }

        public List<Apartament> CautaDupaEtaj(int etaj)
        {
            var apartamente = GetApartamente();
            return apartamente.Where(ap => ap.Etaj == etaj).ToList();
        }

        public List<Apartament> CautaDupaPretMaxim(double pretMax)
        {
            var apartamente = GetApartamente();
            var rezultat = from ap in apartamente
                          where ap.PretChirie <= pretMax
                           select ap;
            return rezultat.ToList();
        }

        public List<Chirias> CautaChiriasiDupaNume(string nume)
        {
            var chiriasi = GetChiriasi();
            return chiriasi.Where(ch => ch.Nume == nume).ToList();
        }
    }
}
