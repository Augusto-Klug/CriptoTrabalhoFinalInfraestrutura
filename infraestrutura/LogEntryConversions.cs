using System.Text.Json;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace CriptoTrabalhoFinalInfraestrutura.infraestrutura;

public static class LogEntryConversions
{
    private static readonly JsonSerializerOptions JsonSerializerOptions = new(JsonSerializerDefaults.Web);

    public static readonly ValueComparer<List<string>> CriptosComparer = new(
        (left, right) => left!.SequenceEqual(right!),
        value => value.Aggregate(0, (hash, item) => HashCode.Combine(hash, item == null ? 0 : item.GetHashCode())),
        value => value.ToList());

    public static string SerializeCriptos(List<string> criptos)
    {
        return JsonSerializer.Serialize(criptos, JsonSerializerOptions);
    }

    public static List<string> DeserializeCriptos(string criptos)
    {
        if (string.IsNullOrWhiteSpace(criptos))
        {
            return [];
        }

        return JsonSerializer.Deserialize<List<string>>(criptos, JsonSerializerOptions) ?? [];
    }
}
