namespace Exceptions
{
    using System;
    class CharacterHasDiedException : Exception
    {
        // Default constructor
        public CharacterHasDiedException(string msg) : base(msg) { }

        // Constructor with inner exception
        public CharacterHasDiedException(string msg, Exception innerEx) : base(msg, innerEx) { }
    }
}
