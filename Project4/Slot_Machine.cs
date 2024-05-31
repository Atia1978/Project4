using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace Project4
{
    internal class Slot_Machine
    {
        private int[,] grid = new int[3, 3];
        private Random random = new Random();
        private int money = 100;


        public void Start()
        {
            Console.WriteLine("Welcome to the Slot Machine Game !");
            while (true)
            {
                Console.WriteLine($" You have {money}.");
                Console.WriteLine("Enter Your wager; $");
                int wager = int.Parse(Console.ReadLine());

                if (wager <= 0 || wager > money)
                {
                    Console.WriteLine("Please Invalid wager");
                    continue;
                }
                if (wager == 0)
                {
                    Console.WriteLine($"!!SORRY YOU DON'T HAVE ANY MONEY !! ; {money}");
                }
                money -= wager;

                FillGrid();
                OutPutGrid();
                CheckWinning(wager);
                Console.WriteLine("Do you wont to play again ?  yes / no  ");
                string playAgain = Console.ReadLine().ToLower();

                if (playAgain != "yes")
                {
                    break;
                    Console.WriteLine("Game ende");
                }
            }

        }
        private void FillGrid()
        {
            for (int vertical = 0; vertical < 3; vertical++)
            {
                for (int horizontal = 0; horizontal < 3; horizontal++)
                {
                    grid[vertical, horizontal] = GetRandomNumeber();
                }
            }

        }
        private int GetRandomNumeber()
        {
            return random.Next(1, 6);
        }

        private void OutPutGrid()
        {
            Console.WriteLine("Current grid:");

            for (int rows = 0; rows < 3; rows++)
            {
                for (int column = 0; column < 3; column++)
                {
                    Console.Write(grid[rows, column] + " ");
                }
                Console.WriteLine();
            }

        }

        private void CheckWinning(int wager)
        {
            bool win = false;

            for (int rows = 0; rows < 3; rows++)
            {
                if (grid[rows, 0] == grid[rows, 1] && grid[rows, 1] == grid[rows, 2])
                {
                    money += 2 * wager;
                    Console.WriteLine($"Congratulations! You won on row {rows + 1}");
                    Console.WriteLine( "Your Gwinnin " +( money += 2 * wager));
                    win = true;

                }
            }
            for (int column = 0; column < 3; column++)
            {
                if (grid[0, column] == grid[1, column] && grid[1, column] == grid[2, column])
                {
                    money += 2 * wager;
                    Console.WriteLine($"Congratulations! You won on column {column + 1}!");
                    Console.WriteLine("Your Gwinnin " + (money += 2 * wager));
                    win = true;
                }
            }
            if (grid[0, 0] == grid[1, 1] && grid[1, 1] == grid[2, 2] || grid[0, 2] == grid[1, 1] && grid[1, 1] == grid[2, 0])
            {
                money += 2 * wager;
                Console.WriteLine("Congratulations! You won on the main diagonal!");
                Console.WriteLine("Your Gwinnin " + (money += 2 * wager));
                win = true;
            }
            if (!win)
            {
                Console.WriteLine("Sorry, you didn't win this time.");
                Console.WriteLine($" You have {money}");
            }

        }
    }

}

