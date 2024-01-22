public class CalculadoraNotas
{
    public static double CalcularMedia(List<int> notas)
    {
        return Math.Round(notas.Average(), 2);
    }
}