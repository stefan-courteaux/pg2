// See https://aka.ms/new-console-template for more information

using Microsoft.EntityFrameworkCore;
using MigrationsPetDemo.Storage;

Console.WriteLine("Hello, World!");

using var context = new VetDbContext();
context.Database.Migrate();

var ownersWithPetsAndNotes = context.Owners
    .Include(o => o.Pets)
    .ThenInclude(p => p.Notes);

Console.WriteLine(ownersWithPetsAndNotes.ToQueryString());

foreach (var owner in ownersWithPetsAndNotes)
{
    
    Console.WriteLine($"{owner.FirstName} {owner.LastName}, {owner.Email}");
    foreach (var pet in owner.Pets)
    {
        Console.WriteLine($"> {pet.Name} {pet.DateOfBirth}");
        foreach (var note in pet.Notes)
        {
            Console.WriteLine($"  > {note.Content}");
        }
    }
    Console.WriteLine("---");
}