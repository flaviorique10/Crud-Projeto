using System.ComponentModel.DataAnnotations;

namespace CrudProjeto.Model
{
    public class Agencia
    {
        [Key]
        public int num_agencia { get; set; }

        public string? nome_agencia { get; set; }

        public string? endereco_agencia { get; set; }

        public string? tipo_agencia { get; set; }

        public string? telefone_agencia { get; set; }

    }
}