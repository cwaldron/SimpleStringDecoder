using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StringDecoderLibrary;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        readonly List<string> _results = new List<string>();

        [TestMethod]
        public void TestHelloWorld()
        {
            var decoder = new StringDecoder {Decoded = Decoded};
            decoder.Decode("06 48 65 6C 6C");
            decoder.Decode("6F 20 00 57 6F");
            decoder.Decode("72 6C 64 00");
            VerifyHelloWorld();
            _results.Clear();
        }

        [TestMethod]
        public void TestHiThere()
        {
            var decoder = new StringDecoder { Decoded = Decoded };
            decoder.Decode("08 48 69 20 54");
            decoder.Decode("68 65 72 65");
            VerifyHiThere();
            _results.Clear();
        }

        [TestMethod]
        public void TestHexString()
        {
            var decoder = new StringDecoder { Decoded = Decoded };
            decoder.Decode("00 11 21 44 88");
            decoder.Decode("CC 00");
            VerifyHexString();
            _results.Clear();
        }

        [TestMethod]
        public void TestQuickBrownFox()
        {
            var decoder = new StringDecoder { Decoded = Decoded };
            decoder.Decode("2C 54 68 65 20 71 75 69 63 6B 20 62 72 6F 77 6E");
            decoder.Decode("20 66 6F 78 20 6A 75 6D 70 65 64 20 6F 76 65 72");
            decoder.Decode("20 74 68 65 20 6C 61 7A 79 20 64 6F 67");
            VerifyQuickBrownFox();
            _results.Clear();
        }

        private void Decoded(string decodedString)
        {
            _results.Add(decodedString);
        }

        private void VerifyHelloWorld()
        {
            Assert.AreEqual(string.Empty, _results[0]);
            Assert.AreEqual("Hello ", _results[1]);
            Assert.AreEqual("World", _results[2]);
        }

        private void VerifyHiThere()
        {
            Assert.AreEqual(string.Empty, _results[0]);
            Assert.AreEqual("Hi There", _results[1]);
        }
        private void VerifyHexString()
        {
            Assert.AreEqual(string.Empty, _results[0]);
            Assert.AreEqual(@"\x11!D\x88\xCC", _results[1]);
        }
        private void VerifyQuickBrownFox()
        {
            Assert.AreEqual(string.Empty, _results[0]);
            Assert.AreEqual(string.Empty, _results[1]);
            Assert.AreEqual("The quick brown fox jumped over the lazy dog", _results[2]);
        }
    }
}
