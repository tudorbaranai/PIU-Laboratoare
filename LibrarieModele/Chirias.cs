using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace LibrarieModele
{
    // lab 10 - implementeaza INotifyPropertyChanged ca sa se sincronizeze cu UI-ul
    public class Chirias : INotifyPropertyChanged
    {
        private const char SEPARATOR = ';';
        private const int NUME_INDEX = 0;
        private const int PRENUME_INDEX = 1;
        private const int TELEFON_INDEX = 2;
        private const int NR_AP_INDEX = 3;

        private string nume;
        private string prenume;
        private string telefon;
        private int numarApartament;

        public string Nume
        {
            get => nume;
            set
            {
                nume = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(NumeComplet));
            }
        }
        public string Prenume
        {
            get => prenume;
            set
            {
                prenume = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(NumeComplet));
            }
        }
        public string Telefon
        {
            get => telefon;
            set
            {
                telefon = value;
                OnPropertyChanged();
            }
        }
        public int NumarApartament
        {
            get => numarApartament;
            set
            {
                numarApartament = value;
                OnPropertyChanged();
            }
        }

        // calculat - NU are setter, deci notificarea vine din Nume/Prenume
        public string NumeComplet=> ($"{Nume} {Prenume}").Trim();

        public Chirias()
        {
            nume = string.Empty;
            prenume = string.Empty;
            telefon = string.Empty;
            numarApartament = 0;
        }
        public Chirias(string n, string p, string t, int nr)
        {
            nume = n;
            prenume = p;
            telefon = t;
            numarApartament = nr;
        }

        //constructor din linie fisier
        public Chirias(string linieFisier)
        {
            string[] date = linieFisier.Split(SEPARATOR);
            nume = date[NUME_INDEX];
            prenume = date[PRENUME_INDEX];
            telefon = date[TELEFON_INDEX];
            numarApartament = Convert.ToInt32(date[NR_AP_INDEX]);
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

        // INotifyPropertyChanged (lab 10)
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
