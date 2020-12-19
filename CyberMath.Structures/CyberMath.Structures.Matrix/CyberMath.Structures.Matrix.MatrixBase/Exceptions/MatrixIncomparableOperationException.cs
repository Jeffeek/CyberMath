using System;

namespace CyberMath.Structures.Matrix.MatrixBase.Exceptions
{
    public class MatrixIncomparableOperationException : Exception
    {
        private string _message = "Impossible operation for this matrices";
        public override string Message => _message;

        public MatrixIncomparableOperationException(string message)
        {
            _message = message;
        }

        public MatrixIncomparableOperationException()
        {
            
        }
    }
}
