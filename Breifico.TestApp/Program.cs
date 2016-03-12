using Breifico.Algorithms.Formats;

namespace Breifico.TestApp
{
    class Program
    {


        static void Main(string[] args) {
            var x = new BmpReader(@"D:\My Files\Unit\my.bmp");
            x.Read();
        }
    }
}
