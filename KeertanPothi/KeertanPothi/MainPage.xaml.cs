using DBTest.Models;
using KeertanPothi.model;
using KeertanPothi.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace KeertanPothi
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : MasterDetailPage
    {
        public MainPage()
        {
            InitializeComponent();
            lblVersion.Text = $"Version: {VersionTracking.CurrentVersion} (Db V: {Util.CurrentDbVersion})";
            this.Detail = new NavigationPage(new Search());
            LoadMenu();
        }

        private void LoadMenu()
        {
            Util.PrefIsAdmin = true;
            lstMenu.ItemsSource = Util.GetStaticMenu().Where(a => a.IsVisible);
        }
        protected override void OnAppearing()
        {
            Theme theme = new Theme();
            slFooterBg.BackgroundColor = Color.FromHex(Util.DarkThemeBgColor);
            slMenuBg.BackgroundColor = Color.FromHex(Util.DarkThemeBgColor);
            BindingContext = theme;
            base.OnAppearing();
        }

        private void lstMenu_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
                return;
            SideMenu itemClicked = e.SelectedItem as SideMenu;
            var type = Type.GetType("KeertanPothi.Views." + itemClicked.ViewName);
            Detail = new NavigationPage(Activator.CreateInstance(type) as Page);
            IsPresented = false;
            lstMenu.SelectedItem = null;
        }

        private void Header_Tapped(object sender, EventArgs e)
        {
            Detail = new NavigationPage(new DefaultDetail());
        }
    }
}
