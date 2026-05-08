using System.Globalization;

namespace LibrarieModele
{
    public enum TipApartament
    {
        Garsoniera = 1,
        DoaCamere = 2,
        TreiCamere = 3,
        PatruCamere = 4,
        Penthouse = 5
    }

    [Flags]
    public enum FacilitatiApartament
    {
        Niciuna = 0,
        Balcon = 1,
        Parcare = 2,
        CentralaTermica = 4,
        AerConditionat = 8,
        LocDeJoaca = 16
    }

    public class Apartament
    {
        //constante pt pozitiile campurilor in fisier
        private const char SEPARATOR = ';';
        private const int NUMAR = 0;
        private const int ETAJ = 1;
        private const int SUPRAFATA = 2;
        private const int PRET = 3;
        private const int TIP = 4;
        private const int FACILITATI = 5;
        // campuri noi adaugate la lab 9
        private const int DATA_ADAUGARE = 6;
        private const int DATA_ACTUALIZARE = 7;
        private const int TIP_FINANTARE = 8;

        private const string FORMAT_DATA = "yyyy-MM-dd";

        public int Numar { get; set; }
        public int Etaj { get; set; }
        public double Suprafata { get; set; }
        public double PretChirie { get; set; }
        public TipApartament Tip { get; set; }
        public FacilitatiApartament Facilitati { get; set; }

        // lab 9 - campuri noi
        public DateTime DataAdaugare { get; set; }
        public DateTime DataActualizare { get; set; }
        public string TipFinantare { get; set; }

        //chirira pe un an
        public double ChirieAnuala
        {
            get
            {
                return PretChirie * 12;
            }
        }
        public bool EsteScump=> PretChirie > 2000;

        // pt afisare in combobox
        public string AfisareScurta=> $"Ap. {Numar} - et. {Etaj} - {Tip}";

        public Apartament()
        {
            Numar = 0;
            Etaj = 0;
            Suprafata = 0;
            PretChirie = 0;
            Tip = TipApartament.Garsoniera;
            Facilitati = FacilitatiApartament.Niciuna;
            DataAdaugare = DateTime.Today;
            DataActualizare = DateTime.Today;
            TipFinantare = "Disponibil";
        }

        public Apartament(int numar, int etaj, double suprafata, double pretChirie,
            TipApartament tip, FacilitatiApartament facilitati)
        {
            Numar = numar;
            Etaj = etaj;
            Suprafata = suprafata;
            PretChirie = pretChirie;
            Tip = tip;
            Facilitati = facilitati;
            DataAdaugare = DateTime.Today;
            DataActualizare = DateTime.Today;
            TipFinantare = "Disponibil";
        }
        public Apartament(int numar,int etaj,double suprafata,double pretChirie)
        {
            Numar = numar;
            Etaj = etaj;
            Suprafata = suprafata;
            PretChirie = pretChirie;
             Tip = TipApartament.Garsoniera;
            Facilitati = FacilitatiApartament.Niciuna;
            DataAdaugare = DateTime.Today;
            DataActualizare = DateTime.Today;
            TipFinantare = "Disponibil";
        }

        // constructor care preia o linie din fisier
        public Apartament(string linieFisier)
        {
            string[] date = linieFisier.Split(SEPARATOR);
            Numar = Convert.ToInt32(date[NUMAR]);
            Etaj = Convert.ToInt32(date[ETAJ]);
            Suprafata = Convert.ToDouble(date[SUPRAFATA]);
            PretChirie = Convert.ToDouble(date[PRET]);
            Tip = (TipApartament)Convert.ToInt32(date[TIP]);
            Facilitati = (FacilitatiApartament)Convert.ToInt32(date[FACILITATI]);

            // campuri noi (lab 9) - daca nu sunt in fisier, valori implicite
            // ca sa mearga si datele vechi
            if(date.Length > DATA_ADAUGARE && !string.IsNullOrEmpty(date[DATA_ADAUGARE]))
                DataAdaugare = DateTime.ParseExact(date[DATA_ADAUGARE], FORMAT_DATA, CultureInfo.InvariantCulture);
            else
                DataAdaugare = DateTime.Today;

            if(date.Length > DATA_ACTUALIZARE && !string.IsNullOrEmpty(date[DATA_ACTUALIZARE]))
                DataActualizare = DateTime.ParseExact(date[DATA_ACTUALIZARE], FORMAT_DATA, CultureInfo.InvariantCulture);
            else
                DataActualizare = DateTime.Today;

            if(date.Length > TIP_FINANTARE && !string.IsNullOrEmpty(date[TIP_FINANTARE]))
                TipFinantare = date[TIP_FINANTARE];
            else
                TipFinantare = "Disponibil";
        }

        //conversie obiect la sir pt scriere in fisier
        public string ConversieLaSirPentruFisier()
        {
            string linie = string.Format("{1}{0}{2}{0}{3}{0}{4}{0}{5}{0}{6}{0}{7}{0}{8}{0}{9}",
                SEPARATOR,
                Numar,
                Etaj,
                Suprafata,
                PretChirie,
                (int)Tip,
                (int)Facilitati,
                DataAdaugare.ToString(FORMAT_DATA, CultureInfo.InvariantCulture),
                DataActualizare.ToString(FORMAT_DATA, CultureInfo.InvariantCulture),
                TipFinantare ?? "Disponibil");
           return linie;
        }

        public string Info()
        {
            string info = $"Ap.{Numar} | Etaj:{Etaj} | {Suprafata}mp | {PretChirie} lei/luna | {Tip}";
            if (Facilitati != FacilitatiApartament.Niciuna)
                info += $" | {Facilitati}";
            info += $" | {TipFinantare}";
           return info;
        }
    }
}
