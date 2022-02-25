using System.Numerics;

namespace Lab5
{
    public partial class Form1 : Form
    {
        private static BigInteger N;
        private static BigInteger PublicKey;
        private static BigInteger PrivateKey;

        public Form1()
        {
            InitializeComponent();
        }

        private void btnEncrypt_Click(object sender, EventArgs evt)
        {
            var userStr = msgTextBox.Text;
            var userStrCode = Utils.StrToNumbers(userStr);

            if (string.IsNullOrEmpty(pTextBox.Text) || string.IsNullOrEmpty(qTextBox.Text))
            {
                GeneratePrimes();
            }

            var p = BigInteger.Parse(pTextBox.Text);
            var q = BigInteger.Parse(qTextBox.Text);

            N = BigInteger.Multiply(p, q);
            var phi = BigInteger.Multiply(p - 1, q - 1);

            PublicKey = MathUtils.GetMutuallySimpleNumber(phi);
            PrivateKey = MathUtils.ExtendedGcd(PublicKey, phi).t;

            var encodedUserStr = Utils.TransformStrCode(userStrCode, N, PublicKey, true);

            resultTextBox.Text = encodedUserStr;
        }

        private void btnDecrypt_Click(object sender, EventArgs e)
        {
            var userStr = msgTextBox.Text;
            var decodedUserStr = Utils.TransformStrCode(userStr, N, PrivateKey, false);

            resultTextBox.Text = Utils.NumbersToStr(decodedUserStr);
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            GeneratePrimes();
        }

        private void GeneratePrimes()
        {
            var p = PrimeUtils.Generate(5);
            var q = PrimeUtils.Generate(4);

            pTextBox.Text = p.ToString();
            qTextBox.Text = q.ToString();
        }
    }
}