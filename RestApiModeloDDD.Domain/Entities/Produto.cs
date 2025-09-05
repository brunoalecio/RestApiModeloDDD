namespace RestApiModeloDDD.Domain.Entities
{
    public class Produto : Base
    {
        public Produto(string nome, decimal valor)
        {
            Nome = nome;
            Valor = valor;
            IsDisponivel = true;
        }

        public string Nome { get; set; }
        public decimal Valor { get; set; }
        public bool IsDisponivel { get; set; }

    }
}