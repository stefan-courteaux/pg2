// See https://aka.ms/new-console-template for more information

using AsynchroneAmbtenaar;

Console.WriteLine("Hello, World!");

var ministerie = new Ministerie();
ministerie.RunRustigeDag();

Console.WriteLine(new string('-', 50));
ministerie.RunDrukkeDag();

Console.WriteLine(new string('-', 50));
ministerie.RunDrukkeDagMetChef();

Console.WriteLine(new string('-', 50));
await ministerie.RunDrukkeDagMetChefAsync();