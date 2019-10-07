using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using toks_1;

namespace TestProject1
{
    [TestClass]
    public class UnitTest1
    {

        public string GenerateRandomMessage()
        {
            var path = Path.GetRandomFileName();
            path = path.Replace(".", "");
            return path;
        }
        [TestMethod]
        public void EncodeDecode()
        {
            for (int i = 0; i < 1000; i++)
            {
                var message = GenerateRandomMessage();
                var coder = new BitStuffing();

                var res = coder.Decode(coder.Encode(message));

                Assert.AreEqual(message, res);
            }
        }

        [TestMethod]
        public void EncodeBitstuffDecode()
        {
            var message = "???";
            var coder = new BitStuffing();

            var res = coder.Decode(coder.Encode(message));

            Assert.AreNotEqual(message, res);
        }
    }
}
