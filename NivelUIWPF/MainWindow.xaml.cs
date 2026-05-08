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

        // optiuni finantare pt ListBox (lab 9)
        private static readonly string[] OPTIUNI_FINANTARE = { "Disponibil", "Inchiriat", "Vandut" };

        // facilitati pt ListBox multi-select din modificare (lab 9)
        private static readonly FacilitatiApartament[] OPTIUNI_FACILITATI =
        {
            FacilitatiApartament.Balcon,
            FacilitatiApartament.Parcare,
            FacilitatiApartament.CentralaTermica,
            FacilitatiApartament.AerConditionat,
            FacilitatiApartament.LocDeJoaca
        };

        private IStocareData adminApartamente;

        public MainWindow()
        {
            InitializeComponent();
            adminApartamente = StocareFactory.GetAdministratorStocare();
            InitializeComboBox();
            InitializeControaleLab9();
            AfiseazaApartamente(adminApartamente.GetApartamente());
            ActualizeazaListBoxFacilitati();
        }

        // populeaza ListBox-urile noi si seteaza data implicita (lab 9)
        private void InitializeControaleLab9()
        {
            // ListBox finantare la adaugare
            lbFinantareAdd.ItemsSource = OPTIUNI_FINANTARE;
            lbFinantareAdd.SelectedIndex = 0;

            // ListBox finantare la modificare
            lbFinantareMod.ItemsSource = OPTIUNI_FINANTARE;

            // ListBox facilitati la modificare (afiseaza numele enum-urilor)
            lbFacilitatiMod.ItemsSource = OPTIUNI_FACILITATI;

            // DatePicker la adaugare - default azi
            dpDataAdaugare.SelectedDate = DateTime.Today;
        }

        // Initializeaza ComboBox si atribuie event handler
        private void InitializeComboBox()
        {
            cbTipApartament.SelectionChanged += CbTipApartament_SelectionChanged;
        }

        // Event handler pentru schimbarea selectiei din ComboBox
        private void CbTipApartament_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int selectedIndex = cbTipApartament.SelectedIndex;
            
            if (selectedIndex == 0) // "Toate tipurile" - afiseaza toti apartamentele
            {
                AfiseazaApartamente(adminApartamente.GetApartamente());
            }
            else
            {
                // Converteste indexul selectat in TipApartament enum
                TipApartament tip = (TipApartament)selectedIndex;
                var filtrate = adminApartamente.GetApartamente()
                    .Where(a => a.Tip == tip)
                    .ToList();
                AfiseazaApartamente(filtrate);
            }
            
            // Refresh ListBox-ul dupa filtrare
            ActualizeazaListBoxFacilitati();
        }

        // Actualizeaza ListBox-ul cu apartamentele care au facilitati
        private void ActualizeazaListBoxFacilitati()
        {
            // Sterge toate elementele din ListBox
            lbApartamenteFacilitati.Items.Clear();
            
            // Obtine apartamentele curente afisate (dupa filtrare)
            var apartamente = dgApartamente.ItemsSource as List<Apartament>;
            
            if (apartamente != null)
            {
                // Itereaza prin apartamentele curente
                foreach (var ap in apartamente)
                {
                    // Adauga in ListBox doar apartamentele cu facilitati
                    if (ap.Facilitati != FacilitatiApartament.Niciuna)
                    {
                        string facilitatiText = $"Ap. {ap.Numar}: {ap.Facilitati}";
                        lbApartamenteFacilitati.Items.Add(facilitatiText);
                    }
                }
                
                // Daca nu exista apartamente cu facilitati, afiseaza mesaj
                if (lbApartamenteFacilitati.Items.Count == 0)
                {
                    lbApartamenteFacilitati.Items.Add("Niciun apartament cu facilitati");
                }
            }
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
            // lab 9 - data adaugare din DatePicker, finantare din ListBox
            ap.DataAdaugare = dpDataAdaugare.SelectedDate ?? DateTime.Today;
            ap.TipFinantare = lbFinantareAdd.SelectedItem as string ?? "Disponibil";
            adminApartamente.AddApartament(ap);

            AfiseazaApartamente(adminApartamente.GetApartamente());
            ActualizeazaListBoxFacilitati(); // Refresh ListBox dupa adaugare
            btnReseteaza_Click(null, null); // Reseteaza formularul
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
            // lab 9 - reset si la datepicker + listbox finantare
            dpDataAdaugare.SelectedDate = DateTime.Today;
            lbFinantareAdd.SelectedIndex = 0;
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
                ActualizeazaListBoxFacilitati();
            }
            catch(System.FormatException)
            {
                MessageBox.Show("Pretul trebuie sa fie un numar!", "Eroare", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnResetCautare_Click(object sender, RoutedEventArgs e)
        {
            txtPretMax.Clear();
            cbTipApartament.SelectedIndex = 0; // Reseteaza ComboBox
            AfiseazaApartamente(adminApartamente.GetApartamente());
            ActualizeazaListBoxFacilitati();
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

        // meniu - schimbare intre administrare / cautare / modificare (lab 8 + lab 9)
        private void btnMeniuAdministrare_Click(object sender, RoutedEventArgs e)
        {
            pnlAdministrare.Visibility = Visibility.Visible;
            pnlCautare.Visibility = Visibility.Collapsed;
            pnlModifica.Visibility = Visibility.Collapsed;
        }

        private void btnMeniuCautare_Click(object sender, RoutedEventArgs e)
        {
            pnlAdministrare.Visibility = Visibility.Collapsed;
            pnlCautare.Visibility = Visibility.Visible;
            pnlModifica.Visibility = Visibility.Collapsed;
            // resetez ce era inainte
            lblMesajCautare.Content = "";
            dgRezultatCautare.ItemsSource = null;
            txtCautaNumar.Clear();
            txtCautaNumar.Focus();
        }

        // lab 9 - meniu modifica
        private void btnMeniuModifica_Click(object sender, RoutedEventArgs e)
        {
            pnlAdministrare.Visibility = Visibility.Collapsed;
            pnlCautare.Visibility = Visibility.Collapsed;
            pnlModifica.Visibility = Visibility.Visible;

            // populez ComboBox-ul cu apartamentele existente
            cmbApartamenteModifica.ItemsSource = null;
            cmbApartamenteModifica.ItemsSource = adminApartamente.GetApartamente();
            lblMesajModifica.Content = "";
            ReseteazaFormularModifica();
        }

        private void ReseteazaFormularModifica()
        {
            txtEtajMod.Clear();
            txtSuprafataMod.Clear();
            txtPretMod.Clear();
            rbGarsonieraMod.IsChecked = false;
            rbDouaCamereMod.IsChecked = false;
            rbTreiCamereMod.IsChecked = false;
            rbPatruCamereMod.IsChecked = false;
            rbPenthouseMod.IsChecked = false;
            lbFacilitatiMod.SelectedItems.Clear();
            lbFinantareMod.SelectedIndex = -1;
            dpDataAdaugareMod.SelectedDate = null;
        }

        // lab 9 - cand selectez un apartament din ComboBox, populez formularul
        private void cmbApartamenteModifica_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Apartament ap = cmbApartamenteModifica.SelectedItem as Apartament;
            if(ap == null) return;

            txtEtajMod.Text = ap.Etaj.ToString();
            txtSuprafataMod.Text = ap.Suprafata.ToString();
            txtPretMod.Text = ap.PretChirie.ToString();
            dpDataAdaugareMod.SelectedDate = ap.DataAdaugare;

            // selectez RadioButton-ul potrivit
            switch(ap.Tip)
            {
                case TipApartament.Garsoniera: rbGarsonieraMod.IsChecked = true; break;
                case TipApartament.DoaCamere: rbDouaCamereMod.IsChecked = true; break;
                case TipApartament.TreiCamere: rbTreiCamereMod.IsChecked = true; break;
                case TipApartament.PatruCamere: rbPatruCamereMod.IsChecked = true; break;
                case TipApartament.Penthouse: rbPenthouseMod.IsChecked = true; break;
            }

            // selectez facilitatile in ListBox-ul multi-select
            lbFacilitatiMod.SelectedItems.Clear();
            foreach(FacilitatiApartament f in OPTIUNI_FACILITATI)
            {
                if((ap.Facilitati & f) == f)
                    lbFacilitatiMod.SelectedItems.Add(f);
            }

            // selectez tipul de finantare
            int idxFin = Array.IndexOf(OPTIUNI_FINANTARE, ap.TipFinantare);
            lbFinantareMod.SelectedIndex = idxFin >= 0 ? idxFin : 0;
        }

        // lab 9 - actualizare apartament
        private void btnActualizeaza_Click(object sender, RoutedEventArgs e)
        {
            Apartament apOriginal = cmbApartamenteModifica.SelectedItem as Apartament;
            if(apOriginal == null)
            {
                lblMesajModifica.Foreground = new SolidColorBrush(Color.FromRgb(160, 40, 40));
                lblMesajModifica.Content = "Selectati mai intai un apartament!";
                return;
            }

            // validare campuri
            if(!int.TryParse(txtEtajMod.Text.Trim(), out int etaj) || etaj < 0)
            {
                lblMesajModifica.Foreground = new SolidColorBrush(Color.FromRgb(160, 40, 40));
                lblMesajModifica.Content = "Etajul nu e valid!";
                return;
            }
            if(!double.TryParse(txtSuprafataMod.Text.Trim(), out double suprafata) || suprafata <= 0)
            {
                lblMesajModifica.Foreground = new SolidColorBrush(Color.FromRgb(160, 40, 40));
                lblMesajModifica.Content = "Suprafata nu e valida!";
                return;
            }
            if(!double.TryParse(txtPretMod.Text.Trim(), out double pret) || pret <= 0)
            {
                lblMesajModifica.Foreground = new SolidColorBrush(Color.FromRgb(160, 40, 40));
                lblMesajModifica.Content = "Pretul nu e valid!";
                return;
            }

            TipApartament tip = GetTipSelectatModifica();

            // facilitati din ListBox multi-select
            FacilitatiApartament fac = FacilitatiApartament.Niciuna;
            foreach(var item in lbFacilitatiMod.SelectedItems)
            {
                if(item is FacilitatiApartament f)
                    fac |= f;
            }

            string finantare = lbFinantareMod.SelectedItem as string ?? "Disponibil";
            DateTime dataAd = dpDataAdaugareMod.SelectedDate ?? apOriginal.DataAdaugare;

            // construiesc apartamentul modificat (numarul ramane acelasi)
            Apartament apNou = new Apartament(apOriginal.Numar, etaj, suprafata, pret, tip, fac);
            apNou.DataAdaugare = dataAd;
            apNou.TipFinantare = finantare;

            adminApartamente.ModificaApartament(apNou);

            lblMesajModifica.Foreground = new SolidColorBrush(Color.FromRgb(30, 120, 50));
            lblMesajModifica.Content = $"Apartamentul {apOriginal.Numar} a fost actualizat!";

            // refresh: in ComboBox + datagrid principal
            cmbApartamenteModifica.ItemsSource = null;
            cmbApartamenteModifica.ItemsSource = adminApartamente.GetApartamente();
            AfiseazaApartamente(adminApartamente.GetApartamente());
        }

        private TipApartament GetTipSelectatModifica()
        {
            if(rbDouaCamereMod.IsChecked == true) return TipApartament.DoaCamere;
            if(rbTreiCamereMod.IsChecked == true) return TipApartament.TreiCamere;
            if(rbPatruCamereMod.IsChecked == true) return TipApartament.PatruCamere;
            if(rbPenthouseMod.IsChecked == true) return TipApartament.Penthouse;
            return TipApartament.Garsoniera;
        }

        // cautare apartament dupa numar (lab 8)
        private void btnCautaDupaNumar_Click(object sender, RoutedEventArgs e)
        {
            // resetez culoarea mesajului
            lblMesajCautare.Foreground = new SolidColorBrush(Color.FromRgb(160, 40, 40));

            string s = txtCautaNumar.Text.Trim();
            if(string.IsNullOrEmpty(s))
            {
                lblMesajCautare.Content = "Introduceti un numar de apartament!";
                dgRezultatCautare.ItemsSource = null;
                return;
            }
            if(!int.TryParse(s, out int numar) || numar <= 0)
            {
                lblMesajCautare.Content = "Numarul trebuie sa fie un intreg pozitiv!";
                dgRezultatCautare.ItemsSource = null;
                return;
            }

            Apartament ap = adminApartamente.CautaDupaNumar(numar);
            if(ap == null)
            {
                lblMesajCautare.Content = $"Nu s-a gasit niciun apartament cu numarul {numar}";
                dgRezultatCautare.ItemsSource = null;
            }
            else
            {
                lblMesajCautare.Content = "Apartament gasit:";
                lblMesajCautare.Foreground = new SolidColorBrush(Color.FromRgb(30, 120, 50));
                dgRezultatCautare.ItemsSource = new List<Apartament> { ap };
            }
        }
    }
}
