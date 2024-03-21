using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace helper_api_dotnet_o5.Models.Grupo13
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum RaceEnum
    {
        Aqua,
        Beast,
        [Display(Name = "Beast-Warrior")]
        BeastWarrior,
        [Display(Name = "Creator-God")]
        CreatorGod,
        Cyberse,
        Dinosaur,
        [Display(Name = "Divine-Beast")]
        DivineBeast,
        Dragon,
        Fairy,
        Fiend,
        Fish,
        Insect,
        Machine,
        Plant,
        Psychic,
        Pyro,
        Reptile,
        Rock,
        [Display(Name = "Sea Serpent")]
        SeaSerpent,
        Spellcaster,
        Thunder,
        Warrior,
        [Display(Name = "Winged Beast")]
        WingedBeast,
        Wyrm,
        Zombie
    }
}
