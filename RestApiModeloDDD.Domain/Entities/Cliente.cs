namespace RestApiModeloDDD.Domain.Entities
{
    public class Cliente : Base
    {
        public Cliente(string nome, string sobrenome, string email)
        {
            Nome = nome;
            Sobrenome = sobrenome;
            Email = email;
            DataDeCadastro = DateTime.Now;
            IsAtivo = true;
        }

        public string Nome { get; private set; }
        public string Sobrenome { get; private set; }
        public string Email { get; private set; }
        public DateTime DataDeCadastro { get; private set; }
        public bool IsAtivo { get; private set; }
        public override bool IsValido()
        {
            return !String.IsNullOrEmpty(Nome) && base.IsValido();
        }
    }
}