using System;
using System.Collections.Generic;
using System.Text;

namespace CyberMath.Matrix.Models
{
    public interface IMatrix<T> : IDisposable
    {
        bool IsSquare { get; }
        int RowsCount { get; }
        int ColumnsCount { get; }
        T this[int row, int column] { get; set; }
        IMatrix<T> Transpose();
        void ProcessFunctionOverData(Action<int, int> func);
        string GetAsString();
        IMatrix<T> CreateMatrixWithoutColumn(int columnIndex);
        IMatrix<T> CreateMatrixWithoutRow(int rowIndex);

    }
}
