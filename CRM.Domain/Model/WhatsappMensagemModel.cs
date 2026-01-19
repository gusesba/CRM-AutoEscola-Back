using System.Text.Json.Serialization;

namespace Exemplo.Domain.Model
{
    public class WhatsappMensagemModel
    {
        public string Id { get; set; } = string.Empty;
        public string Body { get; set; } = string.Empty;
        public bool FromMe { get; set; }
        public long Timestamp { get; set; }
        public string Type { get; set; } = string.Empty;
        public bool HasMedia { get; set; }
        public string? MediaUrl { get; set; }
        public string? Author { get; set; }
        public string WhatsappChatId { get; set; } = string.Empty;

        [JsonIgnore]
        public WhatsappChatModel Chat { get; set; }
    }
}
