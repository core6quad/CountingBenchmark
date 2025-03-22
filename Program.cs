using System;
using System.Diagnostics;
using System.Net.Quic;

class cs_fastCounter{
    static void Main() {
        System.ConsoleKeyInfo input;
        Console.WriteLine("Welccome to The Counting Benchmark");
        Console.WriteLine("press any key to start benchmark");
        input = Console.ReadKey();
        Console.WriteLine("Benchmarking...");
        List<float> delta = new List<float>();
         var sw = Stopwatch.StartNew();
        long total = 0;
        for (int i = 0; i < 15001; i++){
            Console.WriteLine(i);
            if (sw.ElapsedMilliseconds - total > 0)
            {
                

            delta.Add(sw.ElapsedMilliseconds - total);
            total = total + sw.ElapsedMilliseconds;
            } else {
                delta.Add(0);
            }
            Console.Clear();

            
        }
        Console.Clear();
        Console.WriteLine("Done in: " + sw.ElapsedMilliseconds / 1000 + " seconds");
        Console.WriteLine("Average time per iteration: " + delta.Average() + " ms"); 
        Console.WriteLine("Max time per iteration: " + delta.Max() + " ms"); 
        Console.WriteLine("Min time per iteration: " + delta.Min() + " ms"); 
        Console.WriteLine("your score is: " + sw.ElapsedMilliseconds / delta.Average());
        Console.WriteLine("Press any key to exit");
        input = Console.ReadKey();
        Environment.Exit(0);
    }
}