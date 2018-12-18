using System;
using StringDecoderLibrary;

namespace SimpleStringDecoder
{
    class Program
    {
        static void Main(string[] args)
        {
            var stringDecoder = new StringDecoder()
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
                else
                {
                    stringDecoder.Decode(input);
                }
            }
        }


        protected static void DecodedOutput(string decoded)
        {
            Console.WriteLine($"decoded string: {decoded}");
        }
    }
}
