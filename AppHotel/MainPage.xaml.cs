using AppHotel.Models;
using AppHotel.Views;

namespace AppHotel;

public partial class MainPage : ContentPage
{
    private readonly Reserva reserva = new();

    public MainPage()
    {
        InitializeComponent();
        BindingContext = reserva;
    }

    private async void OnCalcularClicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(reserva.TipoQuarto))
        {
            await DisplayAlert("Atenção", "Selecione um tipo de quarto.", "OK");
            return;
        }
        if (reserva.DataSaida.Date <= reserva.DataEntrada.Date)
        {
            await DisplayAlert("Atenção", "A data de saída deve ser posterior à entrada.", "OK");
            return;
        }

        await DisplayAlert("Resumo da hospedagem",
            $"Quarto: {reserva.TipoQuarto}\n" +
            $"Hóspedes: {reserva.QuantidadeHospedes:F0}\n" +
            $"Entrada: {reserva.DataEntrada:dd/MM/yyyy}\n" +
            $"Saída: {reserva.DataSaida:dd/MM/yyyy}\n" +
            $"Diárias: {reserva.Diarias}\n" +
            $"Total: {reserva.TotalFormatado}", "OK");
    }

    private async void OnSobreClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new SobrePage());
    }
}
