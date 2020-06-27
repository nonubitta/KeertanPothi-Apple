using Acr.UserDialogs;
using DBTest.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace KeertanPothi.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Search : ContentPage
    {
        SQLiteAsyncConnection _con;
		ObservableCollection<VerseSearch> versesObs;
		public Search()
        {
			BindingContext = new Theme();
			_con = DependencyService.Get<ISqliteDb>().GetSQLiteConnection();
            InitializeComponent();
			//LoadHistory();
        }

		//private async void LoadHistory()
		//{
		//	List<VerseSearch> verseSearch = await _con.QueryAsync<VerseSearch>(Queries.GetShabadHistory(5));
		//	lstHistory.ItemsSource = verseSearch;
		//}

		protected override void OnAppearing()
		{
			Theme theme = new Theme();
			versesObs?.ToList().ForEach(a => a.PageBgTheme = theme);
			BindingContext = theme;

			base.OnAppearing();
			SetSearchOptions();
			ToggleKeyboard(true);
		}

		private async void SearchBar_TextChanged()
		{
			//using (UserDialogs.Instance.Loading("Retrieving Shabad list..."))
			//{
			string search = txtSearch.Text;// e.NewTextValue;
											//int a = Convert.ToChar(search);
			if (search == null || search.Length < 3)
			{
				lstVerse.ItemsSource = null;
				lblTotalRows.Text = "";
				slCount.IsVisible = false;
				slList.IsVisible = false;
				slLogo.IsVisible = true;
				return;
			}
			//if(!Regex.IsMatch(search, @"^[a-zA-Z]+$"))
			//{
			//	//ੳਅੲਸਹਕਖਗਘਙਚਛਜਝਞਟਠਡਢਣਤਥਦਧਨਪਫਬਭਮਯਰਲਵੜਸ਼
			//	search = Util.ReplacePunjabiUnicode(search);
			//}
			string query = string.Empty;
			KeyValue searchItem = pckSearchType.SelectedItem as KeyValue;
			if(searchItem != null)
			{
				string searchType = searchItem.Key;
				switch (searchType)
				{
					case "E":
						query = Queries.ExactSearch(search);
						break;
					case "M":
						query = Queries.MainLetterSearch(search);
						break;
				default:
						string asciiSearch = ",";
						foreach (char c in search)
						{
							string str = Convert.ToInt32(c).ToString().PadLeft(3, '0');
							asciiSearch += str + ",";
						}
						query = Queries.SearchQ(asciiSearch);
						break;
				}
			}
				
			List<VerseSearch> verseSearch = await _con.QueryAsync<VerseSearch>(query);
			versesObs = new ObservableCollection<VerseSearch>(verseSearch);
			lstVerse.ItemsSource = versesObs;
			lblTotalRows.Text = $"{versesObs.Count} shabad(s) found";
			slCount.IsVisible = true;
			slList.IsVisible = true;
			slLogo.IsVisible = false;
		}

		private async void lstVerse_ItemSelected(object sender, SelectedItemChangedEventArgs e)
		{
			if (e.SelectedItem == null)
				return;
			VerseSearch verse = e.SelectedItem as VerseSearch;
			Shabad shabad = await _con.Table<Shabad>().FirstOrDefaultAsync(a => a.VerseID == verse.VerseID);
			await Navigation.PushAsync(new ShabadDetails(shabad.ShabadID, verse.VerseID));
			lstVerse.SelectedItem = null;
		}

		private void Akhar_Clicked(object sender, EventArgs e)
		{
			Button button = sender as Button;
			if (button.CommandParameter != null)
			{
				switch(button.CommandParameter.ToString())
				{
					case "B": 
						if(!string.IsNullOrWhiteSpace(txtSearch.Text))
							txtSearch.Text = txtSearch.Text.Remove(txtSearch.Text.Length - 1, 1);
						break;
					case "S":
						txtSearch.Text += " ";
						break;
					case "N":
						controlGrid2.IsVisible = !controlGrid2.IsVisible;
						controlGrid3.IsVisible = !controlGrid3.IsVisible;
						break;
					case "F": ToggleKeyboard(false);
						return;
				}
			}
			else
			{
				txtSearch.Text += button.Text;
			}
			SearchBar_TextChanged();
		}

		private async void ToggleKeyboard(bool visible, bool force=false)
		{
			if (!visible)
			{
				if (!string.IsNullOrEmpty(txtSearch.Text) || force)
				{
					await EditToolbar.TranslateTo(0, 250, 50, Easing.SinOut);
					await Task.Delay(50);
					EditToolbar.IsVisible = false;
				}
			}
			else 
			{
				EditToolbar.IsVisible = true;
				await EditToolbar.TranslateTo(0, 0, 50, Easing.SinOut);
			}
		}

		private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
		{
			ToggleKeyboard(true);
		}

		private void Clear_Clicked(object sender, EventArgs e)
		{
			txtSearch.Text = string.Empty;
			ToggleKeyboard(true);
			SearchBar_TextChanged();
		}

		private void SetSearchOptions()
		{
			List<KeyValue> searchOptions = new List<KeyValue>();
			searchOptions.Add(new KeyValue { Value = "First Letter Search", Key = "F" });
			searchOptions.Add(new KeyValue { Value = "Main Letter Search", Key = "M" });
			searchOptions.Add(new KeyValue { Value = "Exact Search", Key = "E" });
			searchOptions.Add(new KeyValue { Value = "Ang Search", Key = "A" });
			pckSearchType.ItemsSource = searchOptions;
			pckSearchType.SelectedIndex = 0;
			pckSearchType2.ItemsSource = searchOptions;
		}

		private async void pckSearchType_SelectedIndexChanged(object sender, EventArgs e)
		{
			KeyValue keyValue1 = pckSearchType.SelectedItem as KeyValue;
			if (keyValue1 != null)
			{
				if (keyValue1.Key == "F")
				{
					btnSpace.IsEnabled = false;
					btnNxtKeyboard.IsEnabled = false;
				}
				else
				{
					btnSpace.IsEnabled = true;
					btnNxtKeyboard.IsEnabled = true;
				}

				if (keyValue1.Key == "A")
				{
					ToggleKeyboard(false, true);
					string angNoStr = await DisplayPromptAsync("Ang Number", "Enter ang number:", "OK", "Cancel", maxLength: 4, keyboard: Keyboard.Numeric);
					if (!string.IsNullOrWhiteSpace(angNoStr))
					{
						int angNo = Convert.ToInt32(angNoStr);
						await Navigation.PushAsync(new ShabadDetails(angNo));
					}
					else
					{
						pckSearchType.SelectedIndex = 0;
						ToggleKeyboard(true);
					}
				}
			}
		}

		private void History_Clicked(object sender, EventArgs e)
		{
			Navigation.PushAsync(new History());
		}

		private void Help_Clicked(object sender, EventArgs e)
		{
			Navigation.PushAsync(new ContactUs());
		}
	}
}