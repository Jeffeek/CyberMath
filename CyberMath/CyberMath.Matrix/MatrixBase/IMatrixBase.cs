using System;
using System.Collections;

namespace MatrixBase
{
    public interface IMatrixBase<T> : IDisposable, IEnumerable
    {
        bool IsSquare { get; }
        T this[int row, int column] { get; set; }
        IMatrixBase<T> Transpose();
        void ProcessFunctionOverData(Action<int, int> func);
        string GetAsString();
        IMatrixBase<T> CreateMatrixWithoutColumn(int columnIndex);
        IMatrixBase<T> CreateMatrixWithoutRow(int rowIndex);
    }
}
