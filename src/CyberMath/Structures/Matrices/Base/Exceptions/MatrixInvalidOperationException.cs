using System;

namespace CyberMath.Structures.Matrices.Base.Exceptions
{
    /// <summary>
    /// Exception appear when happens an invalid operation with <see cref="IMatrixBase{T}"/>
    /// </summary>
    public class MatrixInvalidOperationException : Exception
    {
	    /// <inheritdoc />
	    public override string Message { get; } = "Impossible operation for this matrix";

	    /// <inheritdoc />
	    public MatrixInvalidOperationException(string message) => Message = message;

	    /// <inheritdoc />
	    public MatrixInvalidOperationException() { }
    }
}
