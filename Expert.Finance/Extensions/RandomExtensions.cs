namespace Expert.Finance.Extensions;

public static class RandomExtensions
{
    public static IEnumerable<float> GetRandomValuesWithSum(float minvalue, float sum, int k)
    {
        var random = new Random();
        for (int i = 0; i < k - 1; i++)
        {
            if (sum == 0)
            {
                yield return 0;   
            }
            var temp = random.NextSingle() % sum + 1;
            sum -= temp;
            yield return temp;
        }

        yield return sum;
    }
}