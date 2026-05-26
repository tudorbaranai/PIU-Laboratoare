using LibrarieModele;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Data;

namespace NivelUIWPF
{
    // lab 11 - ViewModel simplu pentru zona de chiriasi
    public class MainViewModel : INotifyPropertyChanged
    {
        private Chirias chiriasCurent;
        private string filtruChirias;
        private CollectionViewSource chiriasiViewSource;

        public ObservableCollection<Chirias> ChiriasiAfisati { get; }
        public ICollectionView ChiriasiView => chiriasiViewSource.View;

        public Chirias ChiriasCurent
        {
            get => chiriasCurent;
            set
            {
                if(chiriasCurent != null)
                    chiriasCurent.PropertyChanged -= ChiriasCurent_PropertyChanged;

                chiriasCurent = value;

                if(chiriasCurent != null)
                    chiriasCurent.PropertyChanged += ChiriasCurent_PropertyChanged;

                OnPropertyChanged();
                OnPropertyChanged(nameof(EsteChiriasValid));
            }
        }

        public string FiltruChirias
        {
            get => filtruChirias;
            set
            {
                filtruChirias = value;
                OnPropertyChanged();
                ChiriasiView.Refresh();
            }
        }

        public bool EsteChiriasValid => ChiriasCurent != null && ChiriasCurent.EsteValid;

        public MainViewModel()
        {
            ChiriasiAfisati = new ObservableCollection<Chirias>();
            chiriasCurent = new Chirias();
            filtruChirias = string.Empty;

            chiriasiViewSource = new CollectionViewSource();
            chiriasiViewSource.Source = ChiriasiAfisati;
            chiriasiViewSource.Filter += ChiriasiViewSource_Filter;
            ChiriasCurent.PropertyChanged += ChiriasCurent_PropertyChanged;
        }

        public void IncarcaChiriasi(IEnumerable<Chirias> chiriasi)
        {
            ChiriasiAfisati.Clear();
            foreach(Chirias ch in chiriasi)
                ChiriasiAfisati.Add(ch);

            ChiriasiView.Refresh();
        }

        public void AdaugaChiriasInLista(Chirias chirias)
        {
            ChiriasiAfisati.Add(chirias);
            ChiriasiView.Refresh();
        }

        public void ResetFormularChirias()
        {
            ChiriasCurent = new Chirias();
        }

        private bool FiltreazaChirias(object item)
        {
            if(item is not Chirias ch)
                return false;

            if(string.IsNullOrWhiteSpace(FiltruChirias))
                return true;

            string cautare = FiltruChirias.Trim().ToLower();

            return (ch.Nume ?? string.Empty).ToLower().Contains(cautare) ||
                   (ch.Prenume ?? string.Empty).ToLower().Contains(cautare) ||
                   (ch.Telefon ?? string.Empty).ToLower().Contains(cautare) ||
                   ch.NumarApartament.ToString().Contains(cautare);
        }

        private void ChiriasiViewSource_Filter(object sender, FilterEventArgs e)
        {
            e.Accepted = FiltreazaChirias(e.Item);
        }

        private void ChiriasCurent_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            OnPropertyChanged(nameof(EsteChiriasValid));
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
