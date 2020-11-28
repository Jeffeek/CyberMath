using System;

namespace CyberMath.Matrix.Exceptions
{
    public class IncomparableOperationException : Exception
    {
        private string _message = "Impossible operation for this matrices";
        public override string Message => _message;

        public IncomparableOperationException(string message)
        {
            _message = message;
        }
    }
}
