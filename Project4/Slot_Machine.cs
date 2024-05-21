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
        private const int WINNING_MULTIPLIER = 2;
        private const int GRID_SIZE = 3;
        private const int MIN_SIZE = 1;
        private const int MAX_SIZE = 6;
        private int money = 100;
        private int[,] grid = new int[GRID_SIZE, GRID_SIZE];
        private Random random = new Random();



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
                int selectMode = GetSelectMode();
                FillGrid();
                OutPutGrid();
                CheckWinning(wager, selectMode);
                Console.WriteLine("Do you wont to play again ?  yes / no  ");
                string playAgain = Console.ReadLine().ToLower();

                if (playAgain != "yes")
                {
                    break;
                    Console.WriteLine("Game ende");
                }
                Console.WriteLine("Game ended. Thank you for playing!");
            }

        }
        private void FillGrid()
        {
            for (int rows = 0; rows < 3; rows++)
            {
                for (int column = 0; column < GRID_SIZE; column++)
                {
                    grid[rows, column] = GetRandomNumeber();
                }
            }

        }
        private int GetRandomNumeber()
        {
            return random.Next(MIN_SIZE, MAX_SIZE);
        }

        private void OutPutGrid()
        {
            Console.WriteLine("Current grid:");

            for (int rows = 0; rows < GRID_SIZE; rows++)
            {
                for (int column = 0; column < GRID_SIZE; column++)
                {
                    Console.Write(grid[rows, column] + " ");
                }
                Console.WriteLine();
            }

        }

        private void CheckWinning(int wager, int selectMode)
        {
            bool win = false;
            if (selectMode == 1)
            {
                if (IsRow(1))
                {
                    money += wager * WINNING_MULTIPLIER;
                    Console.WriteLine("Congratulations! You won on the middle horizontal row!");
                    win = true;
                }
            }
            if (selectMode == 2) 
            {
                for (int row = 0; row < GRID_SIZE; row++)
                {
                    if (IsRow(row))
                    {
                        money += wager * WINNING_MULTIPLIER;
                        Console.WriteLine($"Congratulations! You won on row {row + 1}!");
                        win = true;
                    }
                }
            }
            else if (selectMode == 3)
            {
                for (int col = 0; col < GRID_SIZE; col++)
                {
                    if (IsColumnWinning(col))
                    {
                        money += wager * WINNING_MULTIPLIER;
                        Console.WriteLine($"Congratulations! You won on column {col + 1}!");
                        win = true;
                    }
                }
            }

            if (!win)
            {
                Console.WriteLine("Sorry, you didn't win this time.");
            }

            Console.WriteLine($"You have ${money} left.");
        }


        private int GetSelectMode()
        {
            while (true)
            {
                int selectMode;
                Console.WriteLine("Select mode:");
                Console.WriteLine("1. Middle Horizontal");
                Console.WriteLine("2. All Horizontal");
                Console.WriteLine("3. All Verticals");

                if (int.TryParse(Console.ReadLine(), out selectMode) && selectMode >= 1 && selectMode <= 3)
                {
                    return selectMode;
                }
                Console.WriteLine("Invalid input. Please enter a number between 1 and 3.");
            }

        }
        private bool IsRow(int rows)
        {
            for (int col = 1; col < GRID_SIZE; col++)
            {
                if (grid[rows, col] != grid[rows, col - 1])
                {
                    return false;
                }
            }
            return true;
        }
        private bool IsColumnWinning(int colum)
        {
            for (int row = 1; row < GRID_SIZE; row++)
            {
                if (grid[row, colum] != grid[row - 1, colum])
                {
                    return false;
                }
            }
            return true;
        }
    }







