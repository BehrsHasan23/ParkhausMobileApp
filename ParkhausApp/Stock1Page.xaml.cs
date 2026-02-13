//Behrs Hasan
namespace ParkhausApp;

public partial class Stock1Page : ContentPage
{
    //Variable für Buttonklicks und hat bis jetzt kein Wert
    Button? gewaehlterPlatz = null;


    public Stock1Page()
    {
        InitializeComponent();
        ParkingState.Free1 = Preferences.Get("Stock1_Free", 12);//holt den Wert von Aktuellen freie plätze
        int saved = Preferences.Get("Stock1_Parked", 0);//holt den Wert von Parknummer wenn nicht gespeichert dann 0
        if (saved == 0)
        {
            ParkingState.Stock1Parked = null;
        }
        else
        {
            ParkingState.Stock1Parked = saved;
        }
        //aktualisiert den freie plätzen
        freiePlaezeLabel.Text = $"Freie Plätze: {ParkingState.Free1}";
    }
    private async void CloseButton_Clicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
        
    }
    private async void OnParkplatzClicked(object sender, EventArgs e)
    {
        if (ParkingState.Stock2Parked != null)//nur eine Stock pakieren mehr nicht
        {
            await DisplayAlert("Schon geparkt", "Du hast bereits einen Parkplatz belegt.", "OK");
            return;
        }

        if (ParkingState.Stock1Parked != null)
        {
            await DisplayAlert("Schon geparkt", "Du hast bereits einen Parkplatz belegt.", "OK");
            return;
        }

        var btn = (Button)sender;
       

        gewaehlterPlatz = btn;
        
        gewaehlterPlatz.BackgroundColor = Colors.Green;
        confirmButton.IsVisible = true;// Dann wird es sichtbar sein den ConfirmButton

        foreach (var element in PlacesGrid.Children)
            if (element is Button b && b != gewaehlterPlatz)//nicht geklickte button
                b.IsEnabled = false;
    }

    
    private void OnConfirmClicked(object sender, EventArgs e)
    {
        

        //schütz vor crashe wenn kei parkplatz ausgewählt wurde ist
        if (gewaehlterPlatz == null) return;
        //holt den Parkhausnummer
        int number = int.Parse(gewaehlterPlatz.CommandParameter.ToString());


        gewaehlterPlatz.IsEnabled = false;
        gewaehlterPlatz.BackgroundColor = Colors.Red;

        ParkingState.Stock1Parked = number;
        ParkingState.Free1--;

        Preferences.Set("Stock1_Free", ParkingState.Free1);//Das aktuelle Wert wird Lokal gespeichert

        if (ParkingState.Stock1Parked == null) //wenn kein parkplatz gespeichert ist
        {
            Preferences.Set("Stock1_Parked", 0);//kein Parktplatz gespeichert
        }
        else
        {
            Preferences.Set("Stock1_Parked", ParkingState.Stock1Parked.Value);//aktuellen wert
        }

        freiePlaezeLabel.Text = $"Freie Plätze: {ParkingState.Free1}";

        foreach (var element in PlacesGrid.Children)
            if (element is Button b && b != gewaehlterPlatz)
                b.IsEnabled = false;

        statusLabel.Text = "Parkplatz geparkt";
        confirmButton.IsVisible = false;
    }

}
