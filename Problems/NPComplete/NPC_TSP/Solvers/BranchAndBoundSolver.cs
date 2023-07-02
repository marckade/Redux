using API.Interfaces;
using API.Problems.NPComplete.NPC_TSP.Solvers;

namespace API.Problems.NPComplete.NPC_TSP.Solvers;
class BranchAndBoundSolver : ISolver
{

    // --- Fields ---
    private string _solverName = "Branch and Bound Solver";
    private string _solverDefinition = "This is a branch and bound solver for TSP";
    private string _source = "Daniel Whitaker";
    private string[] _contributers = { "Daniel Whitaker"};

    private GreedySolver _gs = new GreedySolver();

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
    public GreedySolver gs
    {
        get
        {
            return _gs;
        }
    }
    // --- Methods Including Constructors ---
    public BranchAndBoundSolver()
    {

    }

    public string branchAndBound(TSP problem)
    {
        int[,] matrix = stringToMatrix(problem.D);
        int[,] m = (int[,])matrix.Clone();
        List<int> bestTour = new List<int>();
        int bssf = 0;
        // Run greedy algorithm to get best solution so far (bssf)
        (bestTour, bssf) = gs.greedy(m);

        m = (int[,])matrix.Clone();

        // Start initial tour by adding first city/vertex to it
        List<int> visited = new List<int>();
        visited.Add(0);

        // Create a list of unvisited cities/vertices
        List<int> unvisited = new List<int>();

        for (int i = 1; i < matrix.GetLength(0); i++)
        {
            unvisited.Add(i);
        }

        int lb;

        // Create initial reduced cost matrix
        (m, lb) = reduceMatrix(m);

        // create first state
        object[] state = new object[5];
        state[0] = 0;
        state[1] = m;
        state[2] = unvisited;
        state[3] = visited;
        state[4] = lb;

        // Create queue
        List<object[]> queue = new List<object[]>();

        queue.Add(state);

        while (queue.Count > 0)
        {
            // Choose problem to expand
            object[] list = new object[5];
            list = queue.ElementAt(queue.Count - 1);
            queue.RemoveAt(queue.Count - 1);

            // Initialize subproblem variables
            int newLB = (int)list[4];

            // Check if lowerbound is greater than bssf
            if (newLB > bssf)
            {
                continue;
            }

            int row = (int)list[0];
            int[,] mCopy = (int[,])((int[,])list[1]).Clone();
            List<int> newUnvisited = new List<int>((List<int>)list[2]);
            List<int> newVisited = new List<int>((List<int>)list[3]);

            // If only 1 city is left unvisited check for solution
            if (newUnvisited.Count == 1)
            {
                newVisited.Add(newUnvisited.ElementAt(0));
                int solCost = calculateCost(newVisited, matrix);
                //Console.WriteLine("found solution of cost: " + solCost.ToString());
                if (solCost < bssf)
                {
                    bssf = solCost;
                    bestTour = new List<int>(newVisited);
                }
                continue;
            }

            // Explore minimum value of cost matrix
            int min, index;
            (min, index) = getMinRow(mCopy, row);

            if (min < int.MaxValue)
            {
                ((int[,])list[1])[row, index] = int.MaxValue;
                object[] oldList = (object[])list.Clone();
                queue.Add(oldList);
            }
            else if (min == int.MaxValue)
            {
                continue;
            }

            // Get new lower bound
            newLB += min;

            if (newLB > bssf)
            {
                continue;
            }

            // Update cost matrix for new city visited
            mCopy = markInfinite(mCopy, row, index);

            // Can't go back to first city unless in last city
            mCopy = setInfinite(mCopy, index, 0);

            // Update visited and unvisited
            newVisited.Add(index);
            newUnvisited.Remove(index);

            // Reduce new matrix and check new lowerbound
            int cost;
            (mCopy, cost) = reduceMatrix(mCopy);

            newLB += cost;
            if (newLB <= bssf)
            {
                object[] newState = new object[5];
                newState[0] = index;
                newState[1] = mCopy;
                newState[2] = newUnvisited;
                newState[3] = newVisited;
                newState[4] = newLB;
                queue.Add(newState);
            }
        }

        string result = "";
        foreach (int city in bestTour)
        {
            result += city.ToString() + ", ";
        }
        result = result.Substring(0, result.Length - 2);

        return result;
    }

    private void printMatrix(int[,] matrix)
    {
        for (int i = 0; i < matrix.GetLength(0); i++)
        {
            for (int j = 0; j < matrix.GetLength(1); j++)
            {
                if (matrix[i, j] == int.MaxValue)
                {
                    Console.Write("inf ");
                }
                else
                {
                    Console.Write(matrix[i, j] + " ");
                }
            }
            Console.Write("\n");
        }
    }

    private (int, int) getMinRow(int[,] matrix, int row)
    {
        int min = int.MaxValue;
        int index = -1;
        for (int i = 0; i < matrix.GetLength(0); i++)
        {
            if (matrix[row, i] < min)
            {
                min = matrix[row, i];
                index = i;
            }
        }
        return (min, index);
    }

    private int getMinCol(int[,] matrix, int col)
    {
        int min = int.MaxValue;
        for (int i = 0; i < matrix.GetLength(1); i++)
        {
            if (matrix[i, col] < min)
            {
                min = matrix[i, col];
            }
        }
        return min;
    }

    private int[,] reduceRow(int[,] matrix, int row, int min)
    {
        for (int i = 0; i < matrix.GetLength(0); i++)
        {
            if (matrix[row, i] != int.MaxValue)
            {
                matrix[row, i] -= min;
            }
        }
        return matrix;
    }

    private int[,] reduceCol(int[,] matrix, int col, int min)
    {
        for (int i = 0; i < matrix.GetLength(1); i++)
        {
            if (matrix[i, col] != int.MaxValue)
            {
                matrix[i, col] -= min;
            }
        }
        return matrix;
    }

    private (int[,], int) reduceMatrix(int[,] matrix)
    {
        int lower_bound = 0;
        int min, index;

        for (int i = 0; i < matrix.GetLength(0); i++)
        {
            (min, index) = getMinRow(matrix, i);
            if (min < int.MaxValue && min > 0)
            {
                lower_bound += min;
            }
            matrix = reduceRow(matrix, i, min);
        }

        for (int i = 0; i < matrix.GetLength(1); i++)
        {
            min = getMinCol(matrix, i);
            if (min < int.MaxValue && min > 0)
            {
                lower_bound += min;
            }
            matrix = reduceCol(matrix, i, min);
        }

        return (matrix, lower_bound);
    }

    private int[,] setInfinite(int[,] matrix, int row, int col)
    {
        matrix[row, col] = int.MaxValue;
        return matrix;
    }

    private int[,] markInfinite(int[,] matrix, int row, int col)
    {
        // mark row infinite
        for (int i = 0; i < matrix.GetLength(0); i++)
        {
            matrix[row, i] = int.MaxValue;
        }

        // mark column infinite
        for (int i = 0; i < matrix.GetLength(1); i++)
        {
            matrix[i, col] = int.MaxValue;
        }

        matrix[col, row] = int.MaxValue;

        return matrix;
    }

    private int calculateCost(List<int> visited, int[,] matrix)
    {
        int cost = 0;
        for (int i = 0; i < visited.Count; i++)
        {
            if (i < visited.Count - 1)
            {
                cost += matrix[visited[i], visited[i + 1]];
            }
            else
            {
                cost += matrix[visited[i], 0];
            }
        }
        return cost;
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