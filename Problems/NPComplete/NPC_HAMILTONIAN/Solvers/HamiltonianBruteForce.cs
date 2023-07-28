using API.Interfaces;
using API.Interfaces.Graphs.GraphParser;
using API.Interfaces.Graphs;

namespace API.Problems.NPComplete.NPC_HAMILTONIAN.Solvers;
class HamiltonianBruteForce : ISolver
{

    // --- Fields ---
    private string _solverName = "Hamiltonian Brute Force";
    private string _solverDefinition = "This is a brute force solver for the NP-Complete Hamiltonian problem";
    private string _source = "This solver was contributed by Andrija Sevaljevic";
    private string[] _contributers = { "Andrija Sevaljevic" };


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
    public string[] contributers
    {
        get
        {
            return _contributers;
        }
    }
    // --- Methods Including Constructors ---
    public HamiltonianBruteForce()
    {

    }

    private string combinationToCertificate(List<int> combination, List<string> nodes) {
        string certificate = "";
        foreach(int i in combination) {
            certificate += nodes[i - 1] + ',';
        }
        return "{" + certificate + certificate.Split(',')[0] + "}";
    }
    
    public static List<List<int>> GenerateCombinations(int x)
    {
        List<int> currentCombination = new List<int>();
        for (int i = 1; i <= x; i++)
        {
            currentCombination.Add(i);
        }

        List<List<int>> combinations = new List<List<int>>();
        combinations.Add(new List<int>(currentCombination));

        while (true)
        {
            if (GetNextCombination(currentCombination))
            {
                combinations.Add(new List<int>(currentCombination));
            }
            else
            {
                break; // All combinations have been generated
            }
        }

        return combinations;
    }

    private static bool GetNextCombination(List<int> combination)
    {
        int x = combination.Count;
        int i = x - 2;
        while (i >= 0 && combination[i] >= combination[i + 1])
        {
            i--;
        }

        if (i < 0)
        {
            return false; // No more combinations
        }

        int j = x - 1;
        while (combination[j] <= combination[i])
        {
            j--;
        }

        // Swap elements at indices i and j
        int temp = combination[i];
        combination[i] = combination[j];
        combination[j] = temp;

        // Reverse the sequence from i+1 to the end
        combination.Reverse(i + 1, x - i - 1);

        return true;
    }


    public string solve(HAMILTONIAN hamiltonian)
    {
        List<List<int>> combinations = GenerateCombinations(hamiltonian.nodes.Count);

        foreach (List<int> combination in combinations)
        {
            string certificate = combinationToCertificate(combination, hamiltonian.nodes);
            if (hamiltonian.defaultVerifier.verify(hamiltonian, certificate))
            {
                return certificate;
            }
        }

        return "{}";
    }

    /// <summary>
    /// Given Clique instance in string format and solution string, outputs a solution dictionary with 
    /// true values mapped to nodes that are in the solution set else false. 
    /// </summary>
    /// <param name="problemInstance"></param>
    /// <param name="solutionString"></param>
    /// <returns></returns>
    // public Dictionary<string,bool> getSolutionDict(string problemInstance, string solutionString){

    //     Dictionary<string, bool> solutionDict = new Dictionary<string, bool>();
    //     GraphParser gParser = new GraphParser();
    //     CliqueGraph cGraph = new CliqueGraph(problemInstance, true);
    //     List<string> problemInstanceNodes = cGraph.nodesStringList;
    //     List<string> solvedNodes = gParser.getNodesFromNodeListString(solutionString);

    //     // Remove solvedNodes from instanceNodes
    //     foreach(string node in solvedNodes){
    //     problemInstanceNodes.Remove(node);
    //     // Console.WriteLine("Solved nodes: "+node);
    //     solutionDict.Add(node, true);
    //    }
    //     // Add solved nodes to dict as {name, true}
    //     // Add remaining instance nodes as {name, false}

    //     foreach(string node in problemInstanceNodes){

    //             solutionDict.Add(node, false);
    //     }


    //     return solutionDict;
    // }
}
