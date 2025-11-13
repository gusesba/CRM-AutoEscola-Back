using System.Text.Json.Serialization;

namespace Exemplo.Domain.Model
{
    public class UsuarioModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Usuario { get; set; } = string.Empty;
        public string SenhaHash { get; set; } = string.Empty;
        public bool IsAdmin { get; set; } = false;

        [JsonIgnore]
        public ICollection<VendaModel> Vendas { get; set; }
    }
}
