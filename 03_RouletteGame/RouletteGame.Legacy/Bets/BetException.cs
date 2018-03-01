using System;
using System.Runtime.Serialization;

namespace RouletteGame.Legacy.Bets
{
	[Serializable]
	public class BetException : Exception
	{
		public BetException()
		{
		}

		public BetException(string message) : base(message)
		{
		}

		public BetException(string message, Exception innerException) : base(message, innerException)
		{
		}

		protected BetException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}