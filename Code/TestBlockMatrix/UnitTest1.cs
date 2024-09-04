using Block_Matrix_Prj;
using System.Runtime.Intrinsics.X86;

namespace TestBlockMatrix
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Create()
        {
            //Checking Exceptions
            Assert.ThrowsException<Block_Matrix.NegativeSizeException>(() => _ = new Block_Matrix(new List<int>(), 0, 0, 0));
            Assert.ThrowsException<Block_Matrix.B1orB2notequalN>(() => _ = new Block_Matrix(new List<int> { 1, 2, 3, 4, 1, 2, 3, 4 }, 4, 2, 1));
            Assert.ThrowsException<Block_Matrix.NegativeSizeException>(() => _ = new Block_Matrix(-1, 0, 0));
            Assert.ThrowsException<Block_Matrix.B1orB2notequalN>(() => _ = new Block_Matrix(4, 2, 1));


            //Creating Matrix
            Block_Matrix two = new Block_Matrix(new List<int> { 1, 1, 2, 3, 4 }, 3, 1, 2);
            Assert.AreEqual(two.getAt(0, 0), 1);
            Assert.AreEqual(two.N, 3);

            Block_Matrix c = new Block_Matrix(new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 1, 2, 3, 4 }, 5, 3, 2); // Adjust the constructor parameters
            Assert.AreEqual(c.N, 5);

            Block_Matrix d = new (5, 4, 1);
            Assert.AreEqual(d.N, 5);

            for (int i =0; i<5; i++)
            {
                for (int j=0; j<5; j++)
                {
                    Assert.AreEqual(d.getAt(i, j), 0);
                }
            }
            //Checking on Large scale matrix
            Block_Matrix h = new Block_Matrix(1000,500,500);
            Assert.AreEqual(h.N, 1000);

        }

        [TestMethod]
        public void Add ()
        {

            //Checking Addition
            Block_Matrix a = new Block_Matrix(new List<int> { 1, 2, 3, 4, 1, 2, 3, 4}, 4, 2, 2); 
            Block_Matrix b = new Block_Matrix(new List<int> { 1, 2, 3, 4, 1, 2, 3, 4}, 4, 2, 2);

            Block_Matrix c = a + b; 
            
            Assert.AreEqual(c.getAt(0, 1), 4); 
            Assert.AreEqual(c.getAt(0, 2), 0);
            Assert.AreEqual(c.getAt(0, 3), 0);
            Assert.AreEqual(c.getAt(0, 0), 2); 
            Assert.AreEqual(c.getAt(1, 0), 6);
            Assert.AreEqual(c.getAt(1, 1), 8);
            Assert.AreEqual(c.getAt(1, 2), 0);
            Assert.AreEqual(c.getAt(1, 3), 0);
            Assert.AreEqual(c.getAt(2, 2), 2);
            Assert.AreEqual(c.getAt(2, 3), 4);
            Assert.AreEqual(c.getAt(3, 2), 6);
            Assert.AreEqual(c.getAt(3, 3), 8);
            

            //Checking Comutative Property on c and f

            Block_Matrix f = b + a; 
            Assert.AreEqual(f.getAt(0, 0), c.getAt(0, 0));
            Assert.AreEqual(f.getAt(0, 1), c.getAt(0, 1));
            Assert.AreEqual(f.getAt(0, 2), c.getAt(0, 2));
            Assert.AreEqual(f.getAt(0, 3), c.getAt(0, 3));
            Assert.AreEqual(f.getAt(1, 0), c.getAt(1, 0));
            Assert.AreEqual(f.getAt(1, 1), c.getAt(1, 1));
            Assert.AreEqual(f.getAt(1, 2), c.getAt(1, 2));
            Assert.AreEqual(f.getAt(1, 3), c.getAt(1, 3));
            Assert.AreEqual(f.getAt(2, 2), c.getAt(2, 2));
            Assert.AreEqual(f.getAt(2, 3), c.getAt(2, 3));
            Assert.AreEqual(f.getAt(3, 2), c.getAt(3, 2));
            Assert.AreEqual(f.getAt(3, 3), c.getAt(3, 3));
            
            
            //Checking Associative Property on a, b, c
            Block_Matrix h = ( a  +  b ) + c ; 
            Block_Matrix g = a  + ( b  +  c ) ; 
            Assert.AreEqual(g.getAt(0, 0), h.getAt(0, 0));
            Assert.AreEqual(g.getAt(0, 1), h.getAt(0, 1));
            Assert.AreEqual(g.getAt(0, 2), h.getAt(0, 2));
            Assert.AreEqual(g.getAt(0, 3), h.getAt(0, 3));
            Assert.AreEqual(g.getAt(1, 0), h.getAt(1, 0));
            Assert.AreEqual(g.getAt(1, 1), h.getAt(1, 1));
            Assert.AreEqual(g.getAt(1, 2), h.getAt(1, 2));
            Assert.AreEqual(g.getAt(1, 3), h.getAt(1, 3));
            Assert.AreEqual(g.getAt(2, 2), h.getAt(2, 2));
            Assert.AreEqual(g.getAt(2, 3), h.getAt(2, 3));
            Assert.AreEqual(g.getAt(3, 2), h.getAt(3, 2));
            Assert.AreEqual(g.getAt(3, 3), h.getAt(3, 3));


            //Checking With Zero Matrix or Identity Matrix

            Block_Matrix zero = new(4, 2, 2);
            Block_Matrix d  = zero + b;
            Assert.AreEqual(d.getAt(0, 0), 1);
            Assert.AreEqual(d.getAt(0, 1), 2);
            Assert.AreEqual(d.getAt(0, 2), 0);
            Assert.AreEqual(d.getAt(0, 3), 0);
            Assert.AreEqual(d.getAt(1, 0), 3);
            Assert.AreEqual(d.getAt(1, 1), 4);
            Assert.AreEqual(d.getAt(1, 2), 0);
            Assert.AreEqual(d.getAt(1, 3), 0);
            Assert.AreEqual(d.getAt(2, 2), 1);
            Assert.AreEqual(d.getAt(2, 3), 2);
            Assert.AreEqual(d.getAt(3, 2), 3);
            Assert.AreEqual(d.getAt(3, 3), 4);


            //Checking addition not compatible

            Block_Matrix e = new Block_Matrix(new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 1, 2, 3, 4 }, 5, 3, 2);

            Assert.ThrowsException<Block_Matrix.MatricesSizeNotEqual>(() => { Block_Matrix result = e + b; });

        }

        [TestMethod]
        public void Mul ()
        {
            //Checking Multiplication
            Block_Matrix a = new Block_Matrix(new List<int> { 1, 2, 3, 4, 1, 2, 3, 4 }, 4, 2, 2);
            Block_Matrix b = new Block_Matrix(new List<int> { 1, 2, 3, 4, 1, 2, 3, 4 }, 4, 2, 2);

            Block_Matrix c = a * b;
            Assert.AreEqual(c.getAt(0, 0), 7);
            Assert.AreEqual(c.getAt(0, 1), 10);
            Assert.AreEqual(c.getAt(0, 2), 0);
            Assert.AreEqual(c.getAt(0, 3), 0);
            Assert.AreEqual(c.getAt(1, 0), 15);
            Assert.AreEqual(c.getAt(1, 1), 22);
            Assert.AreEqual(c.getAt(1, 2), 0);
            Assert.AreEqual(c.getAt(1, 3), 0);
            Assert.AreEqual(c.getAt(2, 2), 7);
            Assert.AreEqual(c.getAt(2, 3), 10);
            Assert.AreEqual(c.getAt(3, 2), 15);
            Assert.AreEqual(c.getAt(3, 3), 22);


            //Checking With Zero Matrix

            Block_Matrix zero = new(4, 2, 2);
            Block_Matrix d = zero * b;
            Assert.AreEqual(d.getAt(0, 0), 0);
            Assert.AreEqual(d.getAt(0, 1), 0);
            Assert.AreEqual(d.getAt(0, 2), 0);
            Assert.AreEqual(d.getAt(0, 3), 0);
            Assert.AreEqual(d.getAt(1, 0), 0);
            Assert.AreEqual(d.getAt(1, 1), 0);
            Assert.AreEqual(d.getAt(1, 2), 0);
            Assert.AreEqual(d.getAt(1, 3), 0);
            Assert.AreEqual(d.getAt(2, 2), 0);
            Assert.AreEqual(d.getAt(2, 3), 0);
            Assert.AreEqual(d.getAt(3, 2), 0);
            Assert.AreEqual(d.getAt(3, 3), 0);


            //Checking with identity Matrix
            Block_Matrix identity = new Block_Matrix(new List<int> { 1, 0, 0, 1, 1, 0, 0, 1 }, 4, 2, 2); 
            Block_Matrix iden = identity * b;
            Assert.AreEqual(iden.getAt(0, 0), 1);
            Assert.AreEqual(iden.getAt(0, 1), 2);
            Assert.AreEqual(iden.getAt(0, 2), 0);
            Assert.AreEqual(iden.getAt(0, 3), 0);
            Assert.AreEqual(iden.getAt(1, 0), 3);
            Assert.AreEqual(iden.getAt(1, 1), 4);
            Assert.AreEqual(iden.getAt(1, 2), 0);
            Assert.AreEqual(iden.getAt(1, 3), 0);
            Assert.AreEqual(iden.getAt(2, 2), 1);
            Assert.AreEqual(iden.getAt(2, 3), 2);
            Assert.AreEqual(iden.getAt(3, 2), 3);
            Assert.AreEqual(iden.getAt(3, 3), 4);


            //Checking commutative property 
            Block_Matrix h = b * identity;
            Assert.AreEqual(h.getAt(0, 0), iden.getAt(0, 0));
            Assert.AreEqual(h.getAt(0, 1), iden.getAt(0, 1));
            Assert.AreEqual(h.getAt(0, 2), iden.getAt(0, 2));
            Assert.AreEqual(h.getAt(0, 3), iden.getAt(0, 3));
            Assert.AreEqual(h.getAt(1, 0), iden.getAt(1, 0));
            Assert.AreEqual(h.getAt(1, 1), iden.getAt(1, 1));
            Assert.AreEqual(h.getAt(1, 2), iden.getAt(1, 2));
            Assert.AreEqual(h.getAt(1, 3), iden.getAt(1, 3));
            Assert.AreEqual(h.getAt(2, 2), iden.getAt(2, 2));
            Assert.AreEqual(h.getAt(2, 3), iden.getAt(2, 3));
            Assert.AreEqual(h.getAt(3, 2), iden.getAt(3, 2));
            Assert.AreEqual(h.getAt(3, 3), iden.getAt(3, 3));

            //Checking Associativity
            Block_Matrix i = (a * b) * c;
            Block_Matrix j = a * (b * c);
            Assert.AreEqual(i.getAt(0, 0), j.getAt(0, 0));
            Assert.AreEqual(i.getAt(0, 1), j.getAt(0, 1));
            Assert.AreEqual(i.getAt(0, 2), j.getAt(0, 2));
            Assert.AreEqual(i.getAt(0, 3), j.getAt(0, 3));
            Assert.AreEqual(i.getAt(1, 0), j.getAt(1, 0));
            Assert.AreEqual(i.getAt(1, 1), j.getAt(1, 1));
            Assert.AreEqual(i.getAt(1, 2), j.getAt(1, 2));
            Assert.AreEqual(i.getAt(1, 3), j.getAt(1, 3));
            Assert.AreEqual(i.getAt(2, 2), j.getAt(2, 2));
            Assert.AreEqual(i.getAt(2, 3), j.getAt(2, 3));
            Assert.AreEqual(i.getAt(3, 2), j.getAt(3, 2));
            Assert.AreEqual(i.getAt(3, 3), j.getAt(3, 3));


            //Checking Size not equal exception
            Block_Matrix f = new Block_Matrix(new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 1, 2, 3, 4 }, 5, 3, 2);
            Assert.ThrowsException<Block_Matrix.MatricesSizeNotEqual>(() => { var result = f + b; });

            //Checking Block Matrices size not equal 
            Block_Matrix g = new Block_Matrix(new List<int> { 1, 2, 3, 4, 1, 2, 3, 4, 5, 6, 7, 8, 9 }, 5, 2, 3);
            Assert.ThrowsException<Block_Matrix.BlockMatricesSizeNotEqual>(() => { var result = g + f; });


        }

        [TestMethod]
        public void CheckingWZeroWValuesMatrix()
        {
            //Setting Matrix Checking...
            List<int> vec = new List<int>() { 1, 2, 3, 5, 1};
            Block_Matrix a = new Block_Matrix(3, 1, 2);

            Assert.AreEqual(a.getAt(0, 0), 0);
            Assert.AreEqual(a.getAt(1, 1), 0);
            Assert.AreEqual(a.getAt(1, 2), 0);
            Assert.AreEqual(a.getAt(2, 1), 0);
            Assert.AreEqual(a.getAt(2, 2), 0);
            a = new Block_Matrix(vec, 3, 1, 2);
            Assert.AreEqual(a.getAt(0, 0), 1);
            Assert.AreEqual(a.getAt(1, 1), 2);
            Assert.AreEqual(a.getAt(1, 2), 3);
            Assert.AreEqual(a.getAt(2, 1), 5);
            Assert.AreEqual(a.getAt(2, 2), 1);

            Assert.ThrowsException<Block_Matrix.BlockMatricesSizeNotEqual>(() => new Block_Matrix(vec, 2, 1, 1));
        }

        [TestMethod]
        public void getAt ()
        {
            //Checking getAt method that gives the values from list by inserting indexes of an array

            Block_Matrix a = new Block_Matrix(new List<int> { 1, 2, 3, 4, 1, 2, 3, 4 }, 4, 2, 2);
        
            Assert.AreEqual(a.getAt(0, 0), 1);
            Assert.AreEqual(a.getAt(0, 1), 2);
            Assert.AreEqual(a.getAt(1, 0), 3);
            Assert.AreEqual(a.getAt(1, 1), 4);
            Assert.AreEqual(a.getAt(2, 2), 1);
            Assert.AreEqual(a.getAt(2, 3), 2);
            Assert.AreEqual(a.getAt(3, 2), 3);
            Assert.AreEqual(a.getAt(3, 3), 4);


        }

    }
}
    
