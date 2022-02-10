using System.Text;

namespace Lab1 {
    public static class InverseUtils {    
        public static string Convert(string input) {
            var inputArr = input.ToCharArray();
            var result = new StringBuilder(inputArr.Length);
            for (int i = 0; i < inputArr.Length; i++) {
                var asciiCode = (int)inputArr[i] - 848;
                result.Append((char)(255 + 33 - asciiCode));
            }
            
            return result.ToString();
        }
    } 
}
