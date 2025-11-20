namespace fit_life.Models
{
    public class Treino
    {
        public int Id { get; private set; }
        public string Nome { get; private set; }
        public List<Exercicio> Exercicios { get; private set; }
        public float Tempo { get; private set; }


        public Treino(string nome, float tempo)
        {
            if (string.IsNullOrWhiteSpace(nome)) throw new ArgumentException("O nome do treino é obrigatório");

            Nome = nome;
            Exercicios = new List<Exercicio>();
            Tempo = tempo;
        }
        public void AdicionarExercicio(Exercicio exercicio)
        {
            if (exercicio == null) return;
            Exercicios.Add(exercicio);
        }
        public void RemoverExercicio(Exercicio exercicio)
        {
            Exercicios.Remove(exercicio);
        }
        public void AtualizarTempo(float tempo)
        {
            Tempo = tempo;
        }
    }
}
