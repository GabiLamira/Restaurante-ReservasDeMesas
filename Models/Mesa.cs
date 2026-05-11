namespace RestauranteReserva.Models
{
    public class Mesa  // o Entity Framework Core cria a tabela Mesas
    {
        public int Id { get; set; }  // o EF Core entende como chave primária, faz auto-incremento
        // get permite ler o valor, set permite escrever
        public int Numero { get; set; }
        public int Capacidade { get; set; }
        public bool Disponivel { get; set; } = true;  // valor padrão para toda nova mesa 'nascer' disponível
    }
}