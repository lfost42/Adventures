using System;
using System.Collections.Generic;
using System.Linq;

namespace Zedwork
{
    public class Counter
    {
        
        private double? _percentage;
        public Counter(string name, int count)
        {
            Name = name;
            Count = count;
        }

        public string Name { get; }
        public int Count { get; private set; }

        public double GetPercent(int total) => _percentage ??
            (_percentage = Math.Round(Count * 100.0 / total, 2)).Value;

        public void AddExcess(double excess) => _percentage += excess;


    }

    public class CounterManager
    {
        public CounterManager(params Counter[] counters)
        {
            Counters = new List<Counter>(counters);
        }

        public List<Counter> Counters { get; set; }

        public int Total() => Counters.Sum(x => x.Count);

        public double TotalPercentage() => Counters.Sum(x => x.GetPercent(Total()));

        public void AnnounceWinner()
        {

            var excess = Math.Round(100 - TotalPercentage(), 2);

            var maxVotes = Counters.Max(x => x.Count);

            var winners = Counters.Where(x => x.Count == maxVotes).ToList();

            if(winners.Count == 1)
            {
                var winner = winners.First();
                winner.AddExcess(excess);
                Console.WriteLine($"{winner.Name} wins!");
            }
            else 
            {
                if (winners.Count != Counters.Count)
                {
                    var minVotes = Counters.Min(x => x.Count);
                    var loser = Counters.First(x => x.Count == minVotes);
                    loser.AddExcess(excess);
                }
                Console.WriteLine(string.Join(" -draw- ", winners.Select(x => x.Name)));
                
            }

            Console.WriteLine($"Excess: {Math.Round(excess, 2)}%");

            foreach (var c in Counters)
            {
                Console.WriteLine($"{c.Name}: {c.Count}, {c.GetPercent(Total())}%");
            }

            Console.WriteLine($"Total Percentage: {Math.Round(TotalPercentage(), 2)}%");

        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var yes = new Counter("Yes", 4);
            var no = new Counter("No", 3);
            var maybe = new Counter("Maybe", 3);

            var manager = new CounterManager(yes, no, maybe);

            manager.AnnounceWinner();

        }
    }
}
