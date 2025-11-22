namespace fit_life.DTOs.Exercicio
{
    public class CreateExercicioRequest
    {
        public string Nome { get; set; }
        public string Instrucoes { get; set; }
        public string AreaTreinada { get; set; }
        public int Repeticoes { get; set; }
        public int Series { get; set; }
        public string? ImagemBase64 { get; set; }
    }
}
