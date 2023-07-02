using API.Interfaces;

namespace API.Problems.NPComplete.NPC_TSP.Solvers;
class GreedySolver : ISolver
{

    // --- Fields ---
    private string _solverName = "Greedy Solver";
    private string _solverDefinition = "This is a greedy solver for TSP";
    private string _source = "Daniel Whitaker";
    private string[] _contributers = { "Daniel Whitaker"};


    // --- Properties ---
    public string solverName
    {
        get
        {
            return _solverName;
        }
    }
    public string solverDefinition
    {
        get
        {
            return _solverDefinition;
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
    // --- Methods Including Constructors ---
    public GreedySolver()
    {

    }

    public (List<int>, int) greedy(int[,] matrix)
    {
        Random r = new Random();
        bool foundTour = false;
        List<int> visited = new List<int>();
        visited.Add(0);
        int start = 0;
        int cost = 0;

        List<int> unvisited = new List<int>();

        for (int i = 1; i < matrix.GetLength(0); i++)
        {
            unvisited.Add(i);
        }

        while (!foundTour)
        {
            while (visited.Count < matrix.GetLength(0))
            {
                int i = findShortestEdge(matrix, visited, unvisited, start);
                visited.Add(unvisited[i]);
                cost += matrix[start, unvisited.ElementAt(i)];
                start = unvisited[i];
                unvisited.RemoveAt(i);
            }
            cost += matrix[start, 0];
            if (cost < int.MaxValue && cost > -1)
            {
                foundTour = true;
            }
            else
            {
                visited = new List<int>();
                visited.Add(0);
                unvisited = new List<int>();
                for (int i = 1; i < matrix.GetLength(0); i++)
                {
                    unvisited.Add(i);
                }
                int random = r.Next(1, matrix.GetLength(0));
                visited.Add(random);
                unvisited.Remove(random);
                start = random;
                cost = matrix[0, random];
            }
        }

        return (visited, cost);
    }

    public string greedy(string m, bool returnString)
    {
        int[,] matrix = stringToMatrix(m);
        Random r = new Random();
        bool foundTour = false;
        List<int> visited = new List<int>();
        visited.Add(0);
        int start = 0;
        int cost = 0;

        List<int> unvisited = new List<int>();

        for (int i = 1; i < matrix.GetLength(0); i++)
        {
            unvisited.Add(i);
        }

        while (!foundTour)
        {
            while (visited.Count < matrix.GetLength(0))
            {
                int i = findShortestEdge(matrix, visited, unvisited, start);
                visited.Add(unvisited[i]);
                cost += matrix[start, unvisited.ElementAt(i)];
                start = unvisited[i];
                unvisited.RemoveAt(i);
            }
            cost += matrix[start, 0];
            if (cost < int.MaxValue && cost > -1)
            {
                foundTour = true;
            }
            else
            {
                visited = new List<int>();
                visited.Add(0);
                unvisited = new List<int>();
                for (int i = 1; i < matrix.GetLength(0); i++)
                {
                    unvisited.Add(i);
                }
                int random = r.Next(1, matrix.GetLength(0));
                visited.Add(random);
                unvisited.Remove(random);
                start = random;
                cost = matrix[0, random];
            }
        }

        string result = "";
        foreach (int city in visited)
        {
            result += city.ToString() + ", ";
        }
        result = result.Substring(0, result.Length - 2);

        return result;
    }

    public int findShortestEdge(int[,] matrix, List<int> visited, List<int> unVisited, int source)
    {
        int min = int.MaxValue;
        int index = -1;
        for (int i = 0; i < unVisited.Count; i++)
        {
            int cost = matrix[source, unVisited.ElementAt(i)];
            if (cost < min)
            {
                min = cost;
                index = i;
            }
        }
        return index;
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