// See https://aka.ms/new-console-template for more information

using System.Diagnostics;
using ParallellePatisserie;

Console.WriteLine("Hello, World!");


var bakkerij = new Bakkerij();
bakkerij.RunSingle();

Console.WriteLine(new string('-', 50));
bakkerij.RunSequential();

Console.WriteLine(new string('-', 50));
bakkerij.RunParallel();