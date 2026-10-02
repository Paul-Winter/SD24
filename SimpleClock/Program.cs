namespace SimpleClock
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string t;
            string m;
            int seconds;
            
            DateTime dt = DateTime.Now;
            seconds = dt.Second;

            for(;;)
            {
                dt = DateTime.Now;
                if (seconds != dt.Second)
                {
                    m = dt.ToString("T");
                    if (dt.Second == 0)
                        m = m + "\a";
                    Console.WriteLine(m);
                    t = dt.ToString("t");
                    if (dt.Minute == 0 && dt.Second == 0)
                    {
                        t = t + "\a";
                        Console.WriteLine(t);                        
                    }
                }
            }
        }
    }
}
