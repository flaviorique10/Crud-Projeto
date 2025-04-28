namespace CrudProjeto.Model
{
    public class Conta
    {
        public int tipo_cliente { get; set; }

        public string num_conta { get; set; }

        public string cpf_cliente { get; set; }

        public double saldo_conta { get; set; }

        public int tipo_conta { get; set; }

        public int num_agencia { get; set; }

        public DateTime data_abertura { get; set; }
    }
}