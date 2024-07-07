namespace API.Tools.ProblemGenerator;
class ProblemGenerator
{
    Random rand;
    public ProblemGenerator()
    {
        rand = new Random();
    }

    public string GenerateExactCover(int universalSize)
    {
        return GenerateExactCover(universalSize,(int)(universalSize*0.25),(int)(universalSize*(1.0/8)),(int)(universalSize*0.5),(int)(universalSize*0.25));
    }

    public string GenerateExactCover(int universalSize, int avgSize, int numDeviation)
    {
        return GenerateExactCover(universalSize, avgSize, 0, numDeviation, 0);
    }

    public string GenerateExactCover(int universalSize, int avgSize, int sizeDeviation, int avgSets, int numDeviation)
    {
        string str = "{";

        int numSets = avgSets + rand.Next(-numDeviation, numDeviation);

        List<string> sets = GenerateSolved(universalSize, avgSize, sizeDeviation);

        while (sets.Count < numSets)
        {
            sets.Add(GenerateSet(universalSize, avgSize, sizeDeviation));
        }

        Shuffle(sets);

        foreach (string set in sets)
        {
            str += set + ",";
        }

        str = str.TrimEnd(',') + " : {";

        for (int i = 0; i < universalSize; i++)
        {
            str += i + ",";
        }
        str = str.TrimEnd(',') + "}}";

        return str;
    }

    public string GenerateSet(int universalSize, int avgSize, int deviation)
    {
        List<int> ints = GenerateListInt(universalSize);

        return GenerateSet(universalSize, avgSize, deviation, ints);
    }

    public string GenerateSet(int universalSize, int avgSize, int deviation, List<int> ints)
    {
        string str = "{";
        int setSize = avgSize + rand.Next(-deviation, deviation);

        for (int i = 0; i < setSize; i++)
        {
            int randInd = rand.Next(ints.Count);
            str += ints[randInd] + ",";
            ints.RemoveAt(randInd);
            if (ints.Count == 0) break;
        }
        str = str.TrimEnd(',') + "}";

        return str;

    }

    public List<int> GenerateListInt(int universalSize)
    {
        List<int> ints = new();
        for (int i = 0; i < universalSize; i++)
        {
            ints.Add(i);
        }
        return ints;
    }

    public List<string> GenerateSolved(int universalSize, int avgSize, int sizeDeviation)
    {
        List<int> ints = GenerateListInt(universalSize);

        List<string> solvedSets = new();

        while (ints.Count > 0)
        {
            solvedSets.Add(GenerateSet(universalSize, avgSize, sizeDeviation, ints));
        }

        return solvedSets;
    }

    public void Shuffle(List<string> list)  
{  
    int n = list.Count;  
    while (n > 1) {  
        n--;  
        int k = rand.Next(n + 1);
            (list[n], list[k]) = (list[k], list[n]);
        }
    }
}