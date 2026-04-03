using System.Diagnostics;

namespace AsynchroneAmbtenaar;

public class Ministerie
{
    public void RunRustigeDag()
    {
        var ambtenaar = new Ambtenaar() { Naam = "Karel" };
        var stopwatch = new Stopwatch();

        stopwatch.Start();
        ambtenaar.DrinkKoffie();
        ambtenaar.VerrichtKleineTaak(1);
        ambtenaar.DrinkKoffie();
        ambtenaar.VerrichtKleineTaak(2);
        ambtenaar.DrinkKoffie();
        stopwatch.Stop();

        PrintVerstrekenTijd(stopwatch);
    }
    
    public void RunDrukkeDag()
    {
        var ambtenaar = new Ambtenaar() { Naam = "Karel" };
        var stopwatch = new Stopwatch();

        stopwatch.Start();
        ambtenaar.VerrichtKleineTaak(1);
        ambtenaar.VerrichtGroteTaak(2);
        ambtenaar.VerrichtKleineTaak(3);
        stopwatch.Stop();

        PrintVerstrekenTijd(stopwatch);
    }
    
    public void RunDrukkeDagMetChef()
    {
        var chef = new Chef() {Naam = "Bernard"};
        var ambtenaar = new Ambtenaar() { Naam = "Karel", Chef = chef};
        var stopwatch = new Stopwatch();

        stopwatch.Start();
        ambtenaar.VerrichtKleineTaak(1);
        ambtenaar.VraagToestemming(2);
        ambtenaar.VerrichtGroteTaak(2);
        ambtenaar.VerrichtKleineTaak(3);
        stopwatch.Stop();

        PrintVerstrekenTijd(stopwatch);
    }
    
    public async Task RunDrukkeDagMetChefAsync()
    {
        var chef = new Chef() {Naam = "Bernard"};
        var ambtenaar = new Ambtenaar() { Naam = "Karel", Chef = chef};
        var stopwatch = new Stopwatch();

        stopwatch.Start();
        var wachtendeToestemming = ambtenaar.VraagToestemmingAsync(2); 
        ambtenaar.VerrichtKleineTaak(1);
        ambtenaar.VerrichtKleineTaak(3);
        await wachtendeToestemming;
        ambtenaar.VerrichtGroteTaak(2);
        stopwatch.Stop();

        PrintVerstrekenTijd(stopwatch);
    }

    private void PrintVerstrekenTijd(Stopwatch stopwatch)
    {
        Console.WriteLine($"De dag duurde {stopwatch.ElapsedMilliseconds} milliseconden.");
    }
}