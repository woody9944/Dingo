using System;

public class Dingo
{
    static void Main(string[] args)
    {
        String solution = args[0];
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Engine engine = new Engine(solution);
        System.Diagnostics.Debug.WriteLine(engine.Execute());
    }
}