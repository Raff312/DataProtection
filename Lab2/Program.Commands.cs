using System.Text;

namespace Lab2 {
    partial class Program {
        private class CommandDefinition {
            public string[] Codes { get; }
            public string Description { get; set; }
            public Action? Action { get; set; }

            public CommandDefinition(params string[] codes) {
                Codes = codes;
                Description = string.Empty;
            }
        }

        private static readonly CommandDefinition[] CommandDefinitions = {
            new CommandDefinition("1") {
                Description = "GeneratePrime",
                Action = RunGeneratePrime
            },
            new CommandDefinition("2") {
                Description = "GeneratePrimesList",
                Action = RunGeneratePrimesList
            },
            new CommandDefinition("3") {
                Description = "CheckPrime",
                Action = RunCheckPrime
            },
            new CommandDefinition("0") {
                Description = "Exit from program",
                Action = null
            }
        };

        private static CommandDefinition? GetCommandDefinitionByCode(string code) {
            code = code.ToLowerInvariant();
            return CommandDefinitions.FirstOrDefault(x => x.Codes.Contains(code));
        }

        private static void RunGeneratePrime() {
            var digits = Utils.GetValueFromUser<int>("Enter a number of digits: ");
            var prime = PrimeUtils.Generate(digits);

            Console.WriteLine($"Prime number with {digits} digits = {prime}");
        }

        private static void RunGeneratePrimesList() {
            var start = Utils.GetValueFromUser<int>("Enter a start number: ");
            var end = Utils.GetValueFromUser<int>("Enter a end number: ");
            var primes = PrimeUtils.GenerateListEratosthenes(start, end);

            var sb = new StringBuilder();
            sb.AppendJoin(" ", primes);
            Console.WriteLine($"Primes list (Eratosthenes): {sb}\nCount of primes = {primes.Count}");

            primes.Clear();
            primes = PrimeUtils.GenerateListFermat(start, end);

            sb.Clear();
            sb.AppendJoin(" ", primes);
            Console.WriteLine($"Primes list (Fermat): {sb}\nCount of primes = {primes.Count}");
        }

        private static void RunCheckPrime() {
            var num = Utils.GetValueFromUser<int>("Enter a number: ");
            var isPrime = PrimeUtils.IsPrime(num);

            if (isPrime) {
                Console.WriteLine($"Number {num} is prime");
            } else {
                Console.WriteLine($"Number {num} is not prime");
            }
        }
    }
}
