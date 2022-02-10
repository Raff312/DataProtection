namespace Lab1 {
    public static class PermutationUtils {
        public static string Encode(string input, string permutation) {
            CompleteStr(ref input, permutation.Length);
    
            var keys = StrToIntArr(permutation);

            var result = string.Empty;
            for (int i = 0; i < input.Length; i += keys.Length) {
                char[] transpositions = new char[keys.Length];
    
                for (int j = 0; j < keys.Length; j++) {
                    transpositions[keys[j] - 1] = input[i + j];
                }
    
                for (int j = 0; j < keys.Length; j++) {
                    result += transpositions[j];
                }
            }
    
            return result;
        }

        private static void CompleteStr(ref string str, int chunkSize) {
            for (var i = 0; i < str.Length % chunkSize; i++) {
                str += " ";
            }
        }
    
        public static string Decode(string input, string permutation) {
            var keys = StrToIntArr(permutation);

            var result = string.Empty;
            for (int i = 0; i < input.Length; i += keys.Length) {
                char[] transposition = new char[keys.Length];
    
                for (int j = 0; j < keys.Length; j++) {
                    transposition[j] = input[i + keys[j] - 1];
                }
    
                for (int j = 0; j < keys.Length; j++) {
                    result += transposition[j];
                }
            }
    
            return result.Trim();
        }

        private static int[] StrToIntArr(string key) {
            var result = new int[key.Length];
            for (int i = 0; i < key.Length; i++) {
                if (int.TryParse(key[i].ToString(), out var val)) {
                    result[i] = val;
                } else {
                    result[i] = i;
                }
            }

            return result;
        }
    } 
}