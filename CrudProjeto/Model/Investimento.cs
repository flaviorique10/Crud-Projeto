namespace CrudProjeto.Model
{
    public class Investimento
    {
        public string cpf_cliente { get; set; }

        public string num_conta { get; set; }

        public string tipo_investimento { get; set; }

        public double valor_investido { get; set; }

        public double rendimento { get; set; }

        public double valor_total { get; set; }

        public DateTime data_investimento { get; set; }

    }
}
