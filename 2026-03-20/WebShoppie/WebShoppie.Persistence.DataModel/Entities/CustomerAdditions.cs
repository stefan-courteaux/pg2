namespace WebShoppie.DataModel.Entities;

public partial class Customer
{
    public int Age => DateTime.Today.Year - Dateofbirth.Year;
}