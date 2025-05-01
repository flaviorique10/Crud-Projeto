using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CrudProjeto.Model
{
    public class Conta
    {
        [Key]
        public int num_conta { get; set; }

        [ForeignKey("Cliente")]
        public string? cpf_cliente { get; set; }

        public decimal saldo_conta { get; set; }

        [ForeignKey("Agencia")]
        public int num_agencia { get; set; }

        public DateTime data_abertura { get; set; }

        public Agencia? Agencia { get; set; }

        public Cliente? Cliente { get; set; }
    }
}