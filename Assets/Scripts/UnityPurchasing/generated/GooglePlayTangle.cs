// WARNING: Do not modify! Generated file.

namespace UnityEngine.Purchasing.Security {
    public class GooglePlayTangle
    {
        private static byte[] data = System.Convert.FromBase64String("9XWThCIwr+eDTglyjgOg0CPO+5Dzv7dEzPqCaglG7MKPf/6KpRFKCFck1aTOL3lvl4Uuq/SqCZTEq2HHe+mTsRUTkDSOUeoaRLYIGjvPvB46ube4iDq5sro6ubm4Sq40EW87PfQZszaAIRYSxXM7Lw4/iRXzT7m7+8JTdNJwN7kPUImu6UhAtrEF1Xd7ruML3A4jsJcy6JTFefwPaK0hJ9ldw4RZrGlDP19UvB2Trn0FZwYyiDq5moi1vrGSPvA+T7W5ubm9uLtX2chwQuRiJRR6MS1I1L7kZRCgryTfGjHI858xlOcCmhANtQXrzBbYpAeXRuFiQOkHusOd731Y4femMUhcjaEGzXrfsvXd1C5RFSQxDttE4Kyaus6cn+ulM7q7ubi5");
        private static int[] order = new int[] { 8,9,13,12,9,5,6,10,11,11,11,11,13,13,14 };
        private static int key = 184;

        public static readonly bool IsPopulated = true;

        public static byte[] Data() {
        	if (IsPopulated == false)
        		return null;
            return Obfuscator.DeObfuscate(data, order, key);
        }
    }
}
