using System.ComponentModel.DataAnnotations.Schema;

namespace CrudProjeto.Model
{
    public class Historico
    {
        public int tipo_cliente { get; set; }

        [ForeignKey("Cliente")]
        public string? cpf_cliente { get; set; }

        [ForeignKey("Conta")]
        public int num_conta { get; set; }

        [ForeignKey("Conta")]
        public decimal saldo_conta { get; set; }

        public string? tipo_movimento { get; set; }

        public DateTime data_historico { get; set; }

        public decimal valor_movimento { get; set; }

        public Conta? Conta { get; set; }

        public Cliente? Cliente { get; set; }
    }
}