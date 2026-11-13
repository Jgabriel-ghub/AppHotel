using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace AppHotel.Models;

public class Reserva : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    private Quarto? quartoSelecionado;
    private double quantidadeHospedes = 1;
    private DateTime dataEntrada = DateTime.Today;
    private DateTime dataSaida = DateTime.Today.AddDays(1);

    public Quarto? QuartoSelecionado
    {
        get => quartoSelecionado;
        set { quartoSelecionado = value; Atualizar(); }
    }

    public double QuantidadeHospedes
    {
        get => quantidadeHospedes;
        set { quantidadeHospedes = value; Atualizar(); }
    }

    public DateTime DataEntrada
    {
        get => dataEntrada;
        set { dataEntrada = value; Atualizar(); }
    }

    public DateTime DataSaida
    {
        get => dataSaida;
        set { dataSaida = value; Atualizar(); }
    }

    public TimeSpan Periodo => DataSaida.Date - DataEntrada.Date;
    public int Diarias => Math.Max(0, Periodo.Days);
    public decimal ValorDiaria => QuartoSelecionado?.ValorDiaria ?? 0m;
    public decimal Total => ValorDiaria * (decimal)QuantidadeHospedes * Diarias;
    public string ValorDiariaFormatado => ValorDiaria.ToString("C", CultureInfo.GetCultureInfo("pt-BR"));
    public string TotalFormatado => Total.ToString("C", CultureInfo.GetCultureInfo("pt-BR"));

    private void Atualizar()
    {
        OnPropertyChanged(nameof(QuartoSelecionado));
        OnPropertyChanged(nameof(QuantidadeHospedes));
        OnPropertyChanged(nameof(DataEntrada));
        OnPropertyChanged(nameof(DataSaida));
        OnPropertyChanged(nameof(Periodo));
        OnPropertyChanged(nameof(Diarias));
        OnPropertyChanged(nameof(ValorDiaria));
        OnPropertyChanged(nameof(ValorDiariaFormatado));
        OnPropertyChanged(nameof(Total));
        OnPropertyChanged(nameof(TotalFormatado));
    }

    private void OnPropertyChanged([CallerMemberName] string? nome = null) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nome));
}
