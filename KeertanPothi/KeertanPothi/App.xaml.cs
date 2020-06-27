using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;
using System.Threading.Tasks;

[assembly: ExportFont("gurbaniwebthick.ttf", Alias = "PunjabiBoldFont")]
[assembly: ExportFont("PunjabiFont1.otf", Alias = "PunjabiFont")]
[assembly: ExportFont("HandFont3.otf", Alias = "HandFont")]
[assembly: ExportFont("Gurhindi.ttf", Alias = "HindiFont")]
namespace KeertanPothi
{
    public partial class App : Application
    {
        public App()
        {
            DeviceDisplay.KeepScreenOn = true;
            InitializeComponent();
            Device.SetFlags(new[]
            {
                "SwipeView_Experimental",
                "CarouselView_Experimental",
                "IndicatorView_Experimental",
                "Expander_Experimental"
            });
            //VersionTracking.IsFirstLaunchEver;
            //VersionTracking.IsFirstLaunchForCurrentVersion;

            if (!Preferences.Get(Util.PrefDataExistsKey, false))
            {
                CopyDB.CopyDBFile(true);
                Util.SetInitPreferences();
            }
            else
            {
                int appDbVersion = Util.PrefCurrentDbVersion;
                if (appDbVersion < Util.CurrentDbVersion)
                {
                    CopyDB.CopyDBFile(false);
                    Util.PrefCurrentDbVersion = Util.CurrentDbVersion;
                }
            }
            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
