using System;
using System.Runtime.Serialization;

namespace MatrixLibrary
{
    [Serializable]
    public class MatrixException : Exception
    {
        public MatrixException()
        {

        }

        public MatrixException(string message): base(message)
        {

        }

        public MatrixException(string message, Exception innerException) : base(message, innerException)
        {

        }

        protected MatrixException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }


    }

    public class Matrix : ICloneable
    {
        double[,] array = { {0,0,0 },{0,0,0 }, { 0,0,0} };
        /// <summary>
        /// Number of rows.
        /// </summary>
        public int Rows
        {
            get
            {
                if (Array.GetLength(0) <= 0)
                {
                    dynamic d = new IndexOutOfRangeException("index");
                    throw d;
                }
                return Array.GetLength(0);
            }
        }

        /// <summary>
        /// Number of columns.
        /// </summary>
        public int Columns
        {
            get
            {
                if (Array.GetLength(1) <= 0)
                {
                    dynamic d = new IndexOutOfRangeException("index");
                    throw d;
                }
                return Array.GetLength(1);
            }
        }

        /// <summary>
        /// An array of floating-point values that represents the elements of this Matrix.
        /// </summary>
        public double[,] Array
        {
            get => array;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Matrix"/> class.
        /// </summary>
        /// <param name="rows"></param>
        /// <param name="columns"></param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public Matrix(int rows, int columns)
        {
            if (rows <= 0 || columns <= 0)
                throw new ArgumentOutOfRangeException("rows","are less or equal than 0");
            
            array = new double[rows, columns];


        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Matrix"/> class with the specified elements.
        /// </summary>
        /// <param name="array">An array of floating-point values that represents the elements of this Matrix.</param>
        /// <exception cref="ArgumentNullException">Thrown when array is null.</exception>
        public Matrix(double[,] array)
        {
            this.array = array ?? throw new ArgumentNullException("array");
        }

        /// <summary>
        /// Allows instances of a Matrix to be indexed just like arrays.
        /// </summary>
        /// <param name="row"></param>
        /// <param name="column"></param>
        /// <exception cref="ArgumentException"></exception>
        public double this[int row, int column]
        {

            get => (row > Rows - 1 || row < 0 || column > Columns - 1|| column < 0) ? throw new ArgumentException("Argument is out of range") : Array[row, column];
            set
            {
                if (row > Rows - 1 || row < 0 || column > Columns - 1 || column < 0)
                    throw new ArgumentException("Argument is out of range");
                else
                    Array[row, column] = value;
            }
        }

        /// <summary>
        /// Creates a deep copy of this Matrix.
        /// </summary>
        /// <returns>A deep copy of the current object.</returns>
        public object Clone()
        {
            Matrix newMatrix = new Matrix(Rows, Columns)
            {
                array = array
            };

            return newMatrix;
        }

        /// <summary>
        /// Adds two matrices.
        /// </summary>
        /// <param name="matrix1"></param>
        /// <param name="matrix2"></param>
        /// <returns>New <see cref="Matrix"/> object which is sum of two matrices.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="MatrixException"></exception>
        public static Matrix operator +(Matrix matrix1, Matrix matrix2)
        {

            if (matrix2 == null || matrix1 == null)
                throw new ArgumentNullException("matrix1");

            if (matrix1.Rows != matrix2.Rows || matrix1.Columns != matrix2.Columns)
                throw new MatrixException("The matrix has the wrong dimensions for the operation");

            return matrix1.Add(matrix2);
            
        }

        /// <summary>
        /// Subtracts two matrices.
        /// </summary>
        /// <param name="matrix1"></param>
        /// <param name="matrix2"></param>
        /// <returns>New <see cref="Matrix"/> object which is subtraction of two matrices</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="MatrixException"></exception>
        public static Matrix operator -(Matrix matrix1, Matrix matrix2)
        {

            if (matrix1 == null)
                throw new ArgumentNullException("matrix1");



            return matrix1.Subtract(matrix2);
        }

        /// <summary>
        /// Multiplies two matrices.
        /// </summary>
        /// <param name="matrix1"></param>
        /// <param name="matrix2"></param>
        /// <returns>New <see cref="Matrix"/> object which is multiplication of two matrices.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="MatrixException"></exception>
        public static Matrix operator *(Matrix matrix1, Matrix matrix2)
        {
            if (matrix1 == null)
                throw new ArgumentNullException("matrix1");

            return matrix1.Multiply(matrix2);
        }

        /// <summary>
        /// Adds <see cref="Matrix"/> to the current matrix.
        /// </summary>
        /// <param name="matrix"><see cref="Matrix"/> for adding.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="MatrixException"></exception>
        public Matrix Add(Matrix matrix)
        {
            if (matrix == null)
                throw new ArgumentNullException("matrix");

            if (matrix.Rows != Array.GetLength(0) || matrix.Columns != Array.GetLength(1))
                throw new MatrixException("The matrix has the wrong dimensions for the operation");

            Matrix matrix1 = new Matrix(Rows, Columns);

            for (int i = 0; i < matrix1.Rows; i++)
            {
                for (int j = 0; j < matrix1.Columns; j++)
                {
                    matrix1[i, j] = Array[i, j] + matrix[i, j];
                }
            }

            return matrix1;
        }

        /// <summary>
        /// Subtracts <see cref="Matrix"/> from the current matrix.
        /// </summary>
        /// <param name="matrix"><see cref="Matrix"/> for subtracting.</param>
        /// <exception cref="ArgumentNullException">Thrown when parameter is null.</exception>
        /// <exception cref="MatrixException">Thrown when the matrix has the wrong dimensions for the operation.</exception>
        /// <returns><see cref="Matrix"/></returns>
        public Matrix Subtract(Matrix matrix)
        {

            if (matrix == null)
                throw new ArgumentNullException("matrix");

            if (matrix.Rows != Array.GetLength(0) || matrix.Columns != Array.GetLength(1))
                throw new MatrixException("The matrix has the wrong dimensions for the operation");
            Matrix matrix1 = new Matrix(Rows,Columns);
            for (int i = 0; i < matrix1.Rows; i++)
            {
                for (int j = 0; j < matrix1.Columns; j++)
                {
                    matrix1[i, j] = Array[i, j] - matrix[i, j];
                }
            }

            return matrix1;
        }

        /// <summary>
        /// Multiplies <see cref="Matrix"/> on the current matrix.
        /// </summary>
        /// <param name="matrix"><see cref="Matrix"/> for multiplying.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="MatrixException"></exception>
        public Matrix Multiply(Matrix matrix)
        {
            if (matrix == null)
                throw new ArgumentNullException("matrix");


            if (Rows <= 0 || Columns <= 0)
                throw new ArgumentOutOfRangeException("matrix", "are less or equal than 0");

            if (Columns != matrix.Rows)
                throw new MatrixException("The matrix has the wrong dimensions for the operation");


            int r1 = Rows, c1 = Columns;
            int c2 = matrix.Columns;

            Matrix matrix1 = new Matrix(r1, c2);

            for (int i = 0; i < r1; i++)
            {
                for (int j = 0; j < c2; j++)
                {
                    for (int k = 0; k < c1; k++)
                    {
                        matrix1[i, j] += Array[i, k] * matrix[k, j];
                    }
                }
            }

            return matrix1;
        }

        /// <summary>
        /// Tests if <see cref="Matrix"/> is identical to this Matrix.
        /// </summary>
        /// <param name="obj">Object to compare with. (Can be null)</param>
        /// <returns>True if matrices are equal, false if are not equal.</returns>
        /// <exception cref="InvalidCastException">Thrown when object has wrong type.</exception>
        /// <exception cref="MatrixException">Thrown when matrices are incomparable.</exception>
        public override bool Equals(object obj)
        {
            Matrix result;
            if (obj == null)
                return false;
            else
                result = obj as Matrix;

            if(!(obj is Matrix)) 
                return false;

            
            if (result.Rows != Array.GetLength(0) || result.Columns != Array.GetLength(1))
                return false;
            
            for (int i = 0; i < result.Rows; i++)
            {
                
                for(int j = 0; j < result.Columns; j++)
                {
                    if (Array[i, j] != result[i, j])
                        return false;
                }
            }

            return true;


        }

        public override int GetHashCode() => this.Array.GetHashCode();
        
    }
}
