using System;
using System.Threading;

namespace Q2Advanced
{
    class MainClass
    {
        public static void Main (string[] args)
        {
            Play(500, 10, 10, 10, 3);
        }

        private static void Play(int baseMoney, int minMoney, int increase, int posibility, int numberOfCards)
        {
            Console.WriteLine("You heve ${0} now", baseMoney);
            Random rand = new Random();
            int[] cards = new int[numberOfCards];
            while (baseMoney > 0) 
            {
                int start = GetBet(minMoney, increase, baseMoney);
                if (start < 0)
                {
                    Console.WriteLine("You have ${0}. Please checkout", baseMoney);
                    break;
                }

                if ((posibility > 0) && (rand.Next(posibility) == 0))
                {
                    var cardNumber = rand.Next(1, 9);
                    for (var i = 0; i < numberOfCards; i++)
                    {
                        cards[i] = cardNumber;
                    }
                }
                else
                {
                    for (var i = 0; i < numberOfCards; i++)
                    {
                        cards[i] = rand.Next(1, 9);
                    }
                }

                for (var i = 0; i < numberOfCards; i++)
                {
                    ShowNumber(cards[i]);
                }
                Console.WriteLine();

                if (AreNumbersSame(cards))
                {
                    var won = start * cards[0];
                    baseMoney = baseMoney + won;
                    Console.WriteLine("Won ${0}!. You have ${1} now", won, baseMoney);
                }
                else
                {
                    baseMoney = baseMoney - start;
                    Console.WriteLine("Sorry. Loose ${0}. You have ${1} now", start, baseMoney);
                }
            }

            if (baseMoney < 1)
            {
                Console.WriteLine("Please try next time");
            }
        }

        private static bool AreNumbersSame(int[] cards)
        {
            var number = cards[0];
            for (var i = 0; i < cards.Length; i++)
            {
                if (cards[i] != number)
                {
                    return false;
                }
            }

            return true;
        }

        private static int GetBet(int startMoney, int increase, int currentMoney)
        {
            while (true)
            {
                int newStart = startMoney + increase;
                if (newStart <= currentMoney)
                {
                    Console.WriteLine(
                        "Press 'ESC' key to exit, 'Space' to add ${0} to ${1}, 'Enter' to start", 
                        increase,
                        startMoney);
                }
                else
                {
                    Console.WriteLine(
                        "Press 'ESC' key to exit, 'Enter' start to with ${0}", 
                        startMoney);
                }
                var keyInfo = Console.ReadKey();
                switch (keyInfo.Key)
                {
                    case ConsoleKey.Spacebar:
                        if (newStart <= currentMoney)
                        {
                            startMoney = newStart;
                        }
                        break;
                    case ConsoleKey.Escape:
                        return -1;
                    case ConsoleKey.Enter:
                        return startMoney;
                }
            }
        }

        private static void ShowNumber(int number)
        {
            for (int i = 1; i < number; i++)
            {
                Console.Write(i);
                Thread.Sleep(100);
                Console.Write("\b");
            }
            Console.Write("{0} ", number);
        }
    }
}
