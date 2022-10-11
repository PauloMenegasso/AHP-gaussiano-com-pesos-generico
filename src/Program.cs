namespace AHP;

internal class Program
{
    private static void Main(string[] args)
    {
        var path = Directory.GetCurrentDirectory();

        var file = File.ReadAllLines($"{path}/input.csv");
        var titles = file[0].Split(',');
        var maxMinEntries = file[1].Split(',');
        var weights = file[2].Split(',');
        var numericWeights = new List<int>();

        foreach (var weigth in weights)
        {
            try
            {
                numericWeights.Add(Int32.Parse(weigth));
            }
            catch
            {
                numericWeights.Add(0);
            }
        }

        var entries = new List<string[]>();
        for (var i = 3; i < file.Count(); i++)
        {
            entries.Add(file[i].Split(','));
        }

        var matrix = new Matrix(titles, maxMinEntries, numericWeights, entries);

        var normalizedMatrix = Matrix.NormalizeMatrix(matrix);
        for (var i = 0; i < normalizedMatrix.Entries[0].Properties.Count(); i++)
        {
            var entryValues = new List<double>();
            for (var j = 0; j < normalizedMatrix.Entries.Count(); j++)
            {
                entryValues.Add(normalizedMatrix.Entries[j].Properties[i]);
            }
            var gaussianFactor = Statistics.GaussianFactor(entryValues);
            gaussianFactor = gaussianFactor * matrix.Weigths[i + 1];
            normalizedMatrix.GaussianFactors.Add(gaussianFactor);
        }

        normalizedMatrix.GaussianFactors = Statistics.Normalize(normalizedMatrix.GaussianFactors).ToList();

        var results = new List<Result>();

        for (var i = 0; i < normalizedMatrix.Entries.Count(); i++)
        {
            var result = new Result(normalizedMatrix.GaussianFactors, normalizedMatrix.Entries[i]);
            results.Add(result);
        }

        var writeResults = new List<string>();
        foreach (var result in results)
        {
            writeResults.Add($"Name: {result.Name}, Value(%): {result.Score}");
        }

        File.WriteAllLines($"{path}/output.csv", writeResults);
    }
}
