using System.Text.Json.Serialization;

namespace Globomantics.Shared
{
    public class UserClaim
    {
        [JsonPropertyName("type")]
        public string Type { get; set; } = string.Empty;
        [JsonPropertyName("value")]
        public object Value { get; set; } = string.Empty;
    }
}