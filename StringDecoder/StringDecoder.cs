using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

namespace StringDecoderLibrary
{
    public delegate void DecodeCallback(string response);

    public class StringDecoder
    {
        private readonly List<byte> _buffer;
        private bool _lookahead;

        public DecodeCallback Decoded { get; set; }

        public StringDecoder()
        {
            _buffer = new List<byte>();
        }

        public void Decode(string source)
        {
            Contract.Assert(source != null, new ArgumentNullException(nameof(source)).Message);
            var convertedSource = ValidateSource(source);
            DecodeWorker(convertedSource);
        }

        private void DecodeWorker(string source)
        {
            // Add source bytes to buffer.
            var sourceBytes = ConvertToByteArray(source);
            _buffer.AddRange(sourceBytes);

            // Initialize the index.
            const int index = 0;

            // Get string length;
            var strlen = _buffer[index];

            if (strlen == 0)
            {
                _lookahead = true;
                for (var ii = index + 1; ii < _buffer.Count; ++ii)
                {
                    if (_buffer[ii] == 0)
                    {
                        Decoded?.Invoke(ConvertToString(_buffer, index + 1, ii));
                        _lookahead = false;
                        return;
                    }
                }

                Decoded?.Invoke(string.Empty);
                return;
            }

            if (!_lookahead)
            {
                // Exit if there is not enough bytes.
                if (strlen > _buffer.Count)
                {
                    Decoded?.Invoke(String.Empty);
                    return;
                }

                // Convert the encoded bytes into a string.
                Decoded?.Invoke(ConvertToString(_buffer, index + 1, strlen));
            }
        }

        private static string ValidateSource(string source)
        {
            var scrubbed = source.Replace(" ", string.Empty);
            if (scrubbed.Length % 2 != 0)
            {
                throw new ArgumentException(nameof(source));
            }

            return scrubbed;
        }

        public static byte[] ConvertToByteArray(string hexadecimal)
        {
            return Enumerable.Range(0, hexadecimal.Length)
                             .Where(x => x % 2 == 0)
                             .Select(x => Convert.ToByte(hexadecimal.Substring(x, 2), 16))
                             .ToArray();
        }

        private static string ConvertToString(List<byte> buffer, int start, int end)
        {
            var builder = new StringBuilder();

            for (var ii = start; ii <= end; ++ii)
            {
                var character = Convert.ToChar(buffer[ii]);
                if (!char.IsControl(character) && IsAsciiPrintable(character))
                {
                    builder.Append(character);
                }
                else if (buffer[ii] != 0)
                {
                    var charVal = Convert.ToInt32(character);
                    builder.Append($@"\x{charVal:X}");
                }
            }

            // Trim the buffer.
            buffer.RemoveRange(0, end + 1);

            // Return the string.
            return builder.ToString();
        }

        public static bool IsAsciiPrintable(char ch)
        {
            return ch >= 32 && ch < 127;
        }
    }
}
