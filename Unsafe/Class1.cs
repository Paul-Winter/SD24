using System;
using System.Collections.Generic;
using System.Text;

namespace Unsafe
{
    public partial class XY
    {
        public int X { get; set; }
        public int Y { get; set; }
        partial void Show()
        {
            Console.WriteLine($"X = {X}; Y = {Y};");
        }
    }
    internal class Class1
    {
    }
}
