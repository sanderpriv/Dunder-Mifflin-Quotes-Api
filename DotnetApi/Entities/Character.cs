using System.ComponentModel.DataAnnotations;

namespace DotnetApi.Entities;

public class Character
{
    [Key]
    public string Name { get; set; }
    public string DisplayName { get; set; }

    public Character(string name)
    {
        Name = name;
        DisplayName = name;
    }
}