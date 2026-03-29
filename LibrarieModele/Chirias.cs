namespace LibrarieModele
{
    public class Chirias
    {
        public string Nume { get; set; }
        public string Prenume { get; set; }
        public string Telefon { get; set; }
        public int NumarApartament { get; set; }
        public string NumeComplet=> Nume + " " + Prenume;

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

        // afisare informatii chirias
        public string Info()
        {
            return $"Chirias: {NumeComplet} | Tel: {Telefon} | Ap. {NumarApartament}" ;
        }
    }
}
