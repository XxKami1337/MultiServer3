using System;

using Org.BouncyCastle.Crypto.Utilities;

namespace Org.BouncyCastle.Crypto.Prng
{
    public sealed class VmpcRandomGenerator
        : IRandomGenerator 
    {
        /// <remarks>
        /// Permutation generated by code:
        /// <code>
        /// // First 1850 fractional digit of Pi number. 
        /// byte[] key = new BigInteger("14159265358979323846...5068006422512520511").ToByteArray();
        /// s = 0;
        /// P = new byte[256];
        /// for (int i = 0; i &lt; 256; i++) 
        /// {
        ///     P[i] = (byte) i;
        /// }
        /// for (int m = 0; m &lt; 768; m++) 
        /// {
        ///     s = P[(s + P[m &amp; 0xff] + key[m % key.length]) &amp; 0xff];
        ///     byte temp = P[m &amp; 0xff];
        ///     P[m &amp; 0xff] = P[s &amp; 0xff];
        ///     P[s &amp; 0xff] = temp;
        /// } </code>
        /// </remarks>
        private readonly byte[] P =
        {
            0xbb, 0x2c, 0x62, 0x7f, 0xb5, 0xaa, 0xd4, 0x0d, 0x81, 0xfe, 0xb2, 0x82, 0xcb, 0xa0, 0xa1, 0x08,
            0x18, 0x71, 0x56, 0xe8, 0x49, 0x02, 0x10, 0xc4, 0xde, 0x35, 0xa5, 0xec, 0x80, 0x12, 0xb8, 0x69,
            0xda, 0x2f, 0x75, 0xcc, 0xa2, 0x09, 0x36, 0x03, 0x61, 0x2d, 0xfd, 0xe0, 0xdd, 0x05, 0x43, 0x90,
            0xad, 0xc8, 0xe1, 0xaf, 0x57, 0x9b, 0x4c, 0xd8, 0x51, 0xae, 0x50, 0x85, 0x3c, 0x0a, 0xe4, 0xf3,
            0x9c, 0x26, 0x23, 0x53, 0xc9, 0x83, 0x97, 0x46, 0xb1, 0x99, 0x64, 0x31, 0x77, 0xd5, 0x1d, 0xd6,
            0x78, 0xbd, 0x5e, 0xb0, 0x8a, 0x22, 0x38, 0xf8, 0x68, 0x2b, 0x2a, 0xc5, 0xd3, 0xf7, 0xbc, 0x6f,
            0xdf, 0x04, 0xe5, 0x95, 0x3e, 0x25, 0x86, 0xa6, 0x0b, 0x8f, 0xf1, 0x24, 0x0e, 0xd7, 0x40, 0xb3,
            0xcf, 0x7e, 0x06, 0x15, 0x9a, 0x4d, 0x1c, 0xa3, 0xdb, 0x32, 0x92, 0x58, 0x11, 0x27, 0xf4, 0x59,
            0xd0, 0x4e, 0x6a, 0x17, 0x5b, 0xac, 0xff, 0x07, 0xc0, 0x65, 0x79, 0xfc, 0xc7, 0xcd, 0x76, 0x42,
            0x5d, 0xe7, 0x3a, 0x34, 0x7a, 0x30, 0x28, 0x0f, 0x73, 0x01, 0xf9, 0xd1, 0xd2, 0x19, 0xe9, 0x91,
            0xb9, 0x5a, 0xed, 0x41, 0x6d, 0xb4, 0xc3, 0x9e, 0xbf, 0x63, 0xfa, 0x1f, 0x33, 0x60, 0x47, 0x89,
            0xf0, 0x96, 0x1a, 0x5f, 0x93, 0x3d, 0x37, 0x4b, 0xd9, 0xa8, 0xc1, 0x1b, 0xf6, 0x39, 0x8b, 0xb7,
            0x0c, 0x20, 0xce, 0x88, 0x6e, 0xb6, 0x74, 0x8e, 0x8d, 0x16, 0x29, 0xf2, 0x87, 0xf5, 0xeb, 0x70,
            0xe3, 0xfb, 0x55, 0x9f, 0xc6, 0x44, 0x4a, 0x45, 0x7d, 0xe2, 0x6b, 0x5c, 0x6c, 0x66, 0xa9, 0x8c,
            0xee, 0x84, 0x13, 0xa7, 0x1e, 0x9d, 0xdc, 0x67, 0x48, 0xba, 0x2e, 0xe6, 0xa4, 0xab, 0x7c, 0x94,
            0x00, 0x21, 0xef, 0xea, 0xbe, 0xca, 0x72, 0x4f, 0x52, 0x98, 0x3f, 0xc2, 0x14, 0x7b, 0x3b, 0x54,
        };

        /// <remarks>Value generated in the same way as <c>P</c>.</remarks>
        private byte s = 0xbe;
        private byte n = 0;

        public VmpcRandomGenerator()
        {
        }

        public void AddSeedMaterial(byte[] seed) 
        {
            if (seed == null)
                return;

#if NETCOREAPP2_1_OR_GREATER || NETSTANDARD2_1_OR_GREATER
            AddSeedMaterial(seed.AsSpan());
#else
            for (int m = 0; m < seed.Length; m++) 
            {
                byte pn = P[n];
                s = P[(s + pn + seed[m]) & 0xff];
                P[n] = P[s];
                P[s] = pn;
                n = (byte)(n + 1);
            }
#endif
        }

#if NETCOREAPP2_1_OR_GREATER || NETSTANDARD2_1_OR_GREATER
        public void AddSeedMaterial(ReadOnlySpan<byte> seed)
        {
            for (int m = 0; m < seed.Length; m++)
            {
                byte pn = P[n];
                s = P[(s + pn + seed[m]) & 0xff];
                P[n] = P[s];
                P[s] = pn;
                n = (byte)(n + 1);
            }
        }
#endif

        public void AddSeedMaterial(long seed) 
        {
#if NETCOREAPP2_1_OR_GREATER || NETSTANDARD2_1_OR_GREATER
            Span<byte> bytes = stackalloc byte[8];
            Pack.UInt64_To_BE((ulong)seed, bytes);
            AddSeedMaterial(bytes);
#else
            AddSeedMaterial(Pack.UInt64_To_BE((ulong)seed));
#endif
        }

        public void NextBytes(byte[] bytes) 
        {
            NextBytes(bytes, 0, bytes.Length);
        }

        public void NextBytes(byte[] bytes, int start, int len) 
        {
#if NETCOREAPP2_1_OR_GREATER || NETSTANDARD2_1_OR_GREATER
            NextBytes(bytes.AsSpan(start, len));
#else
            lock (P) 
            {
                int end = start + len;
                for (int i = start; i != end; i++) 
                {
                    byte pn = P[n];
                    s = P[(s + pn) & 0xFF];
                    byte ps = P[s];
                    bytes[i] = P[(P[ps] + 1) & 0xFF];
                    P[s] = pn;
                    P[n] = ps;
                    n = (byte)(n + 1);
                }
            }
#endif
        }

#if NETCOREAPP2_1_OR_GREATER || NETSTANDARD2_1_OR_GREATER
        public void NextBytes(Span<byte> bytes)
        {
            lock (P) 
            {
                for (int i = 0; i < bytes.Length; ++i) 
                {
                    byte pn = P[n];
                    s = P[(s + pn) & 0xFF];
                    byte ps = P[s];
                    bytes[i] = P[(P[ps] + 1) & 0xFF];
                    P[s] = pn;
                    P[n] = ps;
                    n = (byte)(n + 1);
                }
            }
        }
#endif
    }
}