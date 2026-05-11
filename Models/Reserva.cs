using System.ComponentModel.DataAnnotations;  // [Required] e outras anotações de validação
using RestauranteReserva.Validations; 

namespace RestauranteReserva.Models
{
    public class Reserva
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Nome do cliente é obrigatório")]  // coluna NOT NULL no SQL 
        [MaxLength(100)]
        public required string NomeCliente { get; set; }  // uma coluna NVARCHAR(100) no SQL, Unicode (qualquer idioma + emojis)
        // o compilador proíbe a criação de um objeto dessa classe sem dar um valor para essa propriedade.
        
        [Required(ErrorMessage = "Telefone é obrigatório")]
        public required string Telefone { get; set; }

        [DataFutura] 
        public DateTime DataHora { get; set; }  // coluna DATETIME2 no SQL

        [Range(1, 20, ErrorMessage = "Número de pessoas deve ser entre 1 e 20")]
        public int NumeroPessoas { get; set; }

        public int? MesaId { get; set; }  // chave estrangeira, o EF Core reconhece o padrão [NomeDaClasse]Id e cria o relacionamento automaticamente no banco
        // ? pois a reserva pode ser feita sem mesa definida
        public Mesa? Mesa { get; set; }  // propriedade de navegação, o EF Core entende que é a referência para a mesa relacionada. O '?' é uma proteção do compilador, uma boa prática, "Eu sei que essa propriedade pode não estar preenchida dependendo de como for buscada."
    }
}

// DateTime e int são value types em C#, eles por natureza nunca podem ser nulos — sempre têm um valor padrão: 0 e 01/01/0001 00:00:00