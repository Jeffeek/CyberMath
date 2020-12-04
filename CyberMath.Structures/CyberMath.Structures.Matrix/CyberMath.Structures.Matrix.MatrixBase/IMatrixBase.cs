using System;
using System.Collections;

namespace CyberMath.Structures.Matrix.MatrixBase
{
    public interface IMatrixBase<T> : IEnumerable
    {
        int RowsCount { get; }
        bool IsSquare { get; }
        T this[int row, int column] { get; set; }
        void ProcessFunctionOverData(Action<int, int> func);
        string GetAsString();
        int ElementsInRow(int i);
        IMatrixBase<T> CreateMatrixWithoutColumn(int columnIndex);
        IMatrixBase<T> CreateMatrixWithoutRow(int rowIndex);
    }
}
