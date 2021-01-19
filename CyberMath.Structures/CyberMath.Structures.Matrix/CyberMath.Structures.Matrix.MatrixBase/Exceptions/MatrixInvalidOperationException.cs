using System;

namespace CyberMath.Structures.MatrixBase.Exceptions
{
    /// <summary>
    /// Exception appear when happens an invalid operation with <see cref="IMatrixBase{T}"/>
    /// </summary>
    public class MatrixInvalidOperationException : Exception
    {
	    public override string Message { get; } = "Impossible operation for this matrix";

        public MatrixInvalidOperationException(string message) => Message = message;

        public MatrixInvalidOperationException() { }
    }
}
