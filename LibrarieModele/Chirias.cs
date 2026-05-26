using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace LibrarieModele
{
    // lab 10 + 11 - notificari pt binding si validare pe model
    public class Chirias : INotifyPropertyChanged, IDataErrorInfo
    {
        private const char SEPARATOR = ';';
        private const int NUME_INDEX = 0;
        private const int PRENUME_INDEX = 1;
        private const int TELEFON_INDEX = 2;
        private const int NR_AP_INDEX = 3;
        private const int LUNGIME_MAXIMA_NUME = 30;

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
                OnPropertyChanged(nameof(EsteValid));
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
                OnPropertyChanged(nameof(EsteValid));
            }
        }
        public string Telefon
        {
            get => telefon;
            set
            {
                telefon = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(EsteValid));
            }
        }
        public int NumarApartament
        {
            get => numarApartament;
            set
            {
                numarApartament = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(EsteValid));
            }
        }

        // calculat - NU are setter, deci notificarea vine din Nume/Prenume
        public string NumeComplet=> ($"{Nume} {Prenume}").Trim();

        // lab 11 - verificare simpla pentru tot obiectul
        public bool EsteValid =>
            string.IsNullOrEmpty(this[nameof(Nume)]) &&
            string.IsNullOrEmpty(this[nameof(Prenume)]) &&
            string.IsNullOrEmpty(this[nameof(Telefon)]) &&
            string.IsNullOrEmpty(this[nameof(NumarApartament)]);

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

        // lab 11 - IDataErrorInfo
        public string Error => string.Empty;

        public string this[string columnName]
        {
            get
            {
                switch(columnName)
                {
                    case nameof(Nume):
                        if(string.IsNullOrWhiteSpace(Nume))
                            return "Numele trebuie completat!";
                        if(Nume.Length > LUNGIME_MAXIMA_NUME)
                            return $"Numele nu poate depasi {LUNGIME_MAXIMA_NUME} caractere!";
                        break;

                    case nameof(Prenume):
                        if(string.IsNullOrWhiteSpace(Prenume))
                            return "Prenumele trebuie completat!";
                        if(Prenume.Length > LUNGIME_MAXIMA_NUME)
                            return $"Prenumele nu poate depasi {LUNGIME_MAXIMA_NUME} caractere!";
                        break;

                    case nameof(Telefon):
                        if(string.IsNullOrWhiteSpace(Telefon))
                            return "Telefonul trebuie completat!";
                        if(Telefon.Length != 10)
                            return "Telefonul trebuie sa aiba 10 cifre!";
                        if(!Telefon.All(char.IsDigit))
                            return "Telefonul trebuie sa contina doar cifre!";
                        break;

                    case nameof(NumarApartament):
                        if(NumarApartament <= 0)
                            return "Numarul apartamentului trebuie sa fie pozitiv!";
                        break;
                }

                return string.Empty;
            }
        }

        // INotifyPropertyChanged (lab 10)
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
