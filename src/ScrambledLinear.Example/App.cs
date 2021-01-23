using System;
using ScrambledLinear;

var random = new Xoshiro256SS();

for (int i = 0; i < 10; i++) {
    Console.WriteLine(random.Next());
}


/* create a 16 GB file

using (var file = System.IO.File.OpenWrite("test.bin")) {
    var buffer = new byte[16 * 1024];
    for (int i = 0; i < 1024 * 1024; i++) {
        random.NextBytes(buffer);
        file.Write(buffer, 0, buffer.Length);
        Console.Write(".");
    }
}
Console.WriteLine();

*/
