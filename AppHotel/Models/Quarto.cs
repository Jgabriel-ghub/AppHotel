namespace AppHotel.Models;

public class Quarto
{
    public string Nome { get; set; } = string.Empty;
    public decimal ValorDiaria { get; set; }
    public int CapacidadeMaxima { get; set; }

    public string Descricao => $"{Nome} - ate {CapacidadeMaxima} hospedes";
}
