using LibrarieModele;

namespace NivelStocareDate
{
    public class AdministrareApartamenteMemorie
    {
        private List<Apartament> apartamente = new List<Apartament>();
        private List<Chirias> chiriasi = new List<Chirias>();

        public void AddApartament(Apartament ap)
        {
            apartamente.Add(ap);
        }
        public void AddChirias(Chirias ch)
        {
            chiriasi.Add(ch);
        }

        public List<Apartament> GetApartamente()
        {
            return apartamente;
        }
        public List<Chirias> GetChiriasi()
        {
            return chiriasi;
        }

        public Apartament CautaDupaNumar(int numar)
        {
            foreach(Apartament ap in apartamente)
            {
                if(ap.Numar == numar)
                    return ap;
            }
            return null;
        }

        //cautare cu linQ
        public List<Apartament> CautaDupaEtaj(int etaj)
        {
            var rezultat = apartamente.Where(ap => ap.Etaj == etaj).ToList();
           return rezultat;
        }

        public List<Apartament> CautaDupaPretMaxim(double pretMax)
        {
            var rezultat = from ap in apartamente
                          where ap.PretChirie <= pretMax
                          select ap;
            return rezultat.ToList();
        }

        // cauta chiriasii dupa nme cu linq
        public List<Chirias> CautaChiriasiDupaNume(string nume)
        {
            return chiriasi.Where(ch => ch.Nume == nume).ToList();
        }
    }
}
