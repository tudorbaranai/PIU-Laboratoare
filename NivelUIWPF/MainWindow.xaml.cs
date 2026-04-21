using LibrarieModele;
using NivelStocareDate;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace NivelUIWPF
{
    public partial class MainWindow : Window
    {
        //constante pt limite (tema acasa lab 7)
        private const int NUMAR_MAXIM = 9999;
        private const int ETAJ_MAXIM = 50;
        private const double SUPRAFATA_MAXIMA = 1000;
        private const double PRET_MAXIM = 100000;

        private IStocareData adminApartamente;

        public MainWindow()
        {
            InitializeComponent();
            adminApartamente = StocareFactory.GetAdministratorStocare();
            AfiseazaApartamente(adminApartamente.GetApartamente());
        }

        private void AfiseazaApartamente(List<Apartament> lista)
        {
            lblNrApartamente.Content = $"Numar apartamente: {lista.Count}";
            //refresh la grid
            dgApartamente.ItemsSource = null;
            dgApartamente.ItemsSource = lista;
        }

        private void btnSalveaza_Click(object sender, RoutedEventArgs e)
        {
            string sNumar = txtNumar.Text.Trim();
            string sEtaj = txtEtaj.Text.Trim();
            string sSupr = txtSuprafata.Text.Trim();
            string sPret = txtPret.Text.Trim();

            if(!ValideazaDate(sNumar, sEtaj, sSupr, sPret, out int numar, out int etaj,
                out double suprafata, out double pret))
            {
                return;
            }

            TipApartament tip = GetTipSelectat();
            FacilitatiApartament facilitati = GetFacilitatiSelectate();

            Apartament ap = new Apartament(numar, etaj, suprafata, pret, tip, facilitati);
            adminApartamente.AddApartament(ap);

            AfiseazaApartamente(adminApartamente.GetApartamente());
        }

        private void btnReseteaza_Click(object sender, RoutedEventArgs e)
        {
            txtNumar.Clear();
            txtEtaj.Clear();
            txtSuprafata.Clear();
            txtPret.Clear();
            rbGarsoniera.IsChecked = true;
            cbBalcon.IsChecked = false;
            cbParcare.IsChecked = false;
            cbCentrala.IsChecked = false;
            cbAC.IsChecked = false;
            cbLocJoaca.IsChecked = false;
            ReseteazaErori();
        }

        private void btnCauta_Click(object sender, RoutedEventArgs e)
        {
            string s = txtPretMax.Text.Trim();
            if(string.IsNullOrEmpty(s))
            {
                MessageBox.Show("Introduceti un pret maxim!", "Atentie", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            try
            {
                double pretMax = double.Parse(s);
                var rezultat = adminApartamente.CautaDupaPretMaxim(pretMax);
                AfiseazaApartamente(rezultat);
            }
            catch(System.FormatException)
            {
                MessageBox.Show("Pretul trebuie sa fie un numar!", "Eroare", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnResetCautare_Click(object sender, RoutedEventArgs e)
        {
            txtPretMax.Clear();
            AfiseazaApartamente(adminApartamente.GetApartamente());
        }

        private TipApartament GetTipSelectat()
        {
            if(rbDouaCamere.IsChecked == true) return TipApartament.DoaCamere;
            if(rbTreiCamere.IsChecked == true) return TipApartament.TreiCamere;
            if(rbPatruCamere.IsChecked == true) return TipApartament.PatruCamere;
            if(rbPenthouse.IsChecked == true) return TipApartament.Penthouse;
            return TipApartament.Garsoniera;
        }

        private FacilitatiApartament GetFacilitatiSelectate()
        {
            FacilitatiApartament f = FacilitatiApartament.Niciuna;
            if(cbBalcon.IsChecked == true) f |= FacilitatiApartament.Balcon;
            if(cbParcare.IsChecked == true) f |= FacilitatiApartament.Parcare;
            if(cbCentrala.IsChecked == true) f |= FacilitatiApartament.CentralaTermica;
            if(cbAC.IsChecked == true) f |= FacilitatiApartament.AerConditionat;
            if(cbLocJoaca.IsChecked == true) f |= FacilitatiApartament.LocDeJoaca;
            return f;
        }

        private bool ValideazaDate(string sNumar, string sEtaj, string sSupr, string sPret,
            out int numar, out int etaj, out double suprafata, out double pret)
        {
            ReseteazaErori();
            numar = 0; etaj = 0; suprafata = 0; pret = 0;

            if(string.IsNullOrEmpty(sNumar))
            {
                AfiseazaEroare(txtNumar, tbErrNumar, "Numarul apartamentului trebuie completat!");
                return false;
            }
            if(!int.TryParse(sNumar, out numar) || numar <= 0)
            {
                AfiseazaEroare(txtNumar, tbErrNumar, "Numarul trebuie sa fie un intreg pozitiv!");
                return false;
            }
            if(numar > NUMAR_MAXIM)
            {
                AfiseazaEroare(txtNumar, tbErrNumar, $"Numarul nu poate depasi {NUMAR_MAXIM}!");
                return false;
            }

            if(string.IsNullOrEmpty(sEtaj))
            {
                AfiseazaEroare(txtEtaj, tbErrEtaj, "Etajul trebuie completat!");
                return false;
            }
            if(!int.TryParse(sEtaj, out etaj) || etaj < 0)
            {
                AfiseazaEroare(txtEtaj, tbErrEtaj, "Etajul trebuie sa fie un numar >= 0!");
                return false;
            }
            if(etaj > ETAJ_MAXIM)
            {
                AfiseazaEroare(txtEtaj, tbErrEtaj, $"Etajul nu poate depasi {ETAJ_MAXIM}!");
                return false;
            }

            if(string.IsNullOrEmpty(sSupr))
            {
                AfiseazaEroare(txtSuprafata, tbErrSuprafata, "Suprafata trebuie completata!");
                return false;
            }
            if(!double.TryParse(sSupr, out suprafata) || suprafata <= 0)
            {
                AfiseazaEroare(txtSuprafata, tbErrSuprafata, "Suprafata trebuie sa fie un numar pozitiv!");
                return false;
            }
            if(suprafata > SUPRAFATA_MAXIMA)
            {
                AfiseazaEroare(txtSuprafata, tbErrSuprafata, $"Suprafata nu poate depasi {SUPRAFATA_MAXIMA} mp!");
                return false;
            }

            if(string.IsNullOrEmpty(sPret))
            {
                AfiseazaEroare(txtPret, tbErrPret, "Pretul chiriei trebuie completat!");
                return false;
            }
            if(!double.TryParse(sPret, out pret) || pret <= 0)
            {
                AfiseazaEroare(txtPret, tbErrPret, "Pretul trebuie sa fie un numar pozitiv!");
                return false;
            }
            if(pret > PRET_MAXIM)
            {
                AfiseazaEroare(txtPret, tbErrPret, $"Pretul nu poate depasi {PRET_MAXIM} lei!");
                return false;
            }

            return true;
        }

        private void ReseteazaErori()
        {
            AscundeEroare(txtNumar, tbErrNumar);
            AscundeEroare(txtEtaj, tbErrEtaj);
            AscundeEroare(txtSuprafata, tbErrSuprafata);
            AscundeEroare(txtPret, tbErrPret);
        }

        private void AscundeEroare(TextBox tb, TextBlock err)
        {
            tb.ClearValue(Control.BorderBrushProperty);
            tb.ClearValue(Control.BackgroundProperty);
            err.Text = string.Empty;
            err.Visibility = Visibility.Collapsed;
        }

        private void AfiseazaEroare(TextBox tb, TextBlock err, string mesaj)
        {
            tb.BorderBrush = Brushes.Red;
            tb.Background = new SolidColorBrush(Color.FromRgb(255, 230, 230));
            err.Text = mesaj;
            err.Visibility = Visibility.Visible;
            tb.Focus();
        }
    }
}
