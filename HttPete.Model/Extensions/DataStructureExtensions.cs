namespace HttPete.Model.Extensions
{
    public static class DataStructureExtensions
    {
        public static int[] ParseArray(this IEnumerable<string> data)
        {
            int[] res = new int[data.Count()];

            for (int i = 0; i < res.Length; i++)
            {
                res[i] = int.Parse(data.ElementAt(i));
            }

            return res;
        }
    }
}
