//Behrs Hasan
namespace ParkhausApp;

public partial class Stock1Page : ContentPage
{
    
    Button? gewaehlterPlatz = null;


    public Stock1Page()
    {
        InitializeComponent();
        //aktualisiert den freie plätzen
        freiePlaezeLabel.Text = $"Freie Plätze: {ParkingState.Free1}";
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

        if (ParkingState.Stock1Parked != null)
        {
            await DisplayAlert("Schon geparkt", "Du hast bereits einen Parkplatz belegt.", "OK");
            return;
        }

        var btn = (Button)sender;
       

        // Falls es voll ist
        if (ParkingState.Free1 <= 0)
        {
            await DisplayAlert("Voll", "Keine freien Plätze mehr!", "OK");
            return;
        }

        gewaehlterPlatz = btn;
        
        gewaehlterPlatz.BackgroundColor = Colors.Green;
        confirmButton.IsVisible = true;// Dann wird es sichtbar sein den ConfirmButton

        foreach (var element in PlacesGrid.Children)
            if (element is Button b && b != gewaehlterPlatz)
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
        freiePlaezeLabel.Text = $"Freie Plätze: {ParkingState.Free1}";

        foreach (var element in PlacesGrid.Children)
            if (element is Button b && b != gewaehlterPlatz)
                b.IsEnabled = false;

        statusLabel.Text = "Parkplatz geparkt";
        confirmButton.IsVisible = false;
    }

}
