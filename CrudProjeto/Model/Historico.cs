namespace CrudProjeto.Model
{
    public class Historico
    {
        public int tipo_cliente { get; set; }

        public string cpf_cliente { get; set; }

        public string num_conta { get; set; }

        public string saldo_conta { get; set; }

        public string tipo_movimento { get; set; }

        public DateTime data_historico { get; set; }

        public double valor_movimento { get; set; }
    }
}