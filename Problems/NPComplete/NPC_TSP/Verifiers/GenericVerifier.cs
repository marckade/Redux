using API.Interfaces;

namespace API.Problems.NPComplete.NPC_TSP.Verifiers;

class TSPVerifier : IVerifier
{
    // --- Fields ---
    private string _verifierName = "Generic Verifier";
    private string _verifierDefinition = "This is a verifier for TSP";
    private string _source = "";
    private string[] _contributers = { "Daniel Whitaker"};

    private string _certificate = "";

    // --- Properties ---
    public string verifierName
    {
        get
        {
            return _verifierName;
        }
    }
    public string verifierDefinition
    {
        get
        {
            return _verifierDefinition;
        }
    }
    public string source
    {
        get
        {
            return _source;
        }
    }
    public string[] contributers{
            get{
                return _contributers;
            }
        }
      public string certificate {
        get {
            return _certificate;
        }
    }


    // --- Methods Including Constructors ---
    public TSPVerifier()
    {

    }

    // Is the given tour valid?
    // inputs:
    //  tour - list of integers representing tour
    //  matrix - 2d integer array representing the cost between each city
    // output:
    //  boolean - true if tour is valid, false otherwise 
    public bool isTour(string t, TSP tsp)
    {
        if (t == null || tsp == null)
        {
            return false;
        }
        
        List<int> tour = stringToList(t);
        int[,] matrix = stringToMatrix(tsp.D);

        bool isValid = true;
        for (int i = 0; i < tour.Count; i++)
        {
            if (i < tour.Count - 1)
            {
                // Check for non-infinite edge between source and destination city
                if (matrix[tour[i], tour[i + 1]] == int.MaxValue)
                {
                    isValid = false;
                }
            }
            else
            {
                // Check for non-infinite edge from last city back to starting city
                if (matrix[tour[i], 0] == int.MaxValue)
                {
                    isValid = false;
                }
            }
        }
        return isValid;
    }

    public List<int> stringToList(string s)
    {
        int count = s.Count(c => (c == ',')) + 1;
        List<int> tour = new List<int>();
        string temp;

        for (int i = 0; i < count; i++)
        {
            if (i == count - 1)
            {
                temp = s;
            }
            else
            {
                temp = s.Substring(0, s.IndexOf(','));
            }
            tour.Add(Int32.Parse(temp));
            s = s.Substring(s.IndexOf(',') + 2);
        }

        return tour;
    }

    public int[,] stringToMatrix(string s)
    {
        s = s.Substring(4, s.Length - 6);
        int rows = s.Count(c => (c == '}'));
        int [,] matrix = new int[rows, rows];
        for(int i = 0; i < rows; i++)
        {
            for(int j = 0; j < rows; j++)
            {
                string temp;
                if (j == rows -1 )
                {
                    temp = s.Substring(0, s.IndexOf(' '));
                }
                else
                {
                    temp = s.Substring(0, s.IndexOf(','));
                }
                if (temp == "int.MaxValue")
                {
                    matrix[i,j] = int.MaxValue;
                }
                else 
                {
                    matrix[i,j] = Int32.Parse(temp);
                }
                s = s.Substring(s.IndexOf(',') + 2);
            }
        }
        return matrix;
    }
}
