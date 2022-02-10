using System.ComponentModel;

namespace Lab1 {
    public static class Utils {
        public static T? GetValueFromUser<T>(string msg) {
            while (true) {
                Console.Write(msg);
                var userAnswer = Console.ReadLine();
                try {
                    return userAnswer != null ? userAnswer.Convert<T>() : default(T);
                } catch (Exception) {
                    ConsoleTools.WriteLine(ConsoleColor.Red, "Invalid value type. Try again...");
                }
            }
        }

        public static T? Convert<T>(this string input) {
            var converter = TypeDescriptor.GetConverter(typeof(T));
            if (converter == null) {
                throw new Exception();
            }

            var convertResult = converter.ConvertFromString(input);
            return convertResult != null ? (T)convertResult : default(T);
        }

        public static IEnumerable<string> WholeEqualChunks(string str, int chunkSize) {
            CompleteStr(ref str, chunkSize);
            for (int i = 0; i < str.Length; i += chunkSize) {
                var value = str.Substring(i, chunkSize);
                yield return value;
            }
        }

        private static void CompleteStr(ref string str, int chunkSize) {
            for (var i = 0; i < str.Length % chunkSize; i++) {
                str += "#";
            }
        }
    }
}