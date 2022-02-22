using API.Interfaces;
using API.Problems.NPComplete.NPC_GRAPHCOLORING;


namespace API.Problems.NPComplete.NPC_SAT3.ReduceTo.NPC_GRAPHCOLORING;

class KarpReduction : IReduction<SAT3, GRAPHCOLORING>
{

    
    # region Fields
    private string _reductionDefinition = "Karps reduction converts clauses from 3SAT into colored nodes in a graph for which a valid coloring exists.";
    private string _source = "Karp, Richard M. Reducibility among combinatorial problems. Complexity of computer computations. Springer, Boston, MA, 1972. 85-103.";
    private SAT3 _reductionFrom;
    private GRAPHCOLORING _reductionTo;

    # endregion

    # region Properties

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

    # endregion
   
    # region Constructors
     public KarpReduction(SAT3 from)
    {
        _reductionFrom = from;
        _reductionTo = Nreduce();

    }


    # endregion

    # region Methods

    # region (N^3) - Reduction
    //The below code is reducing the SAT3 instance to a GRAPHCOLORING instance.
    public GRAPHCOLORING reduce() {

        // color palette 
        // Red : False, Green : True,  Blue : Base

        string[] palette = { "palette: red", "palette: green", "palette: blue" };

        SAT3 SAT3Instance = _reductionFrom;
        GRAPHCOLORING reducedGRAPHCOLORING = new GRAPHCOLORING();


        // ------- Add nodes -------

        List<string> nodes = new List<string>(palette);

        List<string> variables = SAT3Instance.literals.Distinct().ToList();

        for(int i = 0; i < variables.Count; i++){
            nodes.Add(variables[i]+": null");
        }
       
        // Create clause nodes 
        List<List<string>> clauses = new List<List<string>>();
        for (int i = 0; i < SAT3Instance.clauses.Count; i++)
        {
            int clauseIndex = i + 1;

            List<string> tempClause = new List<string>();

            for (int j = 1; j < 7; j++)
            {
                tempClause.Add("C" + clauseIndex + "N" + j + ": null");
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
    

        // Connect palette edges 
        for (int i = 0; i < palette.Length; i++)
        {
            for (int j = 0; j < palette.Length; j++)
            {
                if (i != j)
                {
                    addEdge(palette[i], palette[j], edges);
                }
            }
        }


        // Connect variable edges to palette color blue 
        // Can only be colored True or false can't be base 
        for (int i = 0; i < variables.Count; i++)
        {
            addEdge(variables[i], palette[2], edges);

        }

        // Connect literal to literal negation
        // x1 and !x1 can't have the same color
        for (int i = 0; i < variables.Count; i++)
        {
            for (int j = 0; j < variables.Count; j++)
            {
                if (variables[i].Replace("!", "") == variables[j].Replace("!", "") && variables[i] != variables[j])
                {
                    addEdge(variables[i], variables[j], edges);
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
                    if (k != j) { addEdge(clauses[i][j], clauses[i][k], edges); }
                }

            }

            // Connect  ((a V b) V c )
            for (int j = 3; j < clauses[i].Count; j++)
            {
                for (int k = 3; k < clauses[i].Count; k++)
                {
                    if (k != j) { addEdge(clauses[i][j], clauses[i][k], edges); }
                }
            }


            // Join ((a V b) V c )
            for (int j = 2; j < 4; j++)
            {

                for (int k = 2; k < 4; k++)
                {

                    if (k != j) { addEdge(clauses[i][j], clauses[i][k], edges); }
                }
            }

        }


        // Combine clause, variable and palette  

         for (int i = 0; i < clauses.Count; i++)
        {

            for (int j = 0; j < clauses[i].Count; j++)
            {
                // Connect variables to clause gadgets 
                addEdge(SAT3Instance.clauses[i][0],clauses[i][0], edges);
                addEdge(SAT3Instance.clauses[i][1],clauses[i][1], edges);
                addEdge(SAT3Instance.clauses[i][2],clauses[i][4], edges);


                
                addEdge(clauses[i][0], SAT3Instance.clauses[i][0], edges);
                addEdge(clauses[i][1], SAT3Instance.clauses[i][1], edges);
                addEdge(clauses[i][4], SAT3Instance.clauses[i][2], edges);


                // Connect color blue to (a V b)
                addEdge(clauses[i][2] , palette[2], edges);
                addEdge(palette[2], clauses[i][2],edges);

                // Connect color blue and red to ((a V b) V c )

                // color : red 
                addEdge(clauses[i][5] , palette[0], edges);
                addEdge(palette[0], clauses[i][5],edges);


                //  color : blue 
                addEdge(clauses[i][5] , palette[2], edges);
                addEdge(palette[2], clauses[i][5],edges);
            }
        }

        // Set GRAPHCOLORING edges 
        reducedGRAPHCOLORING.edges = edges;

        //The number of colors that satisfy the problem
        reducedGRAPHCOLORING.K = 3;

     
       // Instance of GRAPHCOLORING

        return reducedGRAPHCOLORING;
    }


     // This method is adding the edges to the list of edges.
    public void addEdge(string x, string y, List<KeyValuePair<string, string>> edges)
    {
        KeyValuePair<string, string> fullEdge = new KeyValuePair<string, string>(x, y);
        edges.Add(fullEdge);
    }


    # endregion

    # region N Reduction

      // The code below  is reducing the SAT3 problem to a GRAPHCOLORING problem in linear time.
    public GRAPHCOLORING Nreduce(){

         // color palette 
        // Red : False, Green : True,  Blue : Base

        string[] palette = { "palette: red", "palette: green", "palette: blue" };

        SAT3 SAT3Instance = _reductionFrom;
        GRAPHCOLORING reducedGRAPHCOLORING = new GRAPHCOLORING();

        // ------- Add nodes -------

        List<string> nodes = new List<string>(palette);

        List<string> variables = SAT3Instance.literals.Distinct().ToList();
        Dictionary<string, string> map = new Dictionary<string, string>();

        for(int i = 0; i < variables.Count; i++){
            nodes.Add(variables[i]+": null");
            map.Add(variables[i], "null");
        }

        List<string> clauseNodes = new List<string>();


        //Create 6 nodes for each clause 
        for(int i = 0; i < SAT3Instance.clauses.Count; i++) {

            clauseNodes.Add("C" + i  + "N0"  + ": null");
            clauseNodes.Add("C" + i + "N1"  + ": null");
            clauseNodes.Add("C" + i  + "N2"  + ": null");
            clauseNodes.Add("C" + i  + "N3"  + ": null");
            clauseNodes.Add("C" + i  + "N4"  + ": null");
            clauseNodes.Add("C" + i  + "N5"  + ": null");

        }

        nodes.AddRange(clauseNodes);

        //Set GRAPHCOLORING nodes
        reducedGRAPHCOLORING.nodes = nodes;


        // ---- End of nodes -----

        // -------------  Add edges ----------------


        List<KeyValuePair<string, string>> edges = new List<KeyValuePair<string, string>>();

        // Connect palette edges 

        for(int i = 0; i < palette.Length; i++){

            int j  = ( i + 1 ) % palette.Length;

            NaddEdge(palette[i], palette[j], edges);

        }

        // Connect variable edges to palette color blue 
        // Can only be colored True or false can't be base 
        for (int i = 0; i < variables.Count; i++) {

            NaddEdge(variables[i], palette[2], edges);

        }

        // Connect literal to literal negation
        // x1 and !x1 can't have the same color
        for (int i = 0; i < variables.Count; i++){

            string variable = "!" + variables[i];

            if(map.ContainsKey(variable)){
                NaddEdge(variable, variables[i], edges);

            }

        }


        
        // Create clause gadget
        // Each clause contains 6 nodes 
        for (int i = 0; i < SAT3Instance.clauses.Count; i++) {

            int a =  i * 6;
           
            // Connect   (a V b ) 
            NaddEdge(clauseNodes[a], clauseNodes[a + 1], edges);
            NaddEdge(clauseNodes[a], clauseNodes[a + 2], edges);
            NaddEdge(clauseNodes[a + 1], clauseNodes[a + 2], edges);

            // Connect  ((a V b) V c )
            NaddEdge(clauseNodes[a + 3], clauseNodes[a + 4], edges);
            NaddEdge(clauseNodes[a + 3], clauseNodes[a + 5], edges);
            NaddEdge(clauseNodes[a + 4], clauseNodes[a + 5], edges);

            // Join ((a V b) V c )
            NaddEdge(clauseNodes[a + 2], clauseNodes[a + 3], edges);

        }


        for (int i = 0; i < SAT3Instance.clauses.Count; i++) {

            int a =  i + 6;

            // Connect variables to clause gadgets 
            NaddEdge(SAT3Instance.clauses[i][0], clauseNodes[a] , edges);
            NaddEdge(SAT3Instance.clauses[i][1], clauseNodes[a + 1] , edges);
            NaddEdge(SAT3Instance.clauses[i][2], clauseNodes[a + 4] , edges);

            // Connect color blue to (a V b)
            NaddEdge(palette[2], clauseNodes[a + 2],edges);

            // Connect color blue and red to ((a V b) V c )

            // Connect red 
            NaddEdge(palette[0], clauseNodes[a + 5],edges);


            // Connect blue 
            NaddEdge(palette[2], clauseNodes[a + 5],edges);

            }

        // Set GRAPHCOLORING edges 
        reducedGRAPHCOLORING.edges = edges;

        //The number of colors that satisfy the problem
        reducedGRAPHCOLORING.K = 3;
        reducedGRAPHCOLORING.parseProblem();

        return reducedGRAPHCOLORING;
    } 


    private void NaddEdge(string x, string y, List<KeyValuePair<string, string>> edges){

        KeyValuePair<string, string> fullEdge = new KeyValuePair<string, string>(x, y);
        KeyValuePair<string, string> fullEdge1 = new KeyValuePair<string, string>(y, x);
        edges.Add(fullEdge);
        edges.Add(fullEdge1);

    }

    # endregion
   
    # endregion

  

   

  

  







}


