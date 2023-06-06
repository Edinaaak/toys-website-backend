namespace NBP_projekat.Exceptions
{
    public class MasterpieceCustomException : Exception
    {
        public MasterpieceCustomException() :  base() { }       

        public MasterpieceCustomException(string? message) : base(message)
        {
        }
    }
}
