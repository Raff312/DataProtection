using System.Numerics;

namespace Lab6
{
    public static class MathUtils
    {
        public static BigInteger GetMutuallySimpleNumber(BigInteger value)
        {
            for (var i = value - 1; i > 2; i--)
            {
                if (BigInteger.GreatestCommonDivisor(value, i) == 1)
                {
                    return i;
                }
            }

            return 13;
        }

        public static (BigInteger gcd, BigInteger t) ExtendedGcd(BigInteger a, BigInteger b)
        {
            if (a < b)
            {
                Swap(ref a, ref b);
            }

            var u = (u1: a, u2: (BigInteger)0);
            var v = (v1: b, v2: (BigInteger)1);
            while (v.v1 != 0)
            {
                var r = u.u1 / v.v1;
                var t = (u.u1 % v.v1, u.u2 - r * v.v2);
                u = v;
                v = t;
            }

            if (u.u2 < 0)
            {
                u.u2 += a;
            }

            return u;
        }

        private static void Swap<T>(ref T lhs, ref T rhs)
        {
            var temp = lhs;
            lhs = rhs;
            rhs = temp;
        }
    }
}
