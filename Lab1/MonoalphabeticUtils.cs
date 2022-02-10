namespace Lab1 {
    public static class MonoalphabeticUtils {
        const string alphabet = " АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ";

        public static string Shift(string input, int offset) {
            var fullAlphabet = alphabet + alphabet.ToLower();
            var alphabetLength = fullAlphabet.Length;
            var output = string.Empty;
            for (var i = 0; i < input.Length; i++) {
                var c = input[i];
                var index = fullAlphabet.IndexOf(c);
                if (index < 0) {
                    output += c.ToString();
                } else {
                    var codeIndex = (alphabetLength + index + offset) % alphabetLength;
                    output += fullAlphabet[codeIndex];
                }
            }

            return output;
        }
    } 
}