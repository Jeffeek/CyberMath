using System;

namespace CyberMath.Structures.MatrixBase.Exceptions
{
    //TODO: summary
    public class MatrixIncomparableOperationException : Exception
    {
        private readonly string _message = "Impossible operation for this matrices";
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
