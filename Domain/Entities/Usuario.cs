using Domain.Enums;

namespace Domain.Entities
{
    public class Usuario
    {
        public Guid Id { get; private set; }
        public string Nome { get; private set; }
        public string Email { get; private set; }
        public string SenhaHash { get; private set; }
        public Role Role { get; private set; }

        // Construtor
        public Usuario(string nome, string email, string senhaHash, Role role)
        {
            Id = Guid.NewGuid();
            Nome = nome;
            Email = email;
            SenhaHash = senhaHash;
            Role = role;
        }

        // Métodos de domínio
        public void AtualizarSenha(string novaSenhaHash)
        {
            SenhaHash = novaSenhaHash;
        }

        public void AtualizarRole(Role novoRole)
        {
            Role = novoRole;
        }
    }
}
