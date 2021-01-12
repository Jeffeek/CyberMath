using System;
using System.Collections.Generic;

namespace CyberMath.Structures.MatrixBase
{
    public interface IMatrixBase<T> : IEnumerable<T>
    {
        int RowsCount { get; }
        bool IsSquare { get; }
        T this[int row, int column] { get; set; }
        void ProcessFunctionOverData(Action<int, int> func);
        int ElementsInRow(int rowIndex);
        IMatrixBase<T> CreateMatrixWithoutColumn(int columnIndex);
        IMatrixBase<T> CreateMatrixWithoutRow(int rowIndex);
    }
}
