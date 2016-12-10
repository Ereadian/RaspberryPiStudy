using System;
using System.Threading;

namespace Q3Complex
{
    class MainClass
    {
        public static void Main (string[] args)
        {
            Play(500, 10, 10, 20, 3);
        }

        private static void Play(int baseMoney, int minMoney, int increase, int posibility, int numberOfCardsPerLine)
        {
            Console.WriteLine("You heve ${0} now", baseMoney);
            Random rand = new Random();
            int[,] cards = new int[numberOfCardsPerLine, numberOfCardsPerLine];
            while (baseMoney > 0) 
            {
                int start = GetBet(minMoney, increase, baseMoney);
                if (start < 0)
                {
                    Console.WriteLine("You have ${0}. Please checkout", baseMoney);
                    break;
                }

                for (var y = 0; y < numberOfCardsPerLine; y++)
                {
                    if ((posibility > 0) && (rand.Next(posibility) == 0))
                    {
                        var cardNumber = rand.Next(1, 9);
                        for (var x = 0; x < numberOfCardsPerLine; x++)
                        {
                            cards[y, x] = cardNumber;
                        }
                    }
                    else
                    {
                        for (var x = 0; x < numberOfCardsPerLine; x++)
                        {
                            cards[y,x] = rand.Next(1, 9);
                        }
                    }
                }

                if (posibility > 0)
                {
                    for (var x = 0; x < numberOfCardsPerLine; x++)
                    {
                        if (rand.Next(posibility) == 0)
                        {
                            var cardNumber = rand.Next(1, 9);
                            for (var y = 0; y < numberOfCardsPerLine; y++)
                            {
                                cards[y,x] = cardNumber;
                            }
                        }
                    }

                    if (rand.Next(posibility) == 0)
                    {
                        var cardNumber = rand.Next(1, 9);
                        for (var i = 0; i < numberOfCardsPerLine; i++)
                        {
                            cards[i,i] = cardNumber;
                        }
                    }

                    if (rand.Next(posibility) == 0)
                    {
                        var cardNumber = rand.Next(1, 9);
                        for (var i = 0; i < numberOfCardsPerLine; i++)
                        {
                            cards[i, numberOfCardsPerLine - i - 1] = cardNumber;
                        }
                    }
                }

                for (var y = 0; y < numberOfCardsPerLine; y++)
                {
                    for (var x = 0; x < numberOfCardsPerLine; x++)
                    {
                        ShowNumber(cards[y, x]);
                    }
                    Console.WriteLine();
                }

                var winNumber = GetWinNumber(cards, numberOfCardsPerLine);
                if (winNumber > 0)
                {
                    var won = start * winNumber;
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

        private static int GetWinNumber(int[,] cards, int numberOfCardsPerLine)
        {
            var number = 0;
            bool areSame;
            int current;
            for (var y = 0; y < numberOfCardsPerLine; y++)
            {
                areSame = true;
                current = cards[y, 0];
                for (var x = 1; x < numberOfCardsPerLine; x++)
                {
                    if (cards[y, x] != current)
                    {
                        areSame = false;
                        break;
                    }
                }

                if (areSame)
                {
                    number = number + current;
                }
            }

            for (var x = 0; x < numberOfCardsPerLine; x++)
            {
                areSame = true;
                current = cards[0, x];
                for (var y = 1; y < numberOfCardsPerLine; y++)
                {
                    if (cards[y, x] != current)
                    {
                        areSame = false;
                        break;
                    }
                }
                if (areSame)
                {
                    number = number + current;
                }
            }

            areSame = true;
            current = cards[0, 0];
            for (var i = 0; i < numberOfCardsPerLine; i++)
            {
                if (cards[i, i] != current)
                {
                    areSame = false;
                    break;
                }
            }
            if (areSame)
            {
                number = number + current;
            }

            areSame = true;
            current = cards[0, numberOfCardsPerLine - 1];
            for (var i = 1; i < numberOfCardsPerLine; i++)
            {
                if (cards[i, numberOfCardsPerLine - i - 1] != current)
                {
                    areSame = false;
                    break;
                }
            }
            if (areSame)
            {
                number = number + current;
            }

            return number;
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
