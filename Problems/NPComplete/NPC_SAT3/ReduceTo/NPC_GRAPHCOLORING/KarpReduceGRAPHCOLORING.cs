using API.Interfaces;
using API.Problems.NPComplete.NPC_GRAPHCOLORING;


namespace API.Problems.NPComplete.NPC_SAT3.ReduceTo.NPC_GRAPHCOLORING;

class KarpReduction : IReduction<SAT3, GRAPHCOLORING>
{


    #region Fields
    private string _reductionDefinition = "Karp's reduction converts each clause from a 3CNF into an OR gadgets to establish the truth assignments using labels.";
    private string _source = "http://cs.bme.hu/thalg/3sat-to-3col.pdf.";
    private SAT3 _reductionFrom;
    private GRAPHCOLORING _reductionTo;
    private string _complexity = "O(n^2)";

    #endregion

    #region Properties

    public string reductionDefinition
    {
        get
        {
            return _reductionDefinition;
        }
    }
    public string source
    {
        get
        {
            return _source;
        }
    }

    public string complexity
    {
        get
        {
            return _complexity;
        }

        set
        {
            _complexity = value;
        }
    }
    public SAT3 reductionFrom
    {
        get
        {
            return _reductionFrom;
        }
        set
        {
            _reductionFrom = value;
        }
    }


    public GRAPHCOLORING reductionTo
    {
        get
        {
            return _reductionTo;
        }
        set
        {
            _reductionTo = value;
        }
    }

    #endregion

    #region Constructors
    public KarpReduction(SAT3 from)
    {
        _reductionFrom = from;
        _reductionTo = reduce();

    }


    # endregion

    # region Methods


    //The below code is reducing the SAT3 instance to a GRAPHCOLORING instance.
    public GRAPHCOLORING reduce()
    {

        // color palette 
        // 0 : False, 1 : True,  2 : Base

        string[] palette = { "false", "true", "base" };

        SAT3 SAT3Instance = _reductionFrom;
        GRAPHCOLORING reducedGRAPHCOLORING = new GRAPHCOLORING();
        Dictionary<string, string> coloring = new Dictionary<string, string>();


        // ------- Add nodes -------

        List<string> nodes = new List<string>(palette);

        for (int i = 0; i < palette.Length; i++)
        {
            coloring.Add(palette[i], i.ToString());
        }

        List<string> variables = SAT3Instance.literals.Distinct().ToList();

        for (int i = 0; i < variables.Count; i++)
        {
            nodes.Add(variables[i]);
            coloring.Add(variables[i], "-1");
        }

        // Create clause nodes 
        List<List<string>> clauses = new List<List<string>>();
        for (int i = 0; i < SAT3Instance.clauses.Count; i++)
        {

            List<string> tempClause = new List<string>();

            for (int j = 0; j < 6; j++)
            {
                tempClause.Add("C" + i + "N" + j);
                coloring.Add("C" + i + "N" + j, "-1");
            }

            // Add clause-nodes to list of clauses 
            clauses.Add(tempClause);

            // Add clause to node list
            nodes.AddRange(tempClause);
        }

        //Set GRAPHCOLORING nodes
        reducedGRAPHCOLORING.nodes = nodes;


        // -------------  Add edges -----------------------
        List<KeyValuePair<string, string>> edges = new List<KeyValuePair<string, string>>();

        // holds edges for parsing problem instance 
        List<string> instanceEdges = new List<string>();


        // Connect palette edges 
        for (int i = 0; i < palette.Length; i++)
        {
            for (int j = 0; j < palette.Length; j++)
            {
                if (i != j)
                {
                    addEdge(palette[i], palette[j], edges, instanceEdges);
                }
            }
        }


        // Connect variable edges to palette color blue 
        // Can only be colored True or false can't be base 
        for (int i = 0; i < variables.Count; i++)
        {
            addEdge(variables[i], palette[2], edges, instanceEdges);

        }

        // Connect literal to literal negation
        // x1 and !x1 can't have the same color
        for (int i = 0; i < variables.Count; i++)
        {
            for (int j = 0; j < variables.Count; j++)
            {
                if (variables[i].Replace("!", "") == variables[j].Replace("!", "") && variables[i] != variables[j])
                {
                    addEdge(variables[i], variables[j], edges, instanceEdges);
                }
            }
        }

        // Create clause gadget
        // Each clause contains 6 nodes 
        for (int i = 0; i < clauses.Count; i++)
        {
            // Connect   (a V b ) 
            for (int j = 0; j < 3; j++)
            {
                for (int k = 0; k < 3; k++)
                {
                    if (k != j) { addEdge(clauses[i][j], clauses[i][k], edges, instanceEdges); }
                }

            }

            // Connect  ((a V b) V c )
            for (int j = 3; j < clauses[i].Count; j++)
            {
                for (int k = 3; k < clauses[i].Count; k++)
                {
                    if (k != j) { addEdge(clauses[i][j], clauses[i][k], edges, instanceEdges); }
                }
            }


            // Join ((a V b) V c )
            for (int j = 2; j < 4; j++)
            {

                for (int k = 2; k < 4; k++)
                {

                    if (k != j) { addEdge(clauses[i][j], clauses[i][k], edges, instanceEdges); }
                }
            }

        }


        // Combine clause, variable and palette  

        for (int i = 0; i < clauses.Count; i++)
        {


            // Connect variables to clause gadgets 
            addEdge(SAT3Instance.clauses[i][0], clauses[i][0], edges, instanceEdges);
            addEdge(SAT3Instance.clauses[i][1], clauses[i][1], edges, instanceEdges);
            addEdge(SAT3Instance.clauses[i][2], clauses[i][4], edges, instanceEdges);



            addEdge(clauses[i][0], SAT3Instance.clauses[i][0], edges, instanceEdges);
            addEdge(clauses[i][1], SAT3Instance.clauses[i][1], edges, instanceEdges);
            addEdge(clauses[i][4], SAT3Instance.clauses[i][2], edges, instanceEdges);


            // Connect palette base node to (a V b)
            addEdge(clauses[i][2], palette[2], edges, instanceEdges);
            addEdge(palette[2], clauses[i][2], edges, instanceEdges);

            // Connect palette nodes to clauses ((a V b) V c )

            // palette : False  
            addEdge(clauses[i][5], palette[0], edges, instanceEdges);
            addEdge(palette[0], clauses[i][5], edges, instanceEdges);


            //  palette : Base  
            addEdge(clauses[i][5], palette[2], edges, instanceEdges);
            addEdge(palette[2], clauses[i][5], edges, instanceEdges);

        }




        // Set GRAPHCOLORING edges 
        reducedGRAPHCOLORING.edges = edges;

        //Set NodeColoring 
        reducedGRAPHCOLORING.nodeColoring = coloring;


        //The number of colors that satisfy the problem
        reducedGRAPHCOLORING.K = 3;
        reducedGRAPHCOLORING.parseProblem(reducedGRAPHCOLORING.nodes, instanceEdges, reducedGRAPHCOLORING.K.ToString());

        foreach (var value in instanceEdges)
        {
            Console.WriteLine(value);
        }


        Console.WriteLine("This is the size of instance " + instanceEdges.Count + " \n Vs  \n");
        Console.WriteLine(reducedGRAPHCOLORING.edges.Count);

        return reducedGRAPHCOLORING;
    }


    // This method is adding the edges to the list of edges.
    public void addEdge(string x, string y, List<KeyValuePair<string, string>> edges, List<string> instanceEdges)
    {
        KeyValuePair<string, string> fullEdge = new KeyValuePair<string, string>(x, y);
        edges.Add(fullEdge);

        string edge = "{" + x + "," + y + "}";
        string reverseEdge = "{" + y + "," + x + "}";

        if (!instanceEdges.Contains(reverseEdge))
        {
            instanceEdges.Add(edge);
        }
    }

    #endregion















}


