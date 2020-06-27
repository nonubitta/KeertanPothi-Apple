using DBTest.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Newtonsoft.Json;
using Acr.UserDialogs;

namespace KeertanPothi.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class KirtanPothiList : ContentPage
    {
        SQLiteAsyncConnection _con;
        ObservableCollection<Pothi> PothiObs;
        public KirtanPothiList()
        {
            BindingContext = new Theme();
            _con = DependencyService.Get<ISqliteDb>().GetSQLiteConnection();
            InitializeComponent();
        }

        private async void LoadPothis()
        {
            List<Pothi> pothis = await _con.QueryAsync<Pothi>(Queries.GetAllPothis());
            foreach (Pothi p in pothis)
            {
                List<PothiShabad> ps = await _con.QueryAsync<PothiShabad>($"Select * from PothiShabad where PothiId = {p.PothiId}");
                p.ShabadCount = ps.Count;
            }
            PothiObs = new ObservableCollection<Pothi>(pothis);
            lstPothi.ItemsSource = PothiObs;
            if(PothiObs.Count == 0)
            {
                lblNoPothi.IsVisible = true;
                lstPothi.IsVisible = false;
            }
            else
            {
                lstPothi.IsVisible = true;
                lblNoPothi.IsVisible = false;
            }

        }

        private async void lstPothi_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
                return;
            Pothi pothi = e.SelectedItem as Pothi;
            await Navigation.PushAsync(new PothiShabadList(pothi));
            lstPothi.SelectedItem = null;
        }

        protected override void OnAppearing()
        {
            LoadPothis();
            Theme theme = new Theme();
            PothiObs?.ToList().ForEach(a => a.PageBgTheme = theme);
            BindingContext = theme;
        }

        private async void DeletePothi_Clicked(object sender, EventArgs e)
        {
            Pothi pothi = (sender as MenuItem).CommandParameter as Pothi;
            bool answer = await DisplayAlert("Delete", $"Are you sure you want to delete {pothi.Name} ?", "Yes", "No");
            if (answer)
            {
                try
                {
                    int deletePothiShabads = await _con.ExecuteAsync(Queries.DeletePothiShabad(pothi.PothiId.Value));
                    int deleted = await _con.DeleteAsync<Pothi>(pothi.PothiId);
                    if (deleted > 0 || pothi.PothiId == null)
                    {
                        PothiObs.Remove(pothi);
                        Util.ShowRoast("Pothi deleted successfully");
                    }
                }
                catch (Exception ex)
                {
                    Util.ShowRoast("Failed to Delete pothi");
                }
            }
        }

        private async void AddPothi_Clicked(object sender, EventArgs e)
        {
            string pothiName = await DisplayPromptAsync("Add new Pothi", "Enter pothi name");
            if (!string.IsNullOrWhiteSpace(pothiName))
            {
                Pothi pothi = new Pothi();
                pothi.Name = pothiName;
                pothi.CreatedOn = DateTime.Now;
                var cnt = await _con.InsertAsync(pothi);
                if (cnt > 0)
                {
                    PothiObs.Add(pothi);
                }
                Util.ShowRoast("Pothi added successfully");
                LoadPothis();
            }
        }

        private void Rename_Clicked(object sender, EventArgs e)
        {
            Pothi pothi = (sender as MenuItem).CommandParameter as Pothi;
            RenamePothi(pothi);
        }

        private async void RenamePothi(Pothi pothi)
        {
            string newPothiName = await DisplayPromptAsync("Rename Pothi", "Enter new pothi name", "Save", "Cancel", initialValue: pothi.Name);
            if(!string.IsNullOrWhiteSpace(newPothiName) && newPothiName != pothi.Name)
            {
                pothi.Name = newPothiName;
                int updated = await _con.UpdateAsync(pothi);
                if (updated > 0)
                {
                    PothiObs[PothiObs.IndexOf(pothi)].Name = newPothiName;
                }
            }
        }

        private async void Export_Clicked(object sender, EventArgs e)
        {
            if(PothiObs.Count > 0)
            {
                bool exported = await Queries.ExportPothis(PothiObs.ToList());
                if (exported)
                    Util.ShowRoast("Pothi(s) exported successfully to Internal Storage/KeertanPothi/Pothis.json.");
                else
                    Util.ShowRoast("Failed to export pothis");
            }
            else
                Util.ShowRoast("No pothis to export");
        }

        private async void Import_Clicked(object sender, EventArgs e)
        {
            List<string> jsons = Util.ReadFile();

            if (jsons != null && jsons.Count > 0)
            {
                bool ans = await DisplayAlert("Import pothis", "Pothis found at Internal Storage/KeertanPothi/Pothis.json. Do you want to import them?", "Yes", "No");
                if (ans)
                {
                    bool exp = await Queries.ImportPothi();
                    if (exp)
                    {
                        Util.ShowRoast("Pothi imported successfully.");
                        LoadPothis();
                    }
                }
            }
            else
                Util.ShowRoast("No file found");
        }

        private async void Backup_Clicked(object sender, EventArgs e)
        {
            await DisplayAlert("Backup", "To back up your pothi, select Google Drive or any other cloud service", "OK");
            try
            {
                List<PothiShabadExt> pothis = new List<PothiShabadExt>();
                foreach (Pothi pothi in PothiObs)
                {
                    List<PothiShabad> ps = await _con.QueryAsync<PothiShabad>($"Select * from PothiShabad where PothiId = {pothi.PothiId}");
                    PothiShabadExt ext = new PothiShabadExt() { pothi = pothi, shabadList = ps };
                    pothis.Add(ext);
                }
                string json = JsonConvert.SerializeObject(pothis);
                Util.ShareFile(json,"Pothis.json", "Pothi shared from Keertan Pothi");
            }
            catch (Exception exception)
            {
                Util.ShowRoast("Unable to export pothis");
            }
        }
        
        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            Pothi pothi = (e as TappedEventArgs).Parameter as Pothi;
            await Navigation.PushAsync(new PothiShabadList(pothi));
            lstPothi.SelectedItem = null;
        }

        private void SwipeView_SwipeStarted(object sender, SwipeStartedEventArgs e)
        {
            DisplayAlert("started", "started", "Cancel");
        }

        private void SwipeView_SwipeEnded(object sender, SwipeEndedEventArgs e)
        {
            DisplayAlert("ended", "ended", "Cancel");
        }
    }
}