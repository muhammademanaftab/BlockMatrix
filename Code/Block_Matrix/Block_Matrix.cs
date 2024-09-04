using System;

namespace Block_Matrix_Prj
{

    public class Block_Matrix
    {

        #region Exceptions
        public class MatricesSizeNotEqual : Exception { }; // In Addition or Multiplication, If matrices size not equal 
        public class BlockMatricesSizeNotEqual : Exception { }; // In Addition or Multiplication, If Block matrices size not equal 
        public class IndexNotExist : Exception { };// In Getat, if index not exist
        public class B1orB2notequalN : Exception { };// In Constructor, If b1+b2!=n
        public class NegativeSizeException : Exception { };// In Constructor, either b1 or b2 or n  is negative

        #endregion

        #region Attribute
        private int n;
        private List<int> x = new();
        private int b1;
        private int b2;
        #endregion

        #region Getter & Setter
        public int N { get => n; set => n = value; }
        private int B1 { get => b1;}
        private int B2 { get => b2;}

        #endregion

        #region Constructor
        public Block_Matrix(List<int> x, int n, int b1, int b2)
        {           
            if (b1 <= 0 || b2 <= 0 || n<= 0) { throw new NegativeSizeException(); }
            if (b1 + b2 != n)
            {
                throw new B1orB2notequalN();
            }
            if (x.Count != b1 * b1 + b2 * b2)
            {
                throw new BlockMatricesSizeNotEqual();
            }

            this.x = x;
            this.n = n;
            this.b1 = b1;
            this.b2 = b2;
        }

        public Block_Matrix(int n, int b1, int b2)
        {
            if (b1 <= 0 || b2 <= 0 || n <= 0) { throw new NegativeSizeException(); }
            if (b1 + b2 != n)
            {
                throw new B1orB2notequalN();
            }
            this.n = n;
            this.b1 = b1;
            this.b2 = b2;
            for (int i = 0; i < b1*b1 + b2*b2; i++)
            {
                this.x.Add(0);
            }
        }
        #endregion region

        #region Methods
        private int ind(int i, int j)
        {
            
            if (i < b1 && j < b1)
            {
                return j + (i * b1);
            }

            else
            {
                return (b1 * b1 + (i - b1) * (n - b1) + (j - b1));
            }

        }

        public double getAt(int i, int j)
        {
            if (i < 0 || i >= n || j < 0 || j >= n)
            {
                throw new IndexNotExist();
            }

            if (i < b1 && j < b1) 
            {
                return x[ind(i, j)];
            }
            else if (i >= b1 && j >= b1 && i < N && j < N)
            {
                return x[ind(i , j)];
            }

            return 0;
        }

        public override string ToString()
        {
            string result = "";
            for (int i = 0; i < N; ++i)
            {
                string separator = "";
                for (int j = 0; j < N; ++j)
                {
                    result += separator + getAt(i, j).ToString();
                    separator = "\t";
                }
                result += '\n';
            }
            return result;
        }

        #endregion

        #region Operators
        public static Block_Matrix operator +(Block_Matrix matrix1, Block_Matrix matrix2)
        {
            if (matrix1.N != matrix2.N)
            {
                throw new MatricesSizeNotEqual();
            }

            if (matrix1.b1 != matrix2.b1 || matrix1.b2 != matrix2.b2)
            {
                throw new BlockMatricesSizeNotEqual();
            }

            List<int> resultValues = new List<int>(matrix1.x.Count); 
            for (int i = 0; i < matrix1.x.Count; i++)
            {
                resultValues.Add(matrix1.x[i] + matrix2.x[i]); 
            }

            return new Block_Matrix(resultValues, matrix1.N, matrix1.b1, matrix1.b2); 
        }


        public static Block_Matrix operator *(Block_Matrix matrix1, Block_Matrix matrix2)
        {
            if (matrix1.N != matrix2.N)
            {
                throw new MatricesSizeNotEqual();
            }

            if (matrix1.B1 != matrix2.B1 || matrix1.B2 != matrix2.B2)
            {
                throw new BlockMatricesSizeNotEqual();
            }

            List<int> resultValues = new List<int>(matrix1.x);
            Block_Matrix result = new Block_Matrix(resultValues, matrix1.N, matrix1.B1, matrix1.B2);

            // First Square Matrix Multiplication
            for (int i = 0; i < matrix1.B1; i++)
            {
                for (int j = 0; j < matrix1.B1; j++)
                {
                    result.x[matrix1.ind(i, j)] = 0;
                    for (int k = 0; k < matrix1.B1; k++)
                    {
                        result.x[matrix1.ind(i, j)] += matrix1.x[matrix1.ind(i, k)] * matrix2.x[matrix1.ind(k, j)];
                    }
                }
            }

            // Second Square Matrix Multiplication
            for (int i = matrix1.B1; i < matrix1.N; i++)
            {
                for (int j = matrix1.B1; j < matrix1.N; j++)
                {
                    result.x[matrix2.ind(i, j)] = 0;
                    for (int k = matrix1.B1; k < matrix1.N; k++)
                    {
                        result.x[matrix2.ind(i, j)] += matrix1.x[matrix2.ind(i, k)] * matrix2.x[matrix2.ind(k, j)];
                    }
                }
            }
            return result;
        }

        #endregion

        #region ExtraFunctionsForHelp
        //Extra Function, For Checking List Values.
        public void print ()
        {
            for (int i=0; i<x.Count; i++)
            {
                Console.WriteLine(x[i]);
            }
        }

        #endregion
    }
}
