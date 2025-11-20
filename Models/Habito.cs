namespace fit_life.Models
{
    public class Habito
    {
        public int Id { get; private set; }
        public string Nome { get; private set; }
        public string Execucao { get; private set; }
        public string Recomendacoes { get; private set; }
        public float Tempo { get; private set; }

        public Habito(string nome, string execucao, string recomendacoes, float tempo)
        {
            if (string.IsNullOrWhiteSpace(nome)) throw new ArgumentException("O nome do hábito é obrigatório.");

            Nome = nome;
            Execucao = execucao;
            Recomendacoes = recomendacoes;
            Tempo = tempo;
        }
        public void Atualizar(string nome, string execucao, float tempo)
        {
            Nome = nome;
            Execucao = execucao;
            Tempo = tempo;
        }
    }
}
