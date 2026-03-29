namespace ApartManager
{
    public class Chirias
    {
        public string Nume { get; set; }
        public string Prenume { get; set; }
        public string Telefon { get; set; }
        public int NumarApartament { get; set; }

        // nume + prenume
        public string NumeComplet => Nume + " " + Prenume;

        public Chirias()
        {
            Nume = string.Empty;
            Prenume = string.Empty;
            Telefon = string.Empty;
            NumarApartament = 0;
        }

        public Chirias(string nume, string prenume, string telefon, int numarApartament)
        {
            Nume = nume;
            Prenume = prenume;
            Telefon = telefon;
            NumarApartament = numarApartament;
        }

        public string Info()
        {
            string info = $"Chirias: {NumeComplet} | Telefon: {Telefon} | Apartament: {NumarApartament}";
            return info;
        }
    }
}
