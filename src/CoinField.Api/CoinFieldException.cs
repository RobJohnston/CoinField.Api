using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace CoinField.Api
{
    /// <summary>
    /// Represents an error raised by the CoinField API, each with a status code and a short message.
    /// </summary>
    public class CoinFieldException : Exception
    {
        //public CoinFieldException()
        //{
        //}

        public CoinFieldException(string message) : base(message)
        {
        }

        //public CoinFieldException(string message, Exception inner)
        //    : base(message, inner)
        //{
        //}

        //public CoinFieldException(SerializationInfo info, StreamingContext context)
        //    : base(info, context)
        //{
        //}

        public int Status { get; internal set; }

        public DateTime Timestamp { get; set; }
        public IEnumerable<Models.CoinFieldErrorResponse.Error> Errors { get; set; }
    }
}