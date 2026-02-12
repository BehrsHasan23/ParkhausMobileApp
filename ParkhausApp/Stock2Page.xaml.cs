//Behrs Hasan
namespace ParkhausApp;

public partial class Stock2Page : ContentPage
{
    //Variable für Buttonklicks und hat bis jetzt kein Wert 
    Button? gewaehlterPlatz = null;

    public Stock2Page()
	{
		InitializeComponent();
        ParkingState.Free2 = Preferences.Get("Stock2_Free", 12);//holt den Wert von Aktuellen freie plätze
        int saved = Preferences.Get("Stock2_Parked", 0);//holt den Wert von Parknummer sonst ist es 0
        if (saved == 0)
        {
            ParkingState.Stock2Parked = null;
        }
        else
        {
            ParkingState.Stock2Parked = saved;
        }
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

        //schütz vor crashe wenn kei parkplatz ausgewählt wurde ist
        if (gewaehlterPlatz == null) return;

        int number = int.Parse(gewaehlterPlatz.CommandParameter.ToString());


        gewaehlterPlatz.IsEnabled = false;
        gewaehlterPlatz.BackgroundColor = Colors.Red;

        ParkingState.Stock2Parked = number;
        ParkingState.Free2--;

        Preferences.Set("Stock2_Free", ParkingState.Free2);//aktuelle wert wird in lokal gespeichert

        if (ParkingState.Stock2Parked == null)//wenn kein Parktplatz gespeichert?
        {
            Preferences.Set("Stock2_Parked", 0);//0 heisst kein Parkplatz gespeichert also wie Null
        }
        else
        {
            Preferences.Set("Stock2_Parked", ParkingState.Stock2Parked.Value);//speichert aktuelle Wert
        }

        freiePlaezeLabel.Text = $"Freie Plätze: {ParkingState.Free2}";



        foreach (var element in PlacesGrid.Children)
            if (element is Button b && b != gewaehlterPlatz)
                b.IsEnabled = false;

        statusLabel.Text = "Parkplatz geparkt";
        confirmButton.IsVisible = false;
    }
}