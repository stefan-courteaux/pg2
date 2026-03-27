using System.ComponentModel.DataAnnotations;

namespace MigrationsPetDemo.Model;

public class Pet
{
    public long Id { get; set; } 

    [MaxLength(30)] 
    public string Name { get; set; }
    public DateTime DateOfBirth { get; set; }
    public DateTime? DateOfDeath { get; set; } 

    public Owner Owner { get; set; } 
    public List<Note> Notes { get ; set; }
    public List<Vet> Vets { get; set; }
}

public class Horse : Pet
{
    [Range(0,6)]
    public int ShoeSizeFront { get; set; }
    [Range(0,6)]
    public int ShoeSizeBack { get; set; }
}

public class Hedgehog : Pet
{
    public bool IsDomesticatedBreed { get; set; }
}