using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace helper_api_dotnet_o5.Models.Grupo13
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum SortEnum
    {
        atk, 
        def, 
        name, 
        type, 
        level, 
        id
}
}
