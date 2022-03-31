using System.Numerics;

namespace Lab6
{
    public partial class Form1 : Form
    {
        private const string ALPHABET = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyzАБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯабвгдеёжзийклмнопрстуфхцчшщъыьэюя1234567890 ,./;:()!";
        
        private List<BigInteger> _publicKey = new();
        private List<BigInteger> _privateKey = new();

        public Form1()
        {
            InitializeComponent();
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            GenerateKeys();
            lblOpenKey.Text = Utils.ListToString(_publicKey);
        }

        private void GenerateKeys()
        {
            _publicKey.Clear();
            _privateKey.Clear();

            var w = new int[] { 2, 7, 11, 21, 42, 89, 180, 354 };
            var sumW = w.Sum();

            var q = PrimeUtils.Generate(sumW.ToString().Length + 1);
            var r = MathUtils.GetMutuallySimpleNumber(q);

            foreach (var x in w)
            {
                _privateKey.Add(x);
            }

            _privateKey.Add(q);
            _privateKey.Add(r);

            foreach (var x in w)
            {
                _publicKey.Add((r * x) % q);
            }
        }

        private void btnEncrypt_Click(object sender, EventArgs e)
        {
            if (_privateKey?.Count + _publicKey?.Count <= 0)
            {
                MessageBox.Show("Ключ не сгенерирован", "Ошибка!", MessageBoxButtons.OK);
                return;
            }

            txtResult.Text = Encrypt(txtMsg.Text);
        }

        private string Encrypt(string text)
        {
            var encryptionString = string.Empty;

            for (var i = 0; i < text.Length; i++)
            {
                var temp = ALPHABET.IndexOf(text[i]);
                var charBinary = Convert.ToString(temp, 2);

                while (charBinary.Length != _publicKey.Count)
                {
                    charBinary = "0" + charBinary;
                }

                BigInteger sum = 0;

                for (var j = 0; j < charBinary.Length; j++)
                {
                    if (charBinary[j] == '1')
                    {
                        sum += _publicKey[j];
                    }
                }

                encryptionString += sum + " ";
            }

            return encryptionString;
        }

        private void btnDecrypt_Click(object sender, EventArgs e)
        {
            if (_privateKey?.Count + _publicKey?.Count <= 0)
            {
                MessageBox.Show("Ключ не сгенерирован", "Ошибка!", MessageBoxButtons.OK);
                return;
            }

            txtResult.Text = Decrypt(txtMsg.Text);
        }

        private string Decrypt(string text)
        {
            var decryptionString = string.Empty;
            var encodedSymbolsStr = text.Split(' ');

            for (var i = 0; i < encodedSymbolsStr.Length - 1; i++)
            {
                var r = _privateKey.ElementAt(_privateKey.Count - 1);
                var q = _privateKey.ElementAt(_privateKey.Count - 2);

                var temp = BigInteger.Parse(encodedSymbolsStr[i]);
                var inverse = MathUtils.ExtendedGcd(r, q).t;
                var c = (temp * inverse) % q;

                List<BigInteger> indexes = new();

                for (var j = _privateKey.Count - 3; j >= 0; j--)
                {
                    if (c - _privateKey.ElementAt(j) > 0)
                    {
                        c -= _privateKey.ElementAt(j);
                        indexes.Add(j);
                    }

                    if (c - _privateKey.ElementAt(j) == 0)
                    {
                        indexes.Add(j);
                        break;
                    }
                }

                var index = 0;
                for (var j = 0; j < _privateKey.Count - 2; j++)
                {
                    if (indexes.Contains(j))
                    {
                        index += (int)Math.Pow(2, _privateKey.Count - 3 - j);
                    }
                }

                decryptionString += ALPHABET[index];
            }

            return decryptionString;
        }
    }
}