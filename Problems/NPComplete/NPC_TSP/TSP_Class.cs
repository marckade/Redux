using API.Interfaces;
using API.Problems.NPComplete.NPC_TSP.Solvers;
using API.Problems.NPComplete.NPC_TSP.Verifiers;

namespace API.Problems.NPComplete.NPC_TSP;

class TSP : IProblem<BranchAndBoundSolver, TSPVerifier>
{

    // --- Fields ---
    private string _problemName = "TSP";
    private string _formalDefinition = "D = (d sub i sub j) | D is the matrix of intercity distances, d sub i sub j represents the distance from city i to city j";
    private string _problemDefinition = "The traveling salesperson problem finds an optimal tour (a path of minimum distance) given a matrix of cities and distances between them. The tour must start and end in the same city and each other city must be visited exactly once.";
    private string _source = "Dasgupta, Sanjoy, Christos H. Papadimitriou, and Umesh V. Vazirani. Algorithms. Boston: McGraw-Hill Higher Education, 2008.";
    private string _defaultInstance = "{ { int.MaxValue, 5, 12, 8 },{ 4, int.MaxValue, 10, 7 },{ 9, 14, int.MaxValue, 5 },{ 16, 3, 10, int.MaxValue } }";
    private string _wikiName ="";
    private string _D;
    private BranchAndBoundSolver _defaultSolver = new BranchAndBoundSolver();
    private TSPVerifier _defaultVerifier = new TSPVerifier();

    // --- Properties ---
    public string problemName
    {
        get
        {
            return _problemName;
        }
    }
    public string formalDefinition
    {
        get
        {
            return _formalDefinition;
        }
    }
    public string problemDefinition
    {
        get
        {
            return _problemDefinition;
        }
    }

    public string source
    {
        get
        {
            return _source;
        }
    }
    public string defaultInstance
    {
        get
        {
            return _defaultInstance;
        }
    }

    public string D
    {
        get 
        { 
            return _D; 
        }
    }
      public string wikiName {
        get {
            return _wikiName;
        }
    }

    public BranchAndBoundSolver defaultSolver 
    {
        get 
        {
            return _defaultSolver;
        }
    }

    public TSPVerifier defaultVerifier
    {
        get
        {
            return _defaultVerifier;
        }
    }

    // --- Methods Including Constructors ---
    public TSP()
    {
        _D = _defaultInstance;
    }
    public TSP(string matrix)
    {
        _D = matrix;
    }
}