

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace Project4
{

        internal class Lottoo
        {
            private const int WINNING_MULTIPLIER = 2;
            private const int GRID_SIZE = 3;
            private const int MIN_SIZE = 0;
            private const int MAX_SIZE = 3;
            private const int JACKPOT_MULTIPLIER = 20;
            private const int TOTAL_MONEY = 100;
            private const int MIDDLE_ROW_INDEX = 1;
            private const string YES_TO_PLAY_AGAIN = "yes";

            private int[,] grid = new int[GRID_SIZE, GRID_SIZE];
            private Random random = new Random();
            private int money = TOTAL_MONEY;

            static void Main(string[] args)
            {
                var games = new Lottoo();
                games.Start();
            }

            public void Start()
            {
                Console.WriteLine("Welcome to the Slot Machine Game !");
                while (true)
                {
                    Console.WriteLine($" You have {money}.");
                    Console.WriteLine("Enter Your wager; $");

                    String wagerInput = Console.ReadLine();
                    int wager;

                    if (!int.TryParse(wagerInput, out wager))
                    {
                        Console.WriteLine("Invalid input. Please enter a valid number.");
                        continue;
                    }

                    if (wager == MIN_SIZE)
                    {
                        Console.WriteLine($"!!SORRY YOU DON'T HAVE ANY MONEY !! ; {money}");
                        break;
                    }

                    if (wager <= MIN_SIZE || wager > money)
                    {
                        Console.WriteLine("Please Invalid wager");
                        continue;
                    }


                    money -= wager;
                    SelectMode selectMode = GetSelectMode();
                    FillGrid();
                    OutPutGrid();
                    CheckWinning(wager, selectMode);

                    if (money <= MIN_SIZE)
                    {
                        Console.WriteLine("You have no money left. Game over.");
                        break;
                    }

                    Console.WriteLine("Do you wont to play again ?  yes / no  ");
                    string playAgain = Console.ReadLine().ToLower();

                    if (playAgain != YES_TO_PLAY_AGAIN)
                    {
                        break;
                    }
                }
                Console.WriteLine("Game ended. Thank you for playing!");
            }
            private void FillGrid()
            {
                for (int rows = 0; rows < GRID_SIZE; rows++)
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

            private void CheckWinning(int wager, SelectMode selectMode)
            {
                bool win = false;
                switch (selectMode)
                {
                    case SelectMode.MiddleHorizontal:

                        if (IsRowWinn(MIDDLE_ROW_INDEX))
                        {
                            money += wager * WINNING_MULTIPLIER;
                            Console.WriteLine("Congratulations! You won on the middle horizontal rows!");
                            win = true;
                        }
                        break;

                    case SelectMode.AllHorizontal:

                        for (int rows = 0; rows < GRID_SIZE; rows++)
                        {
                            if (IsRowWinn(rows))
                            {
                                money += wager * WINNING_MULTIPLIER;
                                Console.WriteLine($"Congratulations! You won on rows {rows + 1}!");
                                win = true;
                            }
                        }
                        break;

                    case SelectMode.AllVerticals:

                        for (int colum = 0; colum < GRID_SIZE; colum++)
                        {
                            if (IsColumnWinning(colum))
                            {
                                money += wager * WINNING_MULTIPLIER;
                                Console.WriteLine($"Congratulations! You won on column {colum + 1}!");
                                win = true;
                            }
                        }
                        break;

                    case SelectMode.Diagonals:
                        if (IsDiagonalWinnig())
                        {
                            money += wager * WINNING_MULTIPLIER;
                            Console.WriteLine($"Congratulations! You won on a diagonal!");
                            win = true;
                        }
                        break;
                    case SelectMode.Jackpot:

                        if (IsJackpot())

                        {
                            money += wager * JACKPOT_MULTIPLIER;
                            Console.WriteLine($"JACKPOT! All elements are the same!");
                            win = true;
                        }
                        break;
                }

                if (!win)
                {
                    Console.WriteLine("Sorry, you didn't win this time.");
                }

                Console.WriteLine($"You have ${money} left.");
            }

            private SelectMode GetSelectMode()
            {
                while (true)
                {
                    SelectMode selectMode;
                    Console.WriteLine("Select mode:");
                    Console.WriteLine("1. Middle Horizontal");
                    Console.WriteLine("2. All Horizontal");
                    Console.WriteLine("3. All Verticals");
                    Console.WriteLine("4. Diagonals");
                    Console.WriteLine("5. Jackpot");

                    if (int.TryParse(Console.ReadLine(), out int mode) && Enum.IsDefined(typeof(SelectMode), mode))
                    {
                        return (SelectMode)mode;
                    }
                    Console.WriteLine("Invalid input. Please enter a number between 1 and 5.");
                }

            }
            private bool IsRowWinn(int rows)
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
            private bool IsDiagonalWinnig()
            {
                bool firstDiagonalWinn = true;
                bool secondDiagonalWinn = true;

                int firstGribDiagonal = grid[0, 0];
                int secondGribDiagonal = grid[0, GRID_SIZE - 1];

                for (int i = 0; i < GRID_SIZE; i++)
                {
                    if (grid[i, i] != firstGribDiagonal)
                    {
                        firstDiagonalWinn = false;
                    }
                    if (grid[i, GRID_SIZE - 1 - i] != secondGribDiagonal)
                    {
                        secondDiagonalWinn = false;
                    }

                }
                return firstDiagonalWinn || secondDiagonalWinn;
            }
            private bool IsJackpot()
            {
                int firstElement = grid[0, 0];
                for (int row = 0; row < GRID_SIZE; row++)
                {
                    for (int col = 0; col < GRID_SIZE; col++)
                    {
                        if (grid[row, col] != firstElement)
                        {
                            return false;
                        }
                    }
                }
                return true;
            }
        }

    }





