﻿using System;
using System.Collections;
using System.Collections.Generic;

namespace SolverCore
{
    public class SymmetricSparseRowMatrix : IMatrix, ILinearOperator
    {
        private double[] a;//значения
        private int[] ja;//положение ненулевых элементов в строке(индекс j)
        private int[] ia;//количество ненулевых элементов в строк

        //ia1- первый элемент в строке
        //ia2 - последний элемент в строке или первый элемент следующий строки

        //конструктор
        public SymmetricSparseRowMatrix(double[] a, int[] ja, int[] ia)
        {
            if (a == null)
            {
                throw new ArgumentNullException(nameof(a));
            }

            if (ja == null)
            {
                throw new ArgumentNullException(nameof(ja));
            }

            if (ia == null)
            {
                throw new ArgumentNullException(nameof(ia));
            }

            if (a.Length != ja.Length)
            {
                throw new ArgumentNullException("a.size != ja.size", nameof(a));
            }

            if (a.Length != ia[ia.Length - 1])
            {
                throw new ArgumentNullException("a.size != ia.[size_matrix]", nameof(ia));
            }
            var size = ia.Length;
            this.ia = new int[size];
            ia.CopyTo(this.ia, 0);

            size = ja.Length;
            this.ja = new int[size];
            this.a = new double[size];
            a.CopyTo(this.a, 0);
            ja.CopyTo(this.ja, 0);
        }

        public SymmetricSparseRowMatrix(int size, int size_a)
        {
            ia = new int[size + 1];
            ja = new int[size_a];
            a = new double[size_a];
        }

        //получение элемента по индексу
        public double this[int i, int j] {
            get
            {
                try
                {
                    if (j > i)
                    {
                        var k = i;
                        i = j;
                        j = k;
                    }
                    var ia1 = ia[i];
                    var ia2 = ia[i + 1];
                    var m = Array.IndexOf(ja, j, ia1, ia2 - ia1);

                    return m == -1 ? 0.0 : a[m];

                }
                catch (IndexOutOfRangeException)
                {
                    throw new IndexOutOfRangeException();
                }
            }
            // set => throw new NotImplementedException();
        }

        //размер
        public int Size => ia.Length - 1;
        //диагональ
        public IVector Diagonal
        {
            get
            {
                var diagonal = new Vector(Size);

                for (int i = 0; i < Size; i++)
                {
                    int ia1 = ia[i];
                    int ia2 = ia[i + 1];

                    for (; ia1 < ia2; ia1++)
                    {
                        if (ja[ia1] == i)
                        {
                            diagonal[i] = a[ia1];
                        }
                    }
                }

                return diagonal;
            }
        }
        public ILinearOperator Transpose => this;

        //заполнение
        public void Fill(FillFunc elems)
        {
            if (elems == null)
            {
                throw new ArgumentNullException(nameof(elems));
            }
            int i = 0, j = 0;
            ia[i] = 0;
            ia[i + 1] = 0;
            foreach (var item in this)
            {
                ja[j] = item.col <= i ? item.col : throw new ArgumentNullException("col>row, i=" + i.ToString() + " j=" + j.ToString(), nameof(elems));
                a[j] = elems(item.row, item.col);
                j++;
                if (item.row != i)
                {
                    i++;
                    ia[i] = item.row - i == 0 && i < Size ? ia[i - 1] : throw new ArgumentNullException("matrix[i,_]=0 || i>size, i=" + i.ToString() + " j=" + j.ToString(), nameof(elems));
                }
                ia[i + 1]++;
            }
        }
        //коллекциятолько (элементы нижнего треугольника)
        public IEnumerator<(double value, int row, int col)> GetEnumerator()
        {
            for (int i = 0; i < Size; i++)
            {
                var ia1 = ia[i];
                var ia2 = ia[i + 1];
                for (; ia1 < ia2; ia1++)
                {
                    yield return (a[ia1], i, ja[ia1]);
                }
            }
        }
        //умножение на нижний треугольник
        public IVector LMult(IVector vector, bool UseDiagonal, DiagonalElement diagonalElement = DiagonalElement.One)
        {
            if (vector == null)
            {
                throw new ArgumentNullException(nameof(vector));
            }
            if (vector.Size != Size)
            {
                throw new RankException();
            }
            var result = new Vector(Size);
            for (int i = 0; i < Size; i++)
            {
                double sum = 0;
                var ia1 = ia[i];
                var ia2 = ia[i + 1];
                for (; i > ja[ia1] && ia1 < ia2; ia1++)
                {
                    var j = ja[ia1];
                    sum += a[ia1] * vector[j];
                }
                if (i == ja[ia1])
                {
                    sum += UseDiagonal ? a[ia1] * vector[i]: (double)diagonalElement * vector[i];
                }
                else
                {
                    throw new ArgumentNullException("matrix[i,i]=0, i = " + i.ToString(), nameof(a));
                }
                result[i] = sum;
            }
            return result;
        }
        //прямой ход
        public IVector LSolve(IVector vector, bool UseDiagonal)
        {
            if (vector == null)
            {
                throw new ArgumentNullException(nameof(vector));
            }

            if (vector.Size != Size)
            {
                throw new RankException();
            }
            double sum = 0;
            var result = vector.Clone();
            for (int i = 0; i < Size; i++)
            {
                var ia1 = ia[i];
                var ia2 = ia[i + 1];
                int j;
                sum = 0;
                for (; ja[ia1] < i && ia1 < ia2; ia1++)
                {
                    j = ja[ia1];
                    sum += result[j] * a[ia1];
                }
                if (i == ja[ia1] && ia1 < ia2)
                {
                    result[i] = UseDiagonal ? (result[i] - sum) / a[ia1] : result[i] - sum;
                } 
                else
                {
                    throw new ArgumentNullException("matrix[i,i]=0, i = " + i.ToString(), nameof(a));
                }
            }
            return result;
        }
        //умножение
        public IVector Multiply(IVector vector)
        {
            if (vector == null)
            {
                throw new ArgumentNullException(nameof(vector));
            }

            if (vector.Size != Size)
            {
                throw new RankException();
            }
            var result = new Vector(Size);
            for (int i = 0; i < Size; i++)
            {
                var ia1 = ia[i];
                var ia2 = ia[i + 1];
                int j;
                for (; ia1 < ia2 - 1; ia1++)
                {
                    j = ja[ia1];
                    result[j] += a[ia1] * vector[i];
                    result[i] += a[ia1] * vector[j];
                }
                j = ja[ia1];
                result[i] += a[ia1] * vector[j];
            }
            return result;
        }
        //умножение на верхний треугольник
        public IVector UMult(IVector vector, bool UseDiagonal,  DiagonalElement diagonalElement = DiagonalElement.One)
        {
            if (vector == null)
            {
                throw new ArgumentNullException(nameof(vector));
            }

            if (vector.Size != Size)
            {
                throw new RankException();
            }
            var result = new Vector(Size);
            for (int i = Size - 1; i >= 0; i--)
            {
                var ia1 = ia[i];
                var ia2 = ia[i + 1] - 1;
                result[i] += UseDiagonal ?  a[ia2] * vector[i] : (double)diagonalElement * vector[i];
                for (ia2--; ia1 <= ia2; ia2--)
                {
                    var j = ja[ia2];
                    result[j] += a[ia2] * vector[i];
                }
            }
            return result;
        }

        public IVector USolve(IVector vector, bool UseDiagonal)
        {
            if (vector == null)
            {
                throw new ArgumentNullException(nameof(vector));
            }

            if (vector.Size != Size)
            {
                throw new RankException();
            }
            var result = vector.Clone();
            var di = Diagonal;
            for (int i = Size - 1; i >= 0; i--)
            {
                var ia1 = ia[i];
                var ia2 = ia[i + 1];
                int j;
                di[i] = UseDiagonal ? di[i] : 1.0;
                for (; ja[ia1] < i && ia1 < ia2; ia1++)
                {
                    j = ja[ia1];
                    result[j] -= result[i] * a[ia1] / di[i] ;//??????
                }
                if (i == ja[ia1] && ia1 < ia2)
                {
                    result[i] =  result[i] / di[i];
                }
                else
                {
                    throw new ArgumentNullException("matrix[i,i]=0, i = " + i.ToString(), nameof(a));
                }
            }
            return result;
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
