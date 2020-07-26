using System;

namespace StockOptionApp.FIleDownload
{
    public interface ICsvParser<T>
    {
        bool TryParse(string line, out T data);
    }

    public class FlexOptionParser : ICsvParser<FlexOptionData>
    {
        public bool TryParse(string line, out FlexOptionData data)
        {
            data = null;
            var split = line.Split(new char[0], StringSplitOptions.RemoveEmptyEntries);
            if (split.Length != 8)
                return false;

            var root = split[0];
            if (TryParseOptionType(split[1], out var optionType)
                && int.TryParse(split[2], out var month)
                && int.TryParse(split[3], out var day)
                && int.TryParse(split[4], out var year)
                && decimal.TryParse(split[5], out var strikePrice)
                && decimal.TryParse(split[6], out var underlyingPrice)
                && decimal.TryParse(split[7], out var mark))
            {
                var expiration = new DateTime(year, month, day);
                data = new FlexOptionData(root, optionType, expiration, strikePrice, underlyingPrice, mark);
                return true;
            }
            return false;
        }

        private bool TryParseOptionType(string v, out OptionType type)
        {
            switch (v)
            {
                case "P":
                    type = OptionType.Put;
                    return true;
                case "C":
                    type = OptionType.Call;
                    return true;
                default:
                    type = OptionType.Unknown;
                    return false;
            }
        }
    }
}
