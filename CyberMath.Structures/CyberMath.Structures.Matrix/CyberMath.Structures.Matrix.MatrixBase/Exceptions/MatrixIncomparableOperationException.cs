using System;

namespace CyberMath.Structures.MatrixBase.Exceptions
{
    /// <summary>
    /// Exception accures when happens an Incomparable operation with <see cref="IMatrixBase{T}"/>
    /// </summary>
    public class MatrixIncomparableOperationException : Exception
    {
        private readonly string _message = "Impossible operation for this matrices";
        public override string Message => _message;

        public MatrixIncomparableOperationException(string message)
        {
            _message = message;
        }

        public MatrixIncomparableOperationException() { }
    }
}
