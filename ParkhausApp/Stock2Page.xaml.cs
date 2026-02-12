//Behrs Hasan
namespace ParkhausApp;

public partial class Stock2Page : ContentPage
{
    //Variable für Buttonklicks und hat kei Wert bis jetzt
    Button? gewaehlterPlatz = null;

    public Stock2Page()
	{
		InitializeComponent();
        freiePlaezeLabel.Text = $"Freie Plätze: {ParkingState.Free2}";
    }
    private async void CloseButton_Clicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();

    }
    private async void OnParkplatzClicked(object sender, EventArgs e)
    {
        if (ParkingState.Stock1Parked != null || ParkingState.Stock2Parked != null)//nur eine Stock pakieren mehr nicht
        {
            await DisplayAlert("Schon geparkt", "Du hast bereits einen Parkplatz belegt.", "OK");
            return;
        }

        if (ParkingState.Stock2Parked != null)//Nur einmal Parken
        {
            await DisplayAlert("Schon geparkt", "Du hast bereits einen Parkplatz belegt.", "OK");
            return;
        }

        var btn = (Button)sender;


        // Falls voll
        if (ParkingState.Free2 <= 0)
        {
            await DisplayAlert("Voll", "Keine freien Plätze mehr!", "OK");
            return;
        }

        //wir speichern das geklickte button in gewaehlterPlatz
        gewaehlterPlatz = btn;

        gewaehlterPlatz.BackgroundColor = Colors.Green;
        confirmButton.IsVisible = true;

        foreach (var element in PlacesGrid.Children)
            if (element is Button b && b != gewaehlterPlatz)
                b.IsEnabled = false;
    }


    private void OnConfirmClicked(object sender, EventArgs e)
    {

        
        if (gewaehlterPlatz == null) return;

        int number = int.Parse(gewaehlterPlatz.CommandParameter.ToString());


        gewaehlterPlatz.IsEnabled = false;
        gewaehlterPlatz.BackgroundColor = Colors.Red;

        ParkingState.Stock2Parked = number;
        ParkingState.Free2--;
        freiePlaezeLabel.Text = $"Freie Plätze: {ParkingState.Free2}";



        foreach (var element in PlacesGrid.Children)
            if (element is Button b && b != gewaehlterPlatz)
                b.IsEnabled = false;

        statusLabel.Text = "Parkplatz geparkt";
        confirmButton.IsVisible = false;
    }
}