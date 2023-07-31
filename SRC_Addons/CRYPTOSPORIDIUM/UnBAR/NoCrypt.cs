﻿namespace PSMultiServer.Addons.CRYPTOSPORIDIUM.UnBAR
{
    internal class NoCrypt : Decryptor
    {
        public override void doInit(byte[] key, byte[] iv)
        {
        }

        public override void doUpdate(byte[] i, int inOffset, byte[] o, int outOffset, int len) => ConversionUtils.arraycopy(i, inOffset, o, (long)outOffset, len);
    }
}
