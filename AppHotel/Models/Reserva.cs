using System;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace AppHotel.Models;

public class Reserva : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    private string? tipoQuarto;
    private double quantidadeHospedes = 1;
    private DateTime dataEntrada = DateTime.Today;
    private DateTime dataSaida = DateTime.Today.AddDays(1);

    public string? TipoQuarto
    {
        get => tipoQuarto;
        set { tipoQuarto = value; Atualizar(); }
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
    public decimal ValorDiaria => TipoQuarto switch
    {
        "Quarto Standard" => 180m,
        "Quarto Luxo" => 290m,
        "Suite Premium" => 450m,
        _ => 0m
    };
    public decimal Total => ValorDiaria * (decimal)QuantidadeHospedes * Diarias;
    public string ValorDiariaFormatado => ValorDiaria.ToString("C", CultureInfo.GetCultureInfo("pt-BR"));
    public string TotalFormatado => Total.ToString("C", CultureInfo.GetCultureInfo("pt-BR"));

    private void Atualizar()
    {
        OnPropertyChanged(nameof(TipoQuarto));
        OnPropertyChanged(nameof(QuantidadeHospedes));
        OnPropertyChanged(nameof(DataEntrada));
        OnPropertyChanged(nameof(DataSaida));
        OnPropertyChanged(nameof(Diarias));
        OnPropertyChanged(nameof(ValorDiariaFormatado));
        OnPropertyChanged(nameof(TotalFormatado));
    }

    private void OnPropertyChanged([CallerMemberName] string? nome = null) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nome));
}
