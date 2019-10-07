using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace toks_1
{
    public class BitStuffing
    {
        private const string beforeBitstuff = "11111";
        private const string afterBitstuff = "111110";

        public string Decode(byte[] message)
        {
            var bitString = BitsToString(new BitArray(message));
            var unbitstuffedString = bitString.Replace(afterBitstuff, beforeBitstuff);
            var unbitstuffedBytes = StringToBits(unbitstuffedString);

            return Encoding.UTF8.GetString(unbitstuffedBytes); 
        }

        public byte[] Encode(string message)
        {
            var bytes = Encoding.UTF8.GetBytes(message);

            var bitString = BitsToString(new BitArray(bytes));
            var bitsuffedString = bitString.Replace(beforeBitstuff, afterBitstuff);
            var bitsuffedBytes = StringToBits(bitsuffedString);

            return bitsuffedBytes;
        }

        private static string BitsToString(BitArray bitArray)
        {
            var result = string.Empty;
            foreach (var bit in bitArray)
                result += (bool)bit == true ? "1" : "0";
            return result;
        }

        private static byte[] StringToBits(string text)
        {
            var bitArray = new BitArray(text.Length);

            for (int i = 0; i < text.Length; i++)
                bitArray[i] = text[i] == '1';

            var bytes = new byte[(bitArray.Length + 8 - 1) / 8];
            bitArray.CopyTo(bytes, 0);
            return bytes;
        }      
    }
}
