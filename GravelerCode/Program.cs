using System.Diagnostics;

class GravelerCode
{
    private static readonly object myLock = new();

    public int totalTries = 0;
    public int maxNumberOfOnes = 0;


    static void Main(string[] args)
    {
        // Get number of tries from user
        Console.Write("Enter the number of tries: ");
        bool inputIsNumber = int.TryParse(Console.ReadLine(), out int numberOfTries);
        if (!inputIsNumber)
        {
            Console.WriteLine("Invalid input. Please enter a number.");
            Console.ReadLine();
            return;
        }
        Console.WriteLine();


        // Start stop watch and process the rolls
        var sw = Stopwatch.StartNew();

        GravelerCode gc = new();
        gc.Process(numberOfTries);

        sw.Stop();


        // Display results
        TimeSpan ts = TimeSpan.FromMilliseconds(sw.ElapsedMilliseconds);
        string totalTime = string.Format("{0:D2}h:{1:D2}m:{2:D2}s:{3:D3}ms",
                        ts.Hours,
                        ts.Minutes,
                        ts.Seconds,
                        ts.Milliseconds);

        Console.WriteLine($"Time: {totalTime}");
        Console.WriteLine($"Number of Roll Sessions: {gc.totalTries}");
        Console.WriteLine($"Highest Ones Roll: {gc.maxNumberOfOnes}");

        Console.ReadLine();
    }

    public void Process(int numberOfTries)
    {
        Parallel.For(0, numberOfTries, (x, loop) =>
        {
            Random random = new();
            int numberOfOnes = 0;
            for (int i = 0; i < 231; i++)
            {
                if (random.Next(1, 5) == 1) // 1 <= roll < 5
                {
                    numberOfOnes++;
                }
            }

            lock (myLock)
            {
                totalTries++;
                if (numberOfOnes > maxNumberOfOnes)
                {
                    maxNumberOfOnes = numberOfOnes;
                }
            }
        });
    }
}
