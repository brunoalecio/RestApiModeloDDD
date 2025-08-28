namespace RestApiModeloDDD.Domain.Entities
{
    public class Base
    {
        public int Id { get; set; }
        public virtual bool IsValido()
        {
            return Id > 0;
        }

    }
}
