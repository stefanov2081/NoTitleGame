namespace Exceptions
{
    using Microsoft.Xna.Framework.GamerServices;
    using System;
    using System.Collections.Generic;
    class CharacterHasDiedException : Exception
    {
        public CharacterHasDiedException(string msg) : base(msg) 
        {
            
        }
        public CharacterHasDiedException(string msg, Exception innerEx) : base(msg, innerEx) { }
    }
}
