namespace fit_life.Models
{
    public class HistoricoTreino
    {
        public int Id { get; private set; }
        public int UsuarioId { get; private set; } 
        public int TreinoId { get; private set; }  
        public DateTime DataConclusao { get; private set; } 
        public int TempoGastoMinutos { get; private set; }

        public HistoricoTreino(int usuarioId, int treinoId, int tempoGastoMinutos)
        {
            UsuarioId = usuarioId;
            TreinoId = treinoId;
            TempoGastoMinutos = tempoGastoMinutos;
            DataConclusao = DateTime.Now; 
        }
    }
}
