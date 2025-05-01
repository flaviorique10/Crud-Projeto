namespace CrudProjeto.Model
{
    public class Cliente
    {
        public string cpf_cliente { get; set; }

        public string nome_cliente { get; set; }

        public string endereco_cliente { get; set; }

        public string telefone_cliente { get; set; }

        public string email_cliente { get; set; }

        public char sexo_cliente { get; set; }

        public DateTime data_nascimento_cliente { get; set; }

        public int tipo_cliente { get; set; }

        public string profissao_cliente { get; set; }

        public string estado_civil_cliente { get; set; }

        public string nacionalidade_cliente { get; set; }

        public string rg_cliente { get; set; }

        public string nome_pai_cliente { get; set; }

        public string nome_mae_cliente { get; set; }

        public double renda_cliente { get; set; }
    }
}