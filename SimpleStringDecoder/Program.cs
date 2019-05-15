using System;
using StringDecoderLibrary;

namespace SimpleStringDecoder
{
    internal class Program
    {
        private static void Main()
        {
            var stringDecoder = new StringDecoder
            {
                Decoded = DecodedOutput
            };

            for (;;)
            {
                Console.WriteLine("Enter strings or (q) to exit ==> ");
                var input = Console.ReadLine();

                if (input == "q")
                {
                    stringDecoder.Decode(string.Empty);
                    break;
                }

                stringDecoder.Decode(input);
            }
        }


        protected static void DecodedOutput(string decoded)
        {
            Console.WriteLine($"decoded string: {decoded}");
        }
    }
}
