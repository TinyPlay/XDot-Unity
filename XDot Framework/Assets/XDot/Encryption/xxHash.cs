namespace XDot.Encryption
{
    /// <summary>
    /// xxHash Module
    /// </summary>
    public class xxHash
    {
        const uint PRIME32_1 = 2654435761U;
        const uint PRIME32_2 = 2246822519U;
        const uint PRIME32_3 = 3266489917U;
        const uint PRIME32_4 = 668265263U;
        const uint PRIME32_5 = 374761393U;
        
        /// <summary>
        /// Calculate Hash
        /// </summary>
        /// <param name="buf"></param>
        /// <param name="len"></param>
        /// <param name="seed"></param>
        /// <returns></returns>
        public static uint CalculateHash(byte[] buf, int len, uint seed = 0)
        {
            uint h32;
            int index = 0;

            if (len >= 16)
            {
                int limit = len - 16;
                uint v1 = seed + PRIME32_1 + PRIME32_2;
                uint v2 = seed + PRIME32_2;
                uint v3 = seed;
                uint v4 = seed - PRIME32_1;

                do
                {
                    uint read_value = (uint)(buf[index++] | buf[index++] << 8 | buf[index++] << 16 | buf[index++] << 24);
                    v1 += read_value * PRIME32_2;
                    v1 = (v1 << 13) | (v1 >> 19);
                    v1 *= PRIME32_1;

                    read_value = (uint)(buf[index++] | buf[index++] << 8 | buf[index++] << 16 | buf[index++] << 24);
                    v2 += read_value * PRIME32_2;
                    v2 = (v2 << 13) | (v2 >> 19);
                    v2 *= PRIME32_1;

                    read_value = (uint)(buf[index++] | buf[index++] << 8 | buf[index++] << 16 | buf[index++] << 24);
                    v3 += read_value * PRIME32_2;
                    v3 = (v3 << 13) | (v3 >> 19);
                    v3 *= PRIME32_1;

                    read_value = (uint)(buf[index++] | buf[index++] << 8 | buf[index++] << 16 | buf[index++] << 24);
                    v4 += read_value * PRIME32_2;
                    v4 = (v4 << 13) | (v4 >> 19);
                    v4 *= PRIME32_1;

                } while (index <= limit);

                h32 = ((v1 << 1) | (v1 >> 31)) + ((v2 << 7) | (v2 >> 25)) + ((v3 << 12) | (v3 >> 20)) + ((v4 << 18) | (v4 >> 14));
            }
            else
            {
                h32 = seed + PRIME32_5;
            }

            h32 += (uint)len;

            while (index <= len - 4)
            {
                h32 += (uint)(buf[index++] | buf[index++] << 8 | buf[index++] << 16 | buf[index++] << 24) * PRIME32_3;
                h32 = ((h32 << 17) | (h32 >> 15)) * PRIME32_4;
            }

            while (index < len)
            {
                h32 += buf[index] * PRIME32_5;
                h32 = ((h32 << 11) | (h32 >> 21)) * PRIME32_1;
                index++;
            }

            h32 ^= h32 >> 15;
            h32 *= PRIME32_2;
            h32 ^= h32 >> 13;
            h32 *= PRIME32_3;
            h32 ^= h32 >> 16;

            return h32;
        }
    }
}