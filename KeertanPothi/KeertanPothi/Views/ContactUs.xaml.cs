using DBTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace KeertanPothi.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ContactUs : TabbedPage
    {
        public ContactUs()
        {
            BindingContext = new Theme();
            InitializeComponent();
        }

 
        private void btnSend_Clicked(object sender, System.EventArgs e)
        {
            try
            {
                var client = new SmtpClient("smtp.gmail.com", 587)
                {
                    Credentials = new NetworkCredential("KeertanPothi@gmail.com", "KeertanPothi1699"),
                    EnableSsl = true
                };
                client.Send("KeertanPothi@gmail.com", "nonubitta@gmail.com", txtSubject.Text, txtEmail.Text + ": " + txtBody.Text);
                Util.ShowRoast("Message sent !!!");
            }
            catch (Exception ex)
            {
                Util.ShowRoast("Failed to send message");
            }
        }

        private void exp1_Tapped(object sender, EventArgs e)
        {
            scrMain.ScrollToAsync(0, 0, true);
            if (exp1.IsExpanded)
            {
                lbl1.Text = "↑";
                exp2.IsExpanded = false;
                exp3.IsExpanded = false;
                lbl3.Text = "↓";
                lbl2.Text = "↓";
            }
            else
                lbl1.Text = "↓";
        }

        private void exp2_Tapped(object sender, EventArgs e)
        {
            if (exp2.IsExpanded) { 
                lbl2.Text = "↑";
                exp1.IsExpanded = false;
                exp3.IsExpanded = false;
                lbl1.Text = "↓";
                lbl3.Text = "↓";
            }
            else
                lbl2.Text = "↓";
        }

        private void exp3_Tapped(object sender, EventArgs e)
        {
            if (exp3.IsExpanded) { 
                lbl3.Text = "↑";
                exp1.IsExpanded = false;
                exp2.IsExpanded = false;
                lbl1.Text = "↓";
                lbl2.Text = "↓";
            }
            else
                lbl3.Text = "↓";
        }

        private async void TapGestureRecognizer_Punjabi(object sender, EventArgs e)
        {
            await Launcher.OpenAsync("https://youtu.be/jsny2dOcLjU");
        }

        private async void TapGestureRecognizer_English(object sender, EventArgs e)
        {
            await Launcher.OpenAsync("https://youtu.be/HVJgWXapYiA");
        }

        private async void ImageButton_Clicked(object sender, EventArgs e)
        {
            await Launcher.OpenAsync("https://youtu.be/jsny2dOcLjU");
        }
    }
}
