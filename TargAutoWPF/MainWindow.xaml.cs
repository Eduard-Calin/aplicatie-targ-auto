using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data; 
using System.Windows.Media;
using LibrarieModele;
using NivelStocareDate;

namespace TargAutoWPF
{
    public partial class MainWindow : Window
    {
        public ObservableCollection<Tranzactie> TranzactiiOC { get; set; } = new ObservableCollection<Tranzactie>();

        
        public FormularTranzactieViewModel FormVM { get; set; } = new FormularTranzactieViewModel();

        private Tranzactie? tranzactieSelectata;
        private List<string> bazaClientiAdaugati = new List<string>();
        private const string FisierDate = "date_salvate.txt";

        public MainWindow()
        {
            InitializeComponent();
            IncarcaDinFisierLocal();

            this.DataContext = this;
            dpData.SelectedDate = DateTime.Today;

            ExtrageClientiiDinTranzactii();
            IncarcaClientiInTabel();

            FormVM.ModelMasina = "Model mașină...";
            FormVM.PretMasina = "Preț (€)...";
        }

        private void IncarcaDinFisierLocal()
        {
            TranzactiiOC.Clear();
            if (File.Exists(FisierDate))
            {
                try
                {
                    string[] linii = File.ReadAllLines(FisierDate);
                    foreach (string linie in linii)
                    {
                        string[] d = linie.Split('|');
                        if (d.Length >= 7)
                        {
                            var vanzator = new Persoana(d[0]);
                            var cumparator = new Persoana(d[1]);
                            string model = d[2];
                            double pret = double.Parse(d[3]);

                            Enum.TryParse(d[4], out CuloareVehicul culoare);
                            Enum.TryParse(d[5], out OptiuniVehicul optiuni);
                            DateTime dataTr = DateTime.Parse(d[6]);

                            Vehicul v = new Vehicul("Auto", model, 2024, pret, culoare, optiuni);
                            Tranzactie t = new Tranzactie(vanzator, cumparator, v, dataTr);
                            TranzactiiOC.Add(t);
                        }
                    }
                }
                catch { MessageBox.Show("Eroare la citirea datelor salvate anterioare."); }
            }
        }

        private void SalveazaInFisierLocal()
        {
            try
            {
                List<string> linii = new List<string>();
                foreach (var t in TranzactiiOC)
                {
                    string linie = $"{t.Vanzator.Nume}|{t.Cumparator.Nume}|{t.Vehicul.Model}|{t.Vehicul.Pret}|{t.Vehicul.Culoare}|{t.Vehicul.Optiuni}|{t.DataTranzactiei:yyyy-MM-dd}";
                    linii.Add(linie);
                }
                File.WriteAllLines(FisierDate, linii);
            }
            catch (Exception ex) { MessageBox.Show("Datele nu au putut fi salvate: " + ex.Message); }
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            SalveazaInFisierLocal();
        }

        private void ExtrageClientiiDinTranzactii()
        {
            bazaClientiAdaugati.Clear();
            foreach (var t in TranzactiiOC)
            {
                if (!bazaClientiAdaugati.Contains(t.Cumparator.Nume))
                    bazaClientiAdaugati.Add(t.Cumparator.Nume);
                if (!bazaClientiAdaugati.Contains(t.Vanzator.Nume))
                    bazaClientiAdaugati.Add(t.Vanzator.Nume);
            }
        }

        private void IncarcaClientiInTabel()
        {
            dgClienti.ItemsSource = null;
            dgClienti.ItemsSource = bazaClientiAdaugati.OrderBy(c => c).ToList();
        }

        private void dgClienti_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgClienti.SelectedItem is string numeClientSelectat)
            {
                lblMasiniClient.Text = $"Istoric tranzacții pentru: {numeClientSelectat}";
                var istoric = new List<IstoricTranzactieClient>();

                foreach (var t in TranzactiiOC)
                {
                    if (t.Vanzator.Nume.Equals(numeClientSelectat, StringComparison.OrdinalIgnoreCase))
                        istoric.Add(new IstoricTranzactieClient { Rol = "Vânzător", ModelMasina = t.Vehicul.Model, Pret = t.Vehicul.Pret, DataTranzactiei = t.DataTranzactiei });

                    if (t.Cumparator.Nume.Equals(numeClientSelectat, StringComparison.OrdinalIgnoreCase))
                        istoric.Add(new IstoricTranzactieClient { Rol = "Cumpărător", ModelMasina = t.Vehicul.Model, Pret = t.Vehicul.Pret, DataTranzactiei = t.DataTranzactiei });
                }
                dgMasiniClient.ItemsSource = istoric;
            }
        }

        private void btnAdaugaClient_Click(object sender, RoutedEventArgs e)
        {
            string numeClientNou = txtNumeClient.Text.Trim();

            if (string.IsNullOrEmpty(numeClientNou) || numeClientNou == txtNumeClient.Tag?.ToString())
            {
                MessageBox.Show("Te rog să introduci un nume valid pentru client!");
                return;
            }

            bool existaDeja = bazaClientiAdaugati.Any(c => c.Equals(numeClientNou, StringComparison.OrdinalIgnoreCase));

            if (existaDeja)
            {
                MessageBox.Show($"Clientul '{numeClientNou}' există deja în baza de date!", "Duplicat", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                bazaClientiAdaugati.Add(numeClientNou);
                MessageBox.Show("Client salvat cu succes!", "Succes", MessageBoxButton.OK, MessageBoxImage.Information);
                ResetareGhostText(txtNumeClient);
                IncarcaClientiInTabel();
            }
        }

        private void RemoveGhostText(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox tb && tb.Text == tb.Tag?.ToString())
            {
                tb.Text = "";
                tb.Foreground = Brushes.White;
            }
        }

        private void AddGhostText(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox tb && string.IsNullOrWhiteSpace(tb.Text))
            {
                tb.Text = tb.Tag?.ToString() ?? "";
                tb.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#94A3B8"));
            }
        }

        private void btnAdauga_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string vanzatorText = (txtVanzator.Text == txtVanzator.Tag?.ToString()) ? "" : txtVanzator.Text.Trim();
                if (string.IsNullOrEmpty(vanzatorText)) { MessageBox.Show("Eroare: Nu ai introdus numele VÂNZĂTORULUI!", "Câmp obligatoriu", MessageBoxButton.OK, MessageBoxImage.Warning); return; }

                string cumparatorText = (txtCumparator.Text == txtCumparator.Tag?.ToString()) ? "" : txtCumparator.Text.Trim();
                if (string.IsNullOrEmpty(cumparatorText)) { MessageBox.Show("Eroare: Nu ai introdus numele CUMPĂRĂTORULUI!", "Câmp obligatoriu", MessageBoxButton.OK, MessageBoxImage.Warning); return; }

                if (!FormVM.EsteValid)
                {
                    MessageBox.Show("Există erori de validare în formular! Verificați câmpurile marcate cu roșu.", "Validare MVVM", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (dpData.SelectedDate == null) { MessageBox.Show("Eroare: Nu ai selectat DATA tranzacției!", "Câmp obligatoriu", MessageBoxButton.OK, MessageBoxImage.Warning); return; }

                double.TryParse(FormVM.PretMasina, out double pret);

                OptiuniVehicul optiuni = OptiuniVehicul.Standard;
                if (chkAC.IsChecked == true) optiuni |= OptiuniVehicul.AerConditionat;
                if (chkNavi.IsChecked == true) optiuni |= OptiuniVehicul.Navigatie;
                if (chkAuto.IsChecked == true) optiuni |= OptiuniVehicul.CutieAutomata;
                if (chkDecapotabila.IsChecked == true) optiuni |= OptiuniVehicul.Decapotabila;
                if (chk4x4.IsChecked == true) optiuni |= OptiuniVehicul.Tractiune4x4;
                if (chkGeamuri.IsChecked == true) optiuni |= OptiuniVehicul.GeamuriElectrice;

                CuloareVehicul col = CuloareVehicul.Alb;
                if (cmbCuloare.SelectedItem is ComboBoxItem selectieCuloare)
                {
                    string numeCuloare = selectieCuloare.Content?.ToString() ?? "Alb";
                    if (numeCuloare == "Negru") col = CuloareVehicul.Negru;
                    else if (numeCuloare == "Rosu") col = CuloareVehicul.Rosu;
                }

                Vehicul v = new Vehicul("Auto", FormVM.ModelMasina, 2024, pret, col, optiuni);
                Tranzactie noua = new Tranzactie(new Persoana(vanzatorText), new Persoana(cumparatorText), v, dpData.SelectedDate.Value);

                TranzactiiOC.Add(noua);

                if (!bazaClientiAdaugati.Contains(vanzatorText)) bazaClientiAdaugati.Add(vanzatorText);
                if (!bazaClientiAdaugati.Contains(cumparatorText)) bazaClientiAdaugati.Add(cumparatorText);

                ResetareCampuri();
                IncarcaClientiInTabel();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void btnModifica_Click(object sender, RoutedEventArgs e)
        {
            if (tranzactieSelectata == null)
            {
                MessageBox.Show("Te rog să selectezi o tranzacție din tabel pentru a o putea modifica!", "Selectează", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            try
            {
                string vanzatorText = (txtVanzator.Text == txtVanzator.Tag?.ToString()) ? "" : txtVanzator.Text.Trim();
                if (string.IsNullOrEmpty(vanzatorText)) { MessageBox.Show("Eroare: VÂNZĂTORUL nu poate fi lăsat gol!", "Eroare", MessageBoxButton.OK, MessageBoxImage.Warning); return; }

                string cumparatorText = (txtCumparator.Text == txtCumparator.Tag?.ToString()) ? "" : txtCumparator.Text.Trim();
                if (string.IsNullOrEmpty(cumparatorText)) { MessageBox.Show("Eroare: CUMPĂRĂTORUL nu poate fi lăsat gol!", "Eroare", MessageBoxButton.OK, MessageBoxImage.Warning); return; }

                if (!FormVM.EsteValid)
                {
                    MessageBox.Show("Datele introduse pentru model sau preț sunt invalide!", "Validare MVVM", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                double.TryParse(FormVM.PretMasina, out double pret);

                tranzactieSelectata.Vanzator.Nume = vanzatorText;
                tranzactieSelectata.Cumparator.Nume = cumparatorText;
                tranzactieSelectata.Vehicul.Model = FormVM.ModelMasina;
                tranzactieSelectata.Vehicul.Pret = pret;
                tranzactieSelectata.DataTranzactiei = dpData.SelectedDate ?? DateTime.Now;

                OptiuniVehicul optiuni = OptiuniVehicul.Standard;
                if (chkAC.IsChecked == true) optiuni |= OptiuniVehicul.AerConditionat;
                if (chkNavi.IsChecked == true) optiuni |= OptiuniVehicul.Navigatie;
                if (chkAuto.IsChecked == true) optiuni |= OptiuniVehicul.CutieAutomata;
                if (chkDecapotabila.IsChecked == true) optiuni |= OptiuniVehicul.Decapotabila;
                if (chk4x4.IsChecked == true) optiuni |= OptiuniVehicul.Tractiune4x4;
                if (chkGeamuri.IsChecked == true) optiuni |= OptiuniVehicul.GeamuriElectrice;
                tranzactieSelectata.Vehicul.Optiuni = optiuni;

                if (cmbCuloare.SelectedItem is ComboBoxItem selectieCuloare)
                {
                    string numeCuloare = selectieCuloare.Content?.ToString() ?? "Alb";
                    if (numeCuloare == "Negru") tranzactieSelectata.Vehicul.Culoare = CuloareVehicul.Negru;
                    else if (numeCuloare == "Rosu") tranzactieSelectata.Vehicul.Culoare = CuloareVehicul.Rosu;
                    else tranzactieSelectata.Vehicul.Culoare = CuloareVehicul.Alb;
                }

                dgTranzactii.Items.Refresh();
                MessageBox.Show("Tranzacția a fost modificată cu succes!", "Succes", MessageBoxButton.OK, MessageBoxImage.Information);
                ResetareCampuri();
            }
            catch (Exception ex) { MessageBox.Show("Eroare la modificare: " + ex.Message); }
        }

        private void btnSterge_Click(object sender, RoutedEventArgs e)
        {
            if (tranzactieSelectata != null)
            {
                TranzactiiOC.Remove(tranzactieSelectata);
                tranzactieSelectata = null;
                ResetareCampuri();
            }
        }

        // ====================================================================
        // FILTRARE CORECTĂ PRIN VIEW - REZOLVAREA BUG-ULUI DE ȘTERGERE
        // ====================================================================
        private void txtCauta_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtCauta.Text == txtCauta.Tag?.ToString()) return;
            string s = txtCauta.Text?.ToLower() ?? "";

           
            ICollectionView view = CollectionViewSource.GetDefaultView(TranzactiiOC);

            if (string.IsNullOrWhiteSpace(s))
            {
                view.Filter = null; // Afișează toate mașinile din nou
            }
            else
            {
                view.Filter = obj =>
                {
                    if (obj is Tranzactie t)
                    {
                        return t.Vehicul.Model.ToLower().Contains(s);
                    }
                    return false;
                };
            }
        }

        private void dgTranzactii_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            tranzactieSelectata = dgTranzactii.SelectedItem as Tranzactie;
            if (tranzactieSelectata != null)
            {
                SetTextReal(txtVanzator, tranzactieSelectata.Vanzator.Nume);
                SetTextReal(txtCumparator, tranzactieSelectata.Cumparator.Nume);

                FormVM.ModelMasina = tranzactieSelectata.Vehicul.Model;
                FormVM.PretMasina = tranzactieSelectata.Vehicul.Pret.ToString();
                txtModel.Foreground = Brushes.White;
                txtPret.Foreground = Brushes.White;

                dpData.SelectedDate = tranzactieSelectata.DataTranzactiei;

                chkAC.IsChecked = tranzactieSelectata.Vehicul.Optiuni.HasFlag(OptiuniVehicul.AerConditionat);
                chkNavi.IsChecked = tranzactieSelectata.Vehicul.Optiuni.HasFlag(OptiuniVehicul.Navigatie);
                chkAuto.IsChecked = tranzactieSelectata.Vehicul.Optiuni.HasFlag(OptiuniVehicul.CutieAutomata);
                chkDecapotabila.IsChecked = tranzactieSelectata.Vehicul.Optiuni.HasFlag(OptiuniVehicul.Decapotabila);
                chk4x4.IsChecked = tranzactieSelectata.Vehicul.Optiuni.HasFlag(OptiuniVehicul.Tractiune4x4);
                chkGeamuri.IsChecked = tranzactieSelectata.Vehicul.Optiuni.HasFlag(OptiuniVehicul.GeamuriElectrice);

                if (tranzactieSelectata.Vehicul.Culoare == CuloareVehicul.Alb) cmbCuloare.SelectedIndex = 0;
                else if (tranzactieSelectata.Vehicul.Culoare == CuloareVehicul.Negru) cmbCuloare.SelectedIndex = 1;
                else if (tranzactieSelectata.Vehicul.Culoare == CuloareVehicul.Rosu) cmbCuloare.SelectedIndex = 2;
            }
        }

        private void Menu_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn)
            {
                TabMain.SelectedIndex = int.Parse(btn.Tag?.ToString() ?? "0");

                if (TabMain.SelectedIndex == 1) IncarcaClientiInTabel();

                foreach (var child in pnlMeniu.Children)
                    if (child is Button b) b.Background = Brushes.Transparent;
                btn.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#38BDF8"));
            }
        }

        private void ResetareCampuri()
        {
            ResetareGhostText(txtVanzator);
            ResetareGhostText(txtCumparator);

            FormVM.ModelMasina = "Model mașină...";
            FormVM.PretMasina = "Preț (€)...";
            txtModel.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#94A3B8"));
            txtPret.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#94A3B8"));

            cmbCuloare.SelectedIndex = 0;
            chkAC.IsChecked = chkNavi.IsChecked = chkAuto.IsChecked = false;
            chkDecapotabila.IsChecked = chk4x4.IsChecked = chkGeamuri.IsChecked = false;
            dpData.SelectedDate = DateTime.Today;
        }

        private void ResetareGhostText(TextBox tb)
        {
            tb.Text = tb.Tag?.ToString();
            tb.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#94A3B8"));
        }

        private void SetTextReal(TextBox tb, string text)
        {
            tb.Text = text;
            tb.Foreground = Brushes.White;
        }
    }

    public class IstoricTranzactieClient
    {
        public string Rol { get; set; } = string.Empty;
        public string ModelMasina { get; set; } = string.Empty;
        public double Pret { get; set; }
        public DateTime DataTranzactiei { get; set; }
    }

    public class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public class FormularTranzactieViewModel : ViewModelBase, IDataErrorInfo
    {
        private string _modelMasina = string.Empty;
        public string ModelMasina
        {
            get => _modelMasina;
            set
            {
                _modelMasina = value;
                OnPropertyChanged(nameof(ModelMasina));
                OnPropertyChanged(nameof(EsteValid));
            }
        }

        private string _pretMasina = string.Empty;
        public string PretMasina
        {
            get => _pretMasina;
            set
            {
                _pretMasina = value;
                OnPropertyChanged(nameof(PretMasina));
                OnPropertyChanged(nameof(EsteValid));
            }
        }

        public bool EsteValid => string.IsNullOrEmpty(this[nameof(ModelMasina)]) && string.IsNullOrEmpty(this[nameof(PretMasina)]);

        public string Error => string.Empty;

        public string this[string columnName]
        {
            get
            {
                if (columnName == nameof(ModelMasina))
                {
                    if (string.IsNullOrWhiteSpace(ModelMasina) || ModelMasina == "Model mașină...")
                        return "Modelul autovehiculului este obligatoriu!";
                }
                else if (columnName == nameof(PretMasina))
                {
                    if (string.IsNullOrWhiteSpace(PretMasina) || PretMasina == "Preț (€)...")
                        return "Prețul autovehiculului este obligatoriu!";
                    if (!double.TryParse(PretMasina, out double pret) || pret <= 0)
                        return "Prețul trebuie să fie un număr valid mai mare decât 0!";
                }

                return string.Empty;
            }
        }
    }
}