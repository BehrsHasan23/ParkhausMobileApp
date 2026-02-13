//Behrs Hasan
namespace ParkhausApp
{
    public partial class MainPage : ContentPage
    {
       
        public MainPage()
        {
            InitializeComponent();
        }

        private async void OnStock1ParkenClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Stock1Page());
            
        }
        private async void OnStock2ParkenClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Stock2Page());

        }
        private async void OnExitPageClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ExitPage());

        }
        //Aktuelle Freie Parkplätze 
        //läuft automatisch 
        protected override void OnAppearing()
        {
            base.OnAppearing();
            Free1Label.Text = $"Freie Plätze: {ParkingState.Free1}";
            Free2Label.Text = $"Freie Plätze: {ParkingState.Free2}";

        }

        }
}
