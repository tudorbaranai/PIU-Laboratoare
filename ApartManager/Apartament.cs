namespace ApartManager
{
    public class Apartament
    {
        public int Numar { get; set; }
        public int Etaj { get; set; }
        public double Suprafata { get; set; }
        public double PretChirie { get; set; }

        private string[] facilitati;

        public string[] Facilitati
        {
            get { return facilitati; }
            set { facilitati = value; }
        }

        // chiria pe un an
        public double ChirieAnuala
        {
            get
            {
                return PretChirie * 12;
            }
        }

        public bool EsteScump => PretChirie > 2000;

        public Apartament()
        {
            Numar = 0;
            Etaj = 0;
            Suprafata = 0;
            PretChirie = 0;
            facilitati = new string[0];
        }

        public Apartament(int numar, int etaj, double suprafata, double pretChirie)
        {
            Numar = numar;
            Etaj = etaj;
            Suprafata = suprafata;
            PretChirie = pretChirie;
            facilitati = new string[0];
        }

        public Apartament(int numar, int etaj, double suprafata, double pretChirie, string[] facilitati)
        {
            Numar = numar;
            Etaj = etaj;
            Suprafata = suprafata;
            PretChirie = pretChirie;
            this.facilitati = facilitati;
        }

        public string Info()
        {
            string info = $"Apartament nr.{Numar} | Etaj: {Etaj} | Suprafata: {Suprafata} mp | Chirie: {PretChirie} lei/luna";

            if (facilitati.Length > 0)
            {
                string listaFacilitati = string.Join(", ", facilitati);
                info += $" | Facilitati: {listaFacilitati}";
            }

            return info;
        }
    }
}
