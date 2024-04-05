using API.Interfaces;
using System.Diagnostics;
using DlxLib;
using API.Tools.ProblemGenerator;
using System.Text;

namespace API.Problems.NPComplete.NPC_ExactCover.Solvers;
class DancingLinks : ISolver
{

    // --- Fields ---
    private string _solverName = "Algorithm XD";
    private string _solverDefinition = "This is a solver for the exact cover problem that utilizes an implementation of Donald Knuth's Dancing Links algorithm using dictionaries.";
    private string _source = "Knuth, Donald E. \"Dancing links.\" arXiv preprint cs/0011047 (2000).";
    private string[] _contributors = { "Andrija Sevaljevic" };


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
    public string[] contributors
    {
        get
        {
            return _contributors;
        }
    }
    // --- Methods Including Constructors ---
    public DancingLinks()
    {

    }

    public string solve(ExactCover exactCover)
    {

        Stack<int> selectedSets = new Stack<int>();
        Dictionary<int, List<int>> Y = new Dictionary<int, List<int>>();
        Dictionary<int, List<int>> X = new Dictionary<int, List<int>>();
        Dictionary<string, int> names = new Dictionary<string, int>();

        for (int i = 0; i < exactCover.X.Count; i++)
        {
            names.Add(exactCover.X[i], i);
            X.Add(i, new List<int>());
        }

        for (int i = 0; i < exactCover.S.Count; i++)
        {
            Y.Add(i, new List<int>());
            foreach (var j in exactCover.S[i])
            {
                X[names[j]].Add(i);
                Y[i].Add(names[j]);
            }
        }

        iterate(Y, ref X, ref selectedSets);

        if (selectedSets.Any())
        {
            return solutionToCertificate(selectedSets,exactCover);
        }

        return "{}";
    }

    private void iterate(Dictionary<int, List<int>> Y, ref Dictionary<int, List<int>> X, ref Stack<int> solution)
    {
        if (!X.Keys.Any()) return;
        Stack<List<int>> columns = new Stack<List<int>>();
        int minimumColumn = findMinimumColumn(X);
        foreach (var row in new List<int>(X[minimumColumn]))
        {
            solution.Push(row);
            X = select(Y, X, row, ref columns);
            iterate(Y, ref X, ref solution);
            if (!X.Keys.Any()) return;
            X = deselect(Y, X, row, ref columns);
            solution.Pop();

        }

    }

    private Dictionary<int, List<int>> select(Dictionary<int, List<int>> Y, Dictionary<int, List<int>> X, int row, ref Stack<List<int>> columns)
    {
        foreach (var j in Y[row])
        {
            foreach (var i in X[j])
                foreach (var k in Y[i])
                    if (k != j)
                        X[k].Remove(i);
            columns.Push(X[j]);
            X.Remove(j);
        }
        return X;
    }

    private Dictionary<int, List<int>> deselect(Dictionary<int, List<int>> Y, Dictionary<int, List<int>> X, int row, ref Stack<List<int>> columns)
    {
        List<int> reversed = new List<int>(Y[row]);
        reversed.Reverse();
        foreach (var j in reversed)
        {
            if (!X.ContainsKey(j)) X.Add(j, new List<int>());
            X[j] = columns.Pop();
            foreach (var i in X[j])
                foreach (var k in Y[i])
                    if (k != j)
                    {
                        if (!X.ContainsKey(k)) X.Add(k, new List<int>());
                        X[k].Add(i);
                    }
        }
        return X;
    }

    private int findMinimumColumn(Dictionary<int, List<int>> X)
    {
        int minimumColumn = X.Keys.First();
        foreach (var kv in X)
        {
            if (kv.Value.Count < X[minimumColumn].Count)
            {
                minimumColumn = kv.Key;
            }
        }
        return minimumColumn;
    }

    public string solutionToCertificate(Stack<int> selectedSets, ExactCover exactCover)
    {
        StringBuilder solutionStringBuilder = new StringBuilder("{");
        foreach (var i in selectedSets)
        {
            solutionStringBuilder.Append('{');
            foreach (var j in exactCover.S[i])
            {
                solutionStringBuilder.Append(j);
                solutionStringBuilder.Append(',');
            }
            solutionStringBuilder.Length--;
            solutionStringBuilder.Append("},");
        }
        solutionStringBuilder.Length--;
        solutionStringBuilder.Append('}');
        return solutionStringBuilder.ToString();
    }
}
