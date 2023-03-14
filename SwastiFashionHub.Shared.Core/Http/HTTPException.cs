using System;

namespace SwastiFashionHub.Shared.Core.Http
{
    [Serializable]
    public class HTTPException : Exception
    {
        public HTTPException()
        {

        }
        public HTTPException(string message) : base(message)
        {

        }
    }
}
