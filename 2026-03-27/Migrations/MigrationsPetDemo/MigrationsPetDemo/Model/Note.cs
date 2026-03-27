using System.ComponentModel.DataAnnotations;

namespace MigrationsPetDemo.Model;

public class Note
{
    public long Id { get; set; } 

    [MaxLength(5000)] 
    public string Content { get; set; }
    public DateTime CreationDate { get; set; }

    public Pet Pet { get; set; } 
}