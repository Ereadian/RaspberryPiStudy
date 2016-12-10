using System;
using System.Threading;

namespace Q1Simple
{
	class MainClass
	{
		public static void Main (string[] args)
		{
            Play(500, 10, 10, 3);
		}

        private static void Play(int baseMoney, int minMoney, int increase, int posibility)
        {
            Console.WriteLine("You heve ${0} now", baseMoney);
            Random rand = new Random();
            while (baseMoney > 0) 
            {
                int start = GetBet(minMoney, increase, baseMoney);
                if (start < 0)
                {
                    Console.WriteLine("You have ${0}. Please checkout", baseMoney);
                    break;
                }

                int a = rand.Next(1, 9);
                int b = rand.Next(1, 9);
                int c = rand.Next(1, 9);

                if ((posibility > 0) && (rand.Next(posibility) == 0))
                {
                    b = a;
                    c = a;
                }

                ShowNumber(a);
                ShowNumber(b);
                ShowNumber(c);
                Console.WriteLine();

                if ((a == b) && (b == c))
                {
                    baseMoney = baseMoney + start;
                    Console.WriteLine("Won ${0}. You have ${1} now", start, baseMoney);
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
