namespace fit_life.Models
{
    public class Usuario
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string SenhaHash { get; private set; }
        public Usuario(string name, string email)
        {
            Name = name;
            Email = email;
        }

        public void DefinirSenha(string senhaPura)
        {
            SenhaHash = BCrypt.Net.BCrypt.HashPassword(senhaPura);
        }

        public bool VerificarSenha(string senhaPura)
        {
            return BCrypt.Net.BCrypt.Verify(senhaPura, SenhaHash);
        }
    }
}