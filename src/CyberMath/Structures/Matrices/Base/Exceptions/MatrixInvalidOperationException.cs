#region Using derectives

using System;

#endregion

namespace CyberMath.Structures.Matrices.Base.Exceptions
{
	/// <summary>
	///     Exception appear when happens an invalid operation with <see cref="IMatrixBase{T}" />
	/// </summary>
	public class MatrixInvalidOperationException : Exception
	{
		/// <inheritdoc />
		public MatrixInvalidOperationException(string message) => Message = message;

		/// <inheritdoc />
		public MatrixInvalidOperationException() { }

		/// <inheritdoc />
		public override string Message { get; } = "Impossible operation for this matrix";
	}
}