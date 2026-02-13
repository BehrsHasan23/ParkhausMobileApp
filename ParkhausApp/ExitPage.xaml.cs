namespace ParkhausApp;

public partial class ExitPage : ContentPage
{
	public ExitPage()
	{
		InitializeComponent();
	}
    private async void CloseButton_Clicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();

    }
    //läuft automatisch 
    protected override void OnAppearing()
    {
        base.OnAppearing();
        //Stock1
        if (ParkingState.Stock1Parked == null)
        {
            Stock1ParkLabel.Text = "Geparkt: -";
        }
        else
        {
            Stock1ParkLabel.Text =
                $"Geparkt: {ParkingState.Stock1Parked}";
        }
        //Stock2
        if (ParkingState.Stock2Parked == null)
        {
            Stock2ParkLabel.Text = "Geparkt: -";
        }
        else
        {
            Stock2ParkLabel.Text =
                $"Geparkt: {ParkingState.Stock2Parked}";
        }
    }
    //Stock1
    private async void OnExitStock1Clicked(object sender, EventArgs e)
    {
        if (ParkingState.Stock1Parked == null)//Wenn nichts geparkt wurde
        {
            await DisplayAlert("Fehler", "Du hast nichts geparkt!", "OK");
            return;
        }
        //Parkplatz ist frei => null
        ParkingState.Stock1Parked = null;
        ParkingState.Free1++;
        //aktuelle Freieplätze
        Preferences.Set("Stock1_Free", ParkingState.Free1);
        //kein Parkplatz ist belegt
        Preferences.Set("Stock1_Parked", 0);

        Stock1ParkLabel.Text = "Geparkt: -";
        AusfahrtLabel.IsVisible = true;
    }
    //Stock2
    private async void OnExitStock2Clicked(object sender, EventArgs e)
    {
        if (ParkingState.Stock2Parked == null)//Wenn nichts geparkt wurde
        {
            await DisplayAlert("Fehler", "Du hast nichts geparkt!", "OK");
            return;
        }
        ParkingState.Stock2Parked = null;
        ParkingState.Free2++;

        Preferences.Set("Stock2_Free", ParkingState.Free2);
        Preferences.Set("Stock2_Parked", 0);

        Stock2ParkLabel.Text = "Geparkt: -";
        AusfahrtLabel.IsVisible = true;
    }

}