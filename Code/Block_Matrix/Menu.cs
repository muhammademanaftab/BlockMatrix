using System;
using System.Collections.Generic;

namespace Block_Matrix_Prj
{
    public class Menu
    {
        private readonly List<Block_Matrix> matrices = new();

        public Menu() { }

        public void Run()
        {
            int choice;
            do
            {
                PrintMenu();
                choice = GetChoice();

                switch (choice)
                {
                    case 0:
                        Console.WriteLine("Exiting...");
                        break;
                    case 1:
                        SetMatrix();
                        break;
                    case 2:
                        PrintMatrix();
                        break;
                    case 3:
                        AddMatrices();
                        break;
                    case 4:
                        MultiplyMatrices();
                        break;
                    case 5:
                        DeleteMatrixAt();
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please choose again.");
                        break;
                }
            } while (choice != 0);
        }

        private static void  PrintMenu()
        {
            Console.WriteLine("\n0. - Quit");
            Console.WriteLine("1. - Set a matrix");
            Console.WriteLine("2. - Print a matrix");
            Console.WriteLine("3. - Add matrices");
            Console.WriteLine("4. - Multiply matrices");
            Console.WriteLine("5. - Delete a matrix");
            Console.Write("Choose: ");
        }

        private static int GetChoice()
        {
            int choice;
            while (!int.TryParse(Console.ReadLine(), out choice) || choice < 0 || choice > 5)
            {
                Console.WriteLine("Invalid choice. Please choose again.");
                Console.Write("Choose: ");
            }
            return choice;
        }

        private void SetMatrix()
        {
            try
            {
                Block_Matrix matrix = TakeInput();
                matrices.Add(matrix);
                Console.WriteLine($"Matrix successfully added at Position: {matrices.Count} " );
            }
            catch (Exception)
            {
                Console.WriteLine("The Size of Matrix must be equal to sum of Block Matrices i.e. n = b1 + b2");
            }
        }

        private void PrintMatrix()
        {
            if (matrices.Count == 0)
            {
                Console.WriteLine("No matrices available to print.");
                return;
            }

            Console.WriteLine("Choose the matrix to print:");
            for (int i = 0; i < matrices.Count; i++)
            {
                Console.WriteLine($" {i + 1}. Matrix {i + 1}");
            }

            int choice = GetMatrixIndex();
            Console.WriteLine(matrices[choice - 1].ToString());
        }

        private void AddMatrices()
        {
            if (matrices.Count < 2)
            {
                Console.WriteLine("At least two matrices are required for addition.");
                return;
            }
            Console.WriteLine("List of Matrices: ");
            for (int i = 0; i < matrices.Count; i++)
            {
                Console.WriteLine($" {i + 1}. Matrix {i + 1}");
            }

            Console.WriteLine("Choose the first matrix from list:");
            int firstIndex = GetMatrixIndex();
            Console.WriteLine("Choose the second matrix from list:");
            int secondIndex = GetMatrixIndex();

            try
            {
                Block_Matrix result = matrices[firstIndex - 1]+(matrices[secondIndex - 1]);
                matrices.Add(result);
                Console.WriteLine($"Matrices added successfully, and stored at position: {matrices.Count}");
            }
            catch (Exception)
            {
                Console.WriteLine("Cannot Add, Both Matrices should have same Size.");
            }
        }

        private void MultiplyMatrices()
        {
            if (matrices.Count < 2)
            {
                Console.WriteLine("At least two matrices are required for multiplication.");
                return;
            }

            Console.WriteLine("List of Matrices: ");
            for (int i = 0; i < matrices.Count; i++)
            {
                Console.WriteLine($" {i + 1}. Matrix {i + 1}");
            }

            Console.WriteLine("Choose the first matrix from list:");
            int firstIndex = GetMatrixIndex();
            Console.WriteLine("Choose the second matrix from list:");
            int secondIndex = GetMatrixIndex();

            try
            {
                Block_Matrix result = matrices[firstIndex - 1]*(matrices[secondIndex - 1]);
                matrices.Add(result);
                Console.WriteLine($"Matrices multiplied successfully, and stored at position: {matrices.Count}.");
            }
            catch (Exception)
            {
                Console.WriteLine("Cannot Multiply, Both Matrices should have same Size.");
            }
        }

        public void DeleteMatrixAt()
        {
            if (matrices.Count == 0)
            {
                Console.WriteLine("No matrices available to delete.");
                return;
            }
            Console.WriteLine("List of Matrices: ");
            for (int i = 0; i < matrices.Count; i++)
            {
                Console.WriteLine($" {i + 1}. Matrix {i + 1}");
            }
            Console.WriteLine("Enter Number of Matrix To Delete From List: ");
            
            if (!int.TryParse(Console.ReadLine(), out int position) || position < 1 || position > matrices.Count)
            {
                Console.WriteLine("Invalid position. Please enter a valid position.");
                return;
            }            

            matrices.RemoveAt(position - 1);
            Console.WriteLine($"Matrix at position {position} deleted successfully.");
        }

        private int GetMatrixIndex()
        {
            int index;
            while (!int.TryParse(Console.ReadLine(), out index) || index < 1 || index > matrices.Count)
            {
                Console.WriteLine("Invalid index. Please choose again.");
                Console.Write("Choose: ");
            }
            return index;
        }

        private static Block_Matrix TakeInput()
        {
            int n, b1, b2;
            bool good;
            bool gd1 = false;
            do
            {
                Console.WriteLine("Enter the size of the matrix (n), such that the sum of the sizes of block matrices (b1, b2) equals n (n must be positive): ");
                n = int.Parse(Console.ReadLine()!);

                Console.WriteLine("Enter the size of the first block matrix (b1): ");
                b1 = int.Parse(Console.ReadLine()!);

                Console.WriteLine("Enter the size of the second block matrix (b2): ");
                b2 = int.Parse(Console.ReadLine()!);

                // Check if n is positive and if it equals the sum of b1 and b2
                if (n > 0 && n == b1 + b2)
                {
                    gd1 = true;
                }
                else
                {
                    Console.WriteLine("Invalid input. Please try again.");
                }
            } while (!gd1);
            List<int> values = new();
            good = (n == b1 + b2);
            if (good)
            {
                Console.WriteLine("Enter Values of First Square Matrix line by line.");
                for (int i = 0; i < b1 * b1; i++)
                {
                    Console.Write($"Enter Element {i+1}: ");
                    bool gd;
                    do
                    {
                        string input = Console.ReadLine()!;
                        gd = int.TryParse(input, out int value);
                        if (gd)
                        {
                            values.Add(value);
                            gd = true;
                        }
                        else
                        {
                            Console.WriteLine("Wrong Input. Please enter an integer.");
                        }

                    } while (!gd);
                    Console.WriteLine();
                }

                Console.WriteLine("Enter Values of Second Square Matrix line by line.");
                for (int i = 0; i < b2 * b2; i++)
                {
                    Console.Write($"Enter Element {i + 1}: ");
                    bool gd;
                    do
                    {
                        string input = Console.ReadLine()!;
                        gd = int.TryParse(input, out int value);
                        if (gd)
                        {
                            values.Add(value);
                            gd = true;
                        }
                        else
                        {
                            Console.WriteLine("Wrong Input. Please enter an integer.");
                        }

                    } while (!gd);
                    Console.WriteLine();

                }
            }

            return new Block_Matrix(values, n, b1, b2);
        }
    }
}
