using API.Interfaces;
using API.Interfaces.Graphs.GraphParser;
using API.Interfaces.Graphs;
using System.Text;
using System.Diagnostics;

namespace API.Problems.NPComplete.NPC_CLIQUE.Solvers;
class BronKerboschSolver : ISolver {

    // --- Fields ---
    private string _solverName = "Bron-Kerbosch Algorithm";
    private string _solverDefinition = "This is an exact solver for the NP-Complete Clique problem using the Bron-Kerbosch pivot algorithm";
    private string _source = "Coen Bron and Joep Kerbosch. Algorithm 457: finding all cliques of an undirected graph. Communications of the ACM, 16(9): 575–577, 1973. doi:10.1145/362342.362367.";
    private string[] _contributors = {"Andrija Sevaljevic"};


    // --- Properties ---
    public string solverName {
        get {
            return _solverName;
        }
    }
    public string solverDefinition {
        get {
            return _solverDefinition;
        }
    }
    public string source {
        get {
            return _source;
        }
    }
    public string[] contributors{
        get{
            return _contributors;
        }
    }
    // --- Methods Including Constructors ---
    public BronKerboschSolver() {
        
    }

    public string solve(CLIQUE cliqueNOT) {

        // test case, minK = 100, numVertices = 1000, avg. connection between nodes = 17.5%
        // 137 to create random instance
        // 47065 to create clique instance
        // 27 to create adjecancy list
        // 19577 to solve it

        Stopwatch timer = new Stopwatch();
        timer.Start();

        int numVertices = 1200;
        int minK = 120;

        var instance = GenerateCliqueInstance(numVertices, minK);
        var instanceString = CliqueInstanceToString(instance);

        timer.Stop();
        Console.WriteLine(timer.ElapsedMilliseconds.ToString() + " to create random instance");
        timer.Restart();

        CLIQUE clique = new CLIQUE(instanceString);

        timer.Stop();
        Console.WriteLine(timer.ElapsedMilliseconds.ToString() + " to create clique instance");
        timer.Restart();

        Dictionary<int, List<int>> graph = new Dictionary<int, List<int>>();
        Dictionary<string,int> nodeToIndex = new Dictionary<string, int>();

        List<int> firstClique = new List<int>();

        // creating adjecency graph
        for(int i = 0; i < clique.nodes.Count; i++) {
            graph.Add(i, new List<int>());
            nodeToIndex.Add(clique.nodes[i],i);
        }
        // adding edges to adjacency graph
        foreach (var edge in clique.edges) {
            int index1 = nodeToIndex[edge.Key];
            int index2 = nodeToIndex[edge.Value];

            graph[index1].Add(index2);
            graph[index2].Add(index1);
        }

        timer.Stop();
        Console.WriteLine(timer.ElapsedMilliseconds.ToString() + " to create adjecancy list");
        timer.Restart();

        removeKnownNonSolutions(ref graph, clique.K); // finding nodes with degree less than K recursively

        List<int> R = new List<int>(); // Nodes in the current clique
        List<int> P = graph.Keys.ToList(); // Possible candidates
        List<int> X = new List<int>(); // Excluded nodes

        

        BronKerboschWithDegreePruningAlgorithm(R,P,X, clique.K, graph, ref firstClique);

        timer.Stop();
        Console.WriteLine(timer.ElapsedMilliseconds.ToString() + " to solve it");

        if(firstClique.Any()) return indexToString(firstClique,clique);

        return "{}";
    }
    static void BronKerboschWithDegreePruningAlgorithm(List<int> R, List<int> P, List<int> X, int minClique, Dictionary<int, List<int>> graph, ref List<int> firstClique)
    {
        if (P.Count == 0 && X.Count == 0)
        {
            if(R.Count >= minClique) firstClique = R;
            return;
        }

        int pivot = ChoosePivot(P, X, graph);

        foreach (int vertex in new List<int>(P.Except(graph[pivot])))
        {
            List<int> newR = new List<int>(R) { vertex };

            List<int> newP = graph[vertex].Intersect(P).ToList();
            List<int> newX = graph[vertex].Intersect(X).ToList();

            BronKerboschWithDegreePruningAlgorithm(newR, newP, newX, minClique, graph, ref firstClique);
            if(firstClique.Any()) return;

            P.Remove(vertex);
            X.Add(vertex);
        }
    }

    static int ChoosePivot(List<int> P, List<int> X, Dictionary<int, List<int>> graph)
    {
        List<int> candidates = P.Concat(X).ToList();
        int pivot = 1;
        foreach(var i in candidates)
            if(graph[i].Count >= pivot) pivot = i;
        return pivot;
    }

    private void removeKnownNonSolutions(ref Dictionary<int, List<int>> graph, int K) { 
        List<int> nodesToRemove = new List<int>();
        // selecting nodes
        foreach(var i in graph.Keys.ToList()) 
            if(graph[i].Count < K - 1) nodesToRemove.Add(i);
        // removing nodes
        foreach(var i in nodesToRemove) {
            foreach(var j in graph[i])
                graph[j].Remove(i);
            graph.Remove(i);
        }
        if(nodesToRemove.Any()) removeKnownNonSolutions(ref graph,K);
    }

    private string indexToString(List<int> solution, CLIQUE clique) {
        StringBuilder solutionStringBuilder = new StringBuilder("{");
        foreach (var i in solution)
        {
            solutionStringBuilder.Append(clique.nodes[i]);
            solutionStringBuilder.Append(',');
        }

        solutionStringBuilder.Length--; // Remove the trailing comma
        solutionStringBuilder.Append('}');

        return solutionStringBuilder.ToString();
    }

    public Tuple<HashSet<int>, HashSet<Tuple<int, int>>, int> GenerateCliqueInstance(int numVertices, int minK)
    {
        Random random = new Random();

        // Generate set of vertices
        HashSet<int> vertices = new HashSet<int>();
        for (int i = 1; i <= numVertices; i++)
        {
            vertices.Add(i);
        }

        // Ensure the graph contains a clique of size at least minK
        HashSet<Tuple<int, int>> edges = GenerateCliqueEdges(vertices, minK, random);

        // Generate K value (minimum clique size)
        int K = minK;

        return Tuple.Create(vertices, edges, K);
    }

    private HashSet<Tuple<int, int>> GenerateCliqueEdges(HashSet<int> vertices, int minK, Random random)
    {
        HashSet<Tuple<int, int>> edges = new HashSet<Tuple<int, int>>();
        List<int> vertexList = vertices.ToList();
        List<int> randomClique = vertices.ToList();

        for(int i = 0; i < vertexList.Count - minK; i++) {
            randomClique.Remove(random.Next(1, randomClique.Count + 1));
        }

        // Ensure at least minK vertices are connected to form a clique
        for (int i = 0; i < minK; i++)
        {
            for (int j = i + 1; j < minK; j++)
            {
                edges.Add(Tuple.Create(randomClique[i], randomClique[j]));
            }
        }

        // Add remaining edges randomly
        for (int i = 0; i < vertexList.Count; i++)
        {
            for (int j = i + 1; j < vertexList.Count; j++)
            {
                if (random.NextDouble() < 0.175 && !edges.Contains(Tuple.Create(vertexList[i], vertexList[j])))
                {
                    edges.Add(Tuple.Create(vertexList[i], vertexList[j]));
                }
            }
        }

        return edges;
    }

    public string CliqueInstanceToString(Tuple<HashSet<int>, HashSet<Tuple<int, int>>, int> instance)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("(({");
        sb.Append(string.Join(",", instance.Item1));
        sb.Append("},{");
        sb.Append(string.Join(",", instance.Item2.Select(e => $"{{{e.Item1},{e.Item2}}}")));
        sb.Append("}),");
        sb.Append(instance.Item3);
        sb.Append(")");

        return sb.ToString();
    }
}