using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CrudProjeto.Model
{
    public class Conta
    {
        [Key]
        public int num_conta { get; set; }

        [Required]
        public string? cpf_cliente { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "O saldo não pode ser negativo.")]
        public decimal saldo_conta { get; set; }

        [Required]
        public int num_agencia { get; set; }

        [DataType(DataType.Date)]
        [Range(typeof(DateTime), "1/1/1900", "1/1/2100", ErrorMessage = "Data inválida.")]
        public DateTime data_abertura { get; set; }

        [ForeignKey("num_agencia")]
        public Agencia? Agencia { get; set; }

        [ForeignKey("cpf_cliente")]
        public Cliente? Cliente { get; set; }
    }
}