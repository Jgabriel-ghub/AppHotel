using AppHotel.Models;
using AppHotel.Views;

namespace AppHotel;

public partial class MainPage : ContentPage
{
    private readonly Reserva reserva = new();

    private readonly List<Quarto> quartos = new()
    {
        new Quarto { Nome = "Quarto Standard", ValorDiaria = 180m, CapacidadeMaxima = 2 },
        new Quarto { Nome = "Quarto Luxo", ValorDiaria = 290m, CapacidadeMaxima = 3 },
        new Quarto { Nome = "Suite Premium", ValorDiaria = 450m, CapacidadeMaxima = 4 }
    };

    public MainPage()
    {
        InitializeComponent();
        BindingContext = reserva;
        pck_quarto.ItemsSource = quartos;
        dtp_entrada.MinimumDate = DateTime.Today;
        dtp_saida.MinimumDate = reserva.DataEntrada.AddDays(1);
    }

    private void OnEntradaDateSelected(object? sender, DateChangedEventArgs e)
    {
        // Atualiza o intervalo permitido para a data de saída.
        DateTime entrada = e.NewDate ?? dtp_entrada.Date ?? DateTime.Today;
        DateTime primeiraSaidaPermitida = entrada.Date.AddDays(1);
        if ((dtp_saida.Date ?? DateTime.Today).Date < primeiraSaidaPermitida)
            dtp_saida.Date = primeiraSaidaPermitida;
        dtp_saida.MinimumDate = primeiraSaidaPermitida;
    }

    private async void OnSaidaDateSelected(object? sender, DateChangedEventArgs e)
    {
        DateTime saida = e.NewDate ?? dtp_saida.Date ?? DateTime.Today;
        DateTime entrada = dtp_entrada.Date ?? DateTime.Today;
        if (saida.Date <= entrada.Date)
        {
            dtp_saida.Date = entrada.Date.AddDays(1);
            await DisplayAlertAsync("Data inválida", "A saída deve ser posterior à entrada.", "OK");
        }
    }

    private async void OnCalcularClicked(object? sender, EventArgs e)
    {
        if (reserva.QuartoSelecionado is null)
        {
            await DisplayAlertAsync("Atenção", "Selecione um tipo de quarto.", "OK");
            return;
        }
        if (reserva.DataSaida.Date <= reserva.DataEntrada.Date)
        {
            await DisplayAlertAsync("Atenção", "A data de saída deve ser posterior à entrada.", "OK");
            return;
        }
        if (reserva.QuantidadeHospedes > reserva.QuartoSelecionado.CapacidadeMaxima)
        {
            await DisplayAlertAsync("Atenção",
                $"Este quarto aceita no máximo {reserva.QuartoSelecionado.CapacidadeMaxima} hóspedes.", "OK");
            return;
        }

        await DisplayAlertAsync("Resumo da hospedagem",
            $"Quarto: {reserva.QuartoSelecionado.Nome}\n" +
            $"Hóspedes: {reserva.QuantidadeHospedes:F0}\n" +
            $"Entrada: {reserva.DataEntrada:dd/MM/yyyy}\n" +
            $"Saída: {reserva.DataSaida:dd/MM/yyyy}\n" +
            $"Diárias: {reserva.Diarias}\n" +
            $"Total: {reserva.TotalFormatado}", "OK");
    }

    private async void OnSobreClicked(object? sender, EventArgs e)
    {
        await Navigation.PushAsync(new SobrePage());
    }
}
