﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SolverCore;
using SolverCore.Solvers;
using SolverCore.Methods;
using Extensions;

using Xunit;
using Xunit.Abstractions;

namespace Methods
{
    public class TestsCGMMethod
    {
        private readonly ITestOutputHelper _testOutputHelper;
        IMethod Method;
        ILogger Logger;
        private double[,] _matrix;
        LoggingSolver loggingSolver;

        public TestsCGMMethod(ITestOutputHelper testOutputHelper)
        {
            Method = new CGM();
            Logger = new FakeLog();
            loggingSolver = new LoggingSolver(Method, Logger);
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public void TestAlgorithm()
        {
            _matrix = new double[3, 3] { { 3, 1, 1 },
                                             { 0, 5, 1 },
                                             { 2, 0, 3 } };

            IVector resultActual = new Vector(new double[] { 1, 1, 1 });

            DenseMatrix denseMatrix = new DenseMatrix(_matrix);
            Vector x0 = new Vector(new double[] { 0, 0, 0 });
            IVector b = denseMatrix.Multiply(resultActual);

            var result = loggingSolver.Solve(denseMatrix, x0, b);
            for (int i = 0; i < resultActual.Size; i++)
                Assert.Equal(result[i], resultActual[i], 8);

        }

        [Fact]
        public void TestNotDiagonallyDominant()
        {
            _matrix = new double[3, 3] { { 3, 1, 1 },
                                             { 0, 5, 1 },
                                             { 80, 0, 3 } };

            IVector resultActual = new Vector(new double[] { 1, 1, 1 });

            DenseMatrix denseMatrix = new DenseMatrix(_matrix);
            Vector x0 = new Vector(new double[] { 0, 0, 0 });
            IVector b = denseMatrix.Multiply(resultActual);

            var result = loggingSolver.Solve(denseMatrix, x0, b);
            Assert.NotEmpty(result);
        }

        [Fact]
        public void TestAlgorithmCountMult()
        {
            var proxyMethod = new ProxyMethod(new CGM());
            loggingSolver = new LoggingSolver(proxyMethod, Logger);
            double[,] _matrix = new double[3, 3] { { 3, 1, 1 },
                                                       { 0, 5, 1 },
                                                       { 2, 0, 3 } };

            IVector resultActual = new Vector(new double[] { 1, 1, 1 });

            DenseMatrix denseMatrix = new DenseMatrix(_matrix);
            ProxyMatrix proxyMatrix = new ProxyMatrix(denseMatrix);

            Vector x0 = new Vector(new double[] { 0, 0, 0 });
            IVector b = denseMatrix.Multiply(resultActual);

            var result = loggingSolver.Solve(proxyMatrix, x0, b);
            var MultCount = proxyMethod.MultCount;

            _testOutputHelper.WriteLine(MultCount[0].ToString());
            _testOutputHelper.WriteLine(MultCount[1].ToString());

            for (int i = 0; i < resultActual.Size; i++)
                Assert.Equal(result[i], resultActual[i], 8);

        }

        //...
    }
}
