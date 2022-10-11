namespace AHP;

public class Statistics
{
    public static double GaussianFactor(IEnumerable<double> list) => StandardDeviation(list) / Mean(list);
    public static double Mean(IEnumerable<double> numbers) => numbers.Sum() / numbers.Count();

    public static IEnumerable<double> Normalize(IEnumerable<double> list)
    {
        var sum = list.Sum();
        var returnList = new List<double>();

        foreach (var listItem in list)
        {
            returnList.Add(listItem / sum);
        }

        return returnList;
    }

    public static IEnumerable<double> Invert(IEnumerable<double> list)
    {
        var returnList = new List<double>();

        foreach (var listItem in list)
        {
            returnList.Add(1 / listItem);
        }

        return returnList;
    }

    public static double StandardDeviation(IEnumerable<double> list) => Math.Sqrt(SumOfSquares(list) / (list.Count() - 1));

    public static IEnumerable<double> ApplyWeigths(List<double> entry, List<int> weights)
    {
        var returnList = new List<double>();
        for (var i = 0; i < entry.Count(); i++)
        {
            returnList.Add(entry[i] * weights[i]);
        }

        return returnList;
    }

    private static double SumOfSquares(IEnumerable<double> list)
    {
        var mean = Statistics.Mean(list);
        var sumOfSquares = 0d;
        foreach (var itemList in list)
        {
            sumOfSquares += (itemList - mean) * (itemList - mean);
        }
        return sumOfSquares;
    }
}
