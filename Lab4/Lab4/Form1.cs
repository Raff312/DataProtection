using System.Text;

namespace Lab4
{
    public partial class Form1 : Form
    {
        private const byte BLOCK_SIZE = 64;
        private const byte ROUND_COUNT = 16;
        private readonly Encoding ENCODING = Encoding.UTF8;

        private readonly byte[] _keys = new byte[ROUND_COUNT];

        public Form1()
        {
            InitializeComponent();
        }

        private void btnEncrypt_Click(object sender, EventArgs e)
        {
            GenerateKeys();
            lblKeys.Text = "(" + string.Join(", ", _keys) + ")";
            txtResult.Text = Encrypt(txtMsg.Text);
        }

        private void GenerateKeys()
        {
            Random rnd = new();
            rnd.NextBytes(_keys);
        }

        private string Encrypt(string input)
        {
            string binaryString = Utils.ToBinary(Utils.ConvertToByteArray(input, ENCODING));
            string[] binaryBlocks = Utils.SplitIntoBlocks(binaryString, BLOCK_SIZE);

            string result = string.Empty;
            foreach (string block in binaryBlocks)
            {
                result += EncryptBlock(block);
            }

            return result;
        }

        private string EncryptBlock(string block)
        {
            int subblockSize = block.Length / 2;
            string leftSubblock = block.Substring(0, subblockSize);
            string rightSubblock = block.Substring(subblockSize);

            int leftSubblockValue = Convert.ToInt32(leftSubblock, 2);
            int rightSubblockValue = Convert.ToInt32(rightSubblock, 2);

            EncryptInternal(ref leftSubblockValue, ref rightSubblockValue);

            return Convert.ToString(leftSubblockValue, 2).PadLeft(subblockSize, '0') + Convert.ToString(rightSubblockValue, 2).PadLeft(subblockSize, '0');
        }

        private void EncryptInternal(ref int left, ref int right)
        {
            txtInfo.Text = string.Empty;
            for (int i = 0; i < ROUND_COUNT; i++)
            {
                int temp = right ^ F(left, _keys.ElementAt(i));
                txtInfo.Text += $"{i}. Левая часть: {left}, Правая часть: {right}, Ключ: {_keys.ElementAt(i)}, Результат операции: {temp} \r\n";
                right = left;
                left = temp;
            }
        }

        private void btnDecrypt_Click(object sender, EventArgs e)
        {
            txtResult.Text = Decrypt(txtMsg.Text);
        }

        private string Decrypt(string input)
        {
            string[] binaryBlocks = Utils.SplitIntoBlocks(input, BLOCK_SIZE);

            string result = string.Empty;
            foreach (string block in binaryBlocks)
            {
                result += DecryptBlock(block);
            }

            return ENCODING.GetString(Utils.BinaryToByte(result));
        }

        private string DecryptBlock(string block)
        {
            int subblockSize = block.Length / 2;
            string leftSubblock = block.Substring(0, subblockSize);
            string rightSubblock = block.Substring(subblockSize);

            int leftSubblockValue = Convert.ToInt32(leftSubblock, 2);
            int rightSubblockValue = Convert.ToInt32(rightSubblock, 2);

            DecryptInternal(ref leftSubblockValue, ref rightSubblockValue);

            return Convert.ToString(leftSubblockValue, 2).PadLeft(subblockSize, '0') + Convert.ToString(rightSubblockValue, 2).PadLeft(subblockSize, '0');
        }

        private void DecryptInternal(ref int left, ref int right)
        {
            txtInfo.Text = string.Empty;
            for (int i = ROUND_COUNT - 1; i >= 0; i--)
            {
                int temp = left ^ F(right, _keys.ElementAt(i));
                txtInfo.Text += $"{i}. Левая часть: {left}, Правая часть: {right}, Ключ: {_keys.ElementAt(i)}, Результат операции: {temp} \r\n";
                left = right;
                right = temp;
            }
        }

        private static int F(int value1, int value2)
        {
            return (value1 * value2) % int.MaxValue;
        }
    }
}