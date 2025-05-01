using System.ComponentModel.DataAnnotations.Schema;

namespace CrudProjeto.Model
{
    public class Investimento
    {
        [ForeignKey("Cliente")]
        public string? cpf_cliente { get; set; }

        [ForeignKey("Conta")]
        public int num_conta { get; set; }

        public string? tipo_investimento { get; set; }

        public decimal valor_investido { get; set; }

        public double rendimento { get; set; }

        public decimal valor_total { get; set; }

        public DateTime data_investimento { get; set; }

        public Conta? Conta { get; set; }

        public Cliente? Cliente { get; set; }
    }
}
