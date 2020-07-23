using System;

namespace StockOptionApp
{
    public class FlexOptionData
    {
        public FlexOptionData(string rootSymbol, OptionType type, DateTime expiration,
            decimal strikePrice, decimal underlyingPrice, decimal mark)
        {
            RootSymbol = rootSymbol;
            Type = type;
            Expiration = expiration;
            StrikePrice = strikePrice;
            UnderlyingPrice = underlyingPrice;
            Mark = mark;
        }

        public string RootSymbol { get; }
        public OptionType Type { get; }
        public DateTime Expiration { get; }
        public decimal StrikePrice { get; }
        public decimal UnderlyingPrice { get; }
        public decimal Mark { get; }
    }

    public enum OptionType : int
    {
        Unknown = 0,
        Put = 1, 
        Call = 2,
    }
}
