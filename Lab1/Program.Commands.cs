namespace Lab1 {
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
                Description = "MonoalphabeticEncode",
                Action = RunMonoalphabeticEncode
            },
            new CommandDefinition("2") {
                Description = "MonoalphabeticDecode",
                Action = RunMonoalphabeticDecode
            },
            new CommandDefinition("3") {
                Description = "PermutationEncode",
                Action = RunPermutationEncode
            },
            new CommandDefinition("4") {
                Description = "PermutationDecode",
                Action = RunPermutationDecode
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

        private static void RunMonoalphabeticEncode() {
            var input = Utils.GetValueFromUser<string>("Enter a string: ");
            var offset = Utils.GetValueFromUser<int>("Enter an offset: ");

            if (string.IsNullOrEmpty(input)) {
                Console.WriteLine("There is no input");
                return;
            }

            var result = MonoalphabeticUtils.Shift(input, offset);
            Console.WriteLine(result);
        }

        private static void RunMonoalphabeticDecode() {
            var input = Utils.GetValueFromUser<string>("Enter a string: ");
            var offset = Utils.GetValueFromUser<int>("Enter an offset: ");

            if (string.IsNullOrEmpty(input)) {
                Console.WriteLine("There is no input");
                return;
            }

            var result = MonoalphabeticUtils.Shift(input, -offset);
            Console.WriteLine(result);
        }
        
        private static void RunPermutationEncode() {
            var input = Utils.GetValueFromUser<string>("Enter a string: ");
            var permutation = Utils.GetValueFromUser<string>("Enter a permutation: ");

            if (string.IsNullOrEmpty(input) || string.IsNullOrEmpty(permutation)) {
                Console.WriteLine("There is no input or permutation");
                return;
            }

            var result = PermutationUtils.Encode(input, permutation);
            Console.WriteLine(result);
        }

        private static void RunPermutationDecode() {
            var input = Utils.GetValueFromUser<string>("Enter a string: ");
            var permutation = Utils.GetValueFromUser<string>("Enter a permutation: ");

            if (string.IsNullOrEmpty(input) || string.IsNullOrEmpty(permutation)) {
                Console.WriteLine("There is no input");
                return;
            }

            var result = PermutationUtils.Decode(input, permutation);
            Console.WriteLine(result);
        }
    }
}
