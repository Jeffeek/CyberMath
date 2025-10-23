#region Using namespaces

using BenchmarkDotNet.Attributes;
using CyberMath.Structures.Matrices.Extensions;
using CyberMath.Structures.Matrices.Matrix;

#endregion

namespace CyberMath.Performance.Tests;

/// <summary>
/// Benchmarks for matrix arithmetic operations (Add, Subtract, Multiply, Determinant, Inverse)
/// </summary>
[MemoryDiagnoser]
public class MatrixOperationsBenchmark
{
    private Matrix<int>? _matrix1Int;
    private Matrix<int>? _matrix2Int;
    private Matrix<double>? _matrix1Double;
    private Matrix<double>? _matrix2Double;

    [Params(10, 50, 100)]
    public int Size;

    [GlobalSetup]
    public void Setup()
    {
        // Initialize int matrices
        _matrix1Int = new Matrix<int>(Size, Size);
        _matrix2Int = new Matrix<int>(Size, Size);
        _matrix1Int.FillRandomly(-100, 100);
        _matrix2Int.FillRandomly(-100, 100);

        // Initialize double matrices
        _matrix1Double = new Matrix<double>(Size, Size);
        _matrix2Double = new Matrix<double>(Size, Size);
        _matrix1Double.FillRandomly(-100.0, 100.0);
        _matrix2Double.FillRandomly(-100.0, 100.0);
    }

    #region Integer Matrix Operations

    [Benchmark]
    public IMatrix<int> MatrixAdd_Int() => _matrix1Int!.Add(_matrix2Int!);

    [Benchmark]
    public IMatrix<int> MatrixSubtract_Int() => _matrix1Int!.Sub(_matrix2Int!);

    [Benchmark]
    public IMatrix<int> MatrixMultiply_Int() => _matrix1Int!.Multiplication(_matrix2Int!);

    [Benchmark]
    public IMatrix<int> MatrixScalarMultiply_Int() => _matrix1Int!.MulOnNumber(5);

    [Benchmark]
    public int MatrixDeterminant_Int() => _matrix1Int!.CalculateDeterminant();

    #endregion

    #region Double Matrix Operations

    [Benchmark]
    public IMatrix<double> MatrixAdd_Double() => _matrix1Double!.Add(_matrix2Double!);

    [Benchmark]
    public IMatrix<double> MatrixSubtract_Double() => _matrix1Double!.Sub(_matrix2Double!);

    [Benchmark]
    public IMatrix<double> MatrixMultiply_Double() => _matrix1Double!.Multiplication(_matrix2Double!);

    [Benchmark]
    public IMatrix<double> MatrixScalarMultiply_Double() => _matrix1Double!.MulOnNumber(5.0);

    [Benchmark]
    public double MatrixDeterminant_Double() => _matrix1Double!.CalculateDeterminant();

    [Benchmark]
    public IMatrix<double> MatrixInverse_Double() => _matrix1Double!.CreateInvertibleMatrix();

    #endregion
}
