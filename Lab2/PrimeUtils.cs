using System.Numerics;

namespace Lab2 {
    public static class PrimeUtils {
        public static List<int> GenerateListFermat(int start, int end) {
            if (start > end) { return new List<int>(); }
            return (
                from i in Enumerable.Range(start, end - start + 1).AsParallel()
                where i == 2 || BigInteger.ModPow(2, i - 1, i) == 1
                select i
            ).ToList();
        }

        public static int Generate(int digits) {
            var n = (int)Math.Pow(10, digits);

            var primes = GenerateListEratosthenes(n / 10 + 1, n - 1);
            var rnd = new Random();
            var primeIndex = rnd.Next(primes.Count);

            return primes[primeIndex];
        }

        public static List<int> GenerateListEratosthenes(int start, int end) {
            if (start > end) { return new List<int>(); }
            return (
                from i in Enumerable.Range(start, end - start + 1).AsParallel()
                where Enumerable.Range(1, (int)Math.Sqrt(i)).All(j => j == 1 || i % j != 0)
                select i
            ).ToList();
        }

        public static bool IsPrime(int number) {
            if (number < 2) {
                return false;
            }

            for (var divisor = 2; divisor <= Math.Sqrt(number); divisor++) {
                if (number % divisor == 0) {
                    return false;
                }
            }
            return true;
        }
    }
}