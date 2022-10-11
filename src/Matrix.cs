namespace AHP;

public class Matrix
{
    public List<string> Titles { get; set; }
    public List<string> MaxMinEntries { get; set; }
    public List<int> Weigths { get; set; }
    public List<Entry> Entries { get; set; }
    public List<double> GaussianFactors { get; set; }
    public Matrix(string[] titles, string[] maxMinentries, List<int> weigths, List<string[]> entries)
    {
        this.Titles = titles.ToList();
        this.MaxMinEntries = maxMinentries.ToList();
        this.Weigths = weigths;
        this.Entries = new List<Entry>();
        this.GaussianFactors = new List<double>();

        foreach (var entry in entries)
        {
            this.Entries.Add(new Entry(entry));
        }
    }

    public static Matrix NormalizeMatrix(Matrix inputMatrix)
    {
        for (var j = 0; j < inputMatrix.Entries[0].Properties.Count(); j++)
        {
            var listToNormalize = new List<double>();
            for (var i = 0; i < inputMatrix.Entries.Count(); i++)
            {
                listToNormalize.Add(inputMatrix.Entries[i].Properties[j]);
            }
            if (inputMatrix.MaxMinEntries[j + 1] == "min")
            {
                listToNormalize = Statistics.Invert(listToNormalize).ToList();
            }
            var normalizedList = Statistics.Normalize(listToNormalize).ToList();

            for (var i = 0; i < inputMatrix.Entries.Count(); i++)
            {
                inputMatrix.Entries[i].Properties[j] = normalizedList[i];
            }
        }
        return inputMatrix;
    }
}

public class Entry
{
    public Entry(string[] entry)
    {
        this.Name = entry[0];
        this.Properties = new List<double>();
        for (var i = 1; i < entry.Count(); i++)
        {
            this.Properties.Add(double.Parse(entry[i]));
        }
    }

    public Entry(string name, IEnumerable<double> itens)
    {
        this.Name = name;
        this.Properties = itens.ToList();
    }

    public string Name { get; set; }
    public List<double> Properties { get; set; }
}

public class Result
{
    public Result(List<double> gaussianFactor, Entry entry)
    {
        this.Name = entry.Name;
        var score = 0d;
        for (var i = 0; i < entry.Properties.Count(); i++)
        {
            score += entry.Properties[i] * gaussianFactor[i];
        }
        this.Score = score;
    }
    public string Name { get; set; }
    public double Score { get; set; }
}