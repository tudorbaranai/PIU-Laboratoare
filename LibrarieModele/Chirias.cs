namespace LibrarieModele
{
    public class Chirias
    {
        private const char SEPARATOR = ';';
        private const int NUME_INDEX = 0;
        private const int PRENUME_INDEX = 1;
        private const int TELEFON_INDEX = 2;
        private const int NR_AP_INDEX = 3;

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

        //constructor din linie fisier
        public Chirias(string linieFisier)
        {
            string[] date = linieFisier.Split(SEPARATOR);
            Nume = date[NUME_INDEX];
            Prenume = date[PRENUME_INDEX];
            Telefon = date[TELEFON_INDEX];
            NumarApartament = Convert.ToInt32(date[NR_AP_INDEX]);
        }

        // conversie pt fisier
        public string ConversieLaSirPentruFisier()
        {
            string linie = string.Format("{1}{0}{2}{0}{3}{0}{4}",
                SEPARATOR,
                Nume ?? "NECUNOSCUT",
                Prenume ?? "NECUNOSCUT",
                Telefon ?? "",
                NumarApartament);
            return linie;
        }

        // afisare informatii chirias
        public string Info()
        {
            return $"Chirias: {NumeComplet} | Tel: {Telefon} | Ap. {NumarApartament}" ;
        }
    }
}
