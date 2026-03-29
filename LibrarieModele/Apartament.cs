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
        public int Numar { get; set; }
        public int Etaj { get; set; }
        public double Suprafata { get; set; }
        public double PretChirie { get; set; }
        public TipApartament Tip { get; set; }
        public FacilitatiApartament Facilitati { get; set; }

        //chirira pe un an
        public double ChirieAnuala
        {
            get
            {
                return PretChirie * 12;
            }
        }
        public bool EsteScump=> PretChirie > 2000;

        public Apartament()
        {
            Numar = 0;
            Etaj = 0;
            Suprafata = 0;
            PretChirie = 0;
            Tip = TipApartament.Garsoniera;
            Facilitati = FacilitatiApartament.Niciuna;
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
        }
        public Apartament(int numar,int etaj,double suprafata,double pretChirie)
        {
            Numar = numar;
            Etaj = etaj;
            Suprafata = suprafata;
            PretChirie = pretChirie;
             Tip = TipApartament.Garsoniera;
            Facilitati = FacilitatiApartament.Niciuna;
        }

        public string Info()
        {
            string info = $"Ap.{Numar} | Etaj:{Etaj} | {Suprafata}mp | {PretChirie} lei/luna | {Tip}";
            if (Facilitati != FacilitatiApartament.Niciuna)
                info += $" | {Facilitati}";
           return info;
        }
    }
}
