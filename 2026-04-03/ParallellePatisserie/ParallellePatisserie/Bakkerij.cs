using System.Diagnostics;

namespace ParallellePatisserie;

public class Bakkerij
{
    public void RunSingle()
    {
        var pistolet = new Pistolet();
        var stopwatch = new Stopwatch();

        stopwatch.Start();
        pistolet.Bak();
        stopwatch.Stop();

        PrintVerstrekenTijd(stopwatch);
    }
    
    public void RunSequential()
    {
        var stopwatch = new Stopwatch();
        const int aantalPistolets = 100;

        var pistolets = new List<Pistolet>();
        for (int i = 0; i < aantalPistolets; i++) 
            pistolets.Add(new Pistolet());

        stopwatch.Start();
        foreach (var pistolet in pistolets) 
            pistolet.Bak();
        stopwatch.Stop();

        PrintVerstrekenTijd(stopwatch);
    }
    
    public void RunParallel()
    {
        var stopwatch = new Stopwatch();
        const int aantalPistolets = 100;

        var pistolets = new List<Pistolet>();
        for (int i = 0; i < aantalPistolets; i++) 
            pistolets.Add(new Pistolet());

        stopwatch.Start();
        Parallel.ForEach(pistolets, pistolet => pistolet.Bak()); 
        stopwatch.Stop();

        PrintVerstrekenTijd(stopwatch);
    }
    
    private static void PrintVerstrekenTijd(Stopwatch stopwatch)
    {
        Console.WriteLine($"Verstreken tijd {stopwatch.ElapsedMilliseconds} ms.");
    }
}