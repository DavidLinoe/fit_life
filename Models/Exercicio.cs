namespace fit_life.Models
{
    public class Exercicio
    {
        public int Id { get; private set; }
        public string Nome { get; private set; }
        public string Instrucoes { get; private set; }
        public string AreaTreinada { get; private set; }
        public int Repeticoes { get; private set; }
        public int Series { get; private set; }

        public Exercicio(string nome, string instrucoes, string areaTreinada, int repeticoes, int series)
        {
            if (string.IsNullOrWhiteSpace(nome)) throw new ArgumentException("Nome inválido");
            if (repeticoes <= 0) throw new ArgumentException("Repetições deve ser maior que zero");

            Nome = nome;
            Instrucoes = instrucoes;
            AreaTreinada = areaTreinada;
            Repeticoes = repeticoes;
            Series = series;
        }
        public void Atualizar(string nome, string instrucoes, string areaTreinada, int repeticoes, int series)
        {
            if (string.IsNullOrWhiteSpace(nome)) throw new ArgumentException("Nome inválido");
            if (repeticoes <= 0) throw new ArgumentException("Repetições deve ser maior que zero");
            Nome = nome;
            Instrucoes = instrucoes;
            AreaTreinada = areaTreinada;
            Repeticoes = repeticoes;
            Series = series;
        }

    }
}
