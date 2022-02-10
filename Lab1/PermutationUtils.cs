namespace Lab1 {
    public static class PermutationUtils {
        
        public static string Encode(string input, string permutation) {
            var isPermutationValid = ValidatePermutation(permutation);
            if (!isPermutationValid) {
                return string.Empty;
            }

            var inputChunks = Utils.WholeEqualChunks(input, permutation.Length);
            foreach (var chunk in inputChunks) {
                Console.WriteLine(chunk);
            }

            return string.Empty;
        }

        public static string Decode(string input, string permutation) {
            var isPermutationValid = ValidatePermutation(permutation);
            if (!isPermutationValid) {
                return string.Empty;
            }

            return string.Empty;
        }

        private static bool ValidatePermutation(string permutation) {
            var map = new Dictionary<char, bool>();
            foreach (var ch in permutation) {
                if (!char.IsDigit(ch)) {
                    return false;
                }

                if (map.ContainsKey(ch)) {
                    return false;
                }

                map.Add(ch, true);
            }

            return true;
        }
    } 
}