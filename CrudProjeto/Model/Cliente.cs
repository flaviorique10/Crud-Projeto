using System.ComponentModel.DataAnnotations;

namespace CrudProjeto.Model
{
    public class Cliente
    {
        [Key]
        public string? cpf_cliente { get; set; }

        public string? nome_cliente { get; set; }

        public string? endereco_cliente { get; set; }

        public string? telefone_cliente { get; set; }

        public string? email_cliente { get; set; }

        public string? sexo_cliente { get; set; }

        [DataType(DataType.Date)]
        [Range(typeof(DateTime), "1/1/1900", "1/1/2100", ErrorMessage = "Data inválida.")]
        public DateTime? data_nascimento_cliente { get; set; }

        public string? profissao_cliente { get; set; }

        public string? estado_civil_cliente { get; set; }

        public string? nacionalidade_cliente { get; set; }

        public string? rg_cliente { get; set; }

        public string? nome_pai_cliente { get; set; }

        public string? nome_mae_cliente { get; set; }

        public decimal renda_cliente { get; set; }


    }
}