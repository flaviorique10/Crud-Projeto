using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CrudProjeto.Model
{
    public class Investimento
    {
        [Key, Column(Order = 0)]
        public string? cpf_cliente { get; set; }

        [Key, Column(Order = 1)]
        public int num_conta { get; set; }

        public string? tipo_investimento { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "O valor não pode ser negativo.")]
        public decimal valor_investido { get; set; }

        public double rendimento { get; set; }

        public decimal valor_total { get; set; }

        [DataType(DataType.Date)]
        [Range(typeof(DateTime), "1/1/1900", "1/1/2100", ErrorMessage = "Data inválida.")]
        public DateTime data_investimento { get; set; }

        [ForeignKey("num_conta")]
        public Conta? Conta { get; set; }

        [ForeignKey("cpf_cliente")]
        public Cliente? Cliente { get; set; }
    }
}
