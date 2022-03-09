using System.Text;

namespace Lab6
{
    public static class Utils
    {
        public static string ListToString(List<System.Numerics.BigInteger> list)
        {
            var str = new StringBuilder(list.Count + 2);
            str.Append('(');

            for (var i = 0; i < list.Count - 1; i++)
            {
                str.Append(list[i].ToString() + ", ");
            }

            str.Append(list.Last().ToString() + ')');
            return str.ToString();
        }
    }
}
