using Dingo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Foo
{
    static void Main(string[] args)
    {
        String solution = args[0];
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        DingoService engine = new DingoService(solution);
        System.Diagnostics.Debug.WriteLine(engine.Execute());
    }
}