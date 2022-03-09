using System.Numerics;
using System.Security.Cryptography;

namespace Lab7
{
    public partial class Form1 : Form
    {
        private const string ALPHABET = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyzАБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯабвгдеёжзийклмнопрстуфхцчшщъыьэюя1234567890 ,./;:()!";

        private List<BigInteger> _publicKey = new();
        private BigInteger _privateKey;

        public Form1()
        {
            InitializeComponent();
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            GenerateKeys();
            lblOpenKey.Text = "(" + _publicKey.ElementAt(0) + ", " + _publicKey.ElementAt(1) + ", " + _publicKey.ElementAt(2) + ")";
        }

        private void GenerateKeys()
        {
            _publicKey.Clear();
            _privateKey = 0;

            var p = PrimeUtils.Generate(5);
            var g = MathUtils.FindPrimitiveRoot(p - 1, p);
            var x = p - 1;

            while (!PrimeUtils.IsCoprime(x, p - 1))
            {
                x = new Random().Next(2, p - 2);
            }

            var y = BigInteger.ModPow(g, x, p);

            _publicKey.Add(y);
            _publicKey.Add(g);
            _publicKey.Add(p);

            _privateKey = x;
        }

        private void btnEncrypt_Click(object sender, EventArgs e)
        {
            if (_privateKey <= 0 || _publicKey?.Count <= 0)
            {
                MessageBox.Show("Ключ не сгенерирован", "Ошибка!", MessageBoxButtons.OK);
                return;
            }

            txtResult.Text = Encrypt(txtMsg.Text);
        }

        private string Encrypt(string text)
        {
            var encryptionString = string.Empty;

            var rng = RandomNumberGenerator.Create();
            for (int i = 0; i < text.Length; i++)
            {
                var y = _publicKey.ElementAt(0);
                var g = _publicKey.ElementAt(1);
                var p = _publicKey.ElementAt(2);

                var temp = ALPHABET.IndexOf(text[i]);

                var k = p;

                var randomNumber = new byte[1];

                while (!PrimeUtils.IsCoprime(k, p - 1) || k < 2 || k > p - 2)
                {
                    rng.GetBytes(randomNumber);
                    k = randomNumber[0];
                }

                var a = BigInteger.ModPow(g, k, p);
                var b = (BigInteger.ModPow(y, k, p) * temp % p) % p;

                encryptionString += a + "|" + b + " ";
            }

            return encryptionString;
        }

        private void btnDecrypt_Click(object sender, EventArgs e)
        {
            if (_privateKey <= 0 || _publicKey?.Count <= 0)
            {
                MessageBox.Show("Ключ не сгенерирован", "Ошибка!", MessageBoxButtons.OK);
                return;
            }

            txtResult.Text = Decrypt(txtMsg.Text);
        }

        private string Decrypt(string text)
        {
            var decryptionString = string.Empty;
            var encodedString = text.Split(' ');

            for (int i = 0; i < encodedString.Length - 1; i++)
            {
                var encodedPairs = encodedString[i].Split('|');
                var p = _publicKey.ElementAt(2);

                BigInteger a = BigInteger.Parse(encodedPairs[0]);
                BigInteger b = BigInteger.Parse(encodedPairs[1]);
                BigInteger index = (b % p * BigInteger.ModPow(a, p - 1 - _privateKey, p)) % p;
                decryptionString += ALPHABET[(int)index];
            }

            return decryptionString;
        }
    }
}