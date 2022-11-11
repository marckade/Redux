using API.Interfaces;
using API.Problems.NPComplete.NPC_CLIQUE;
using API.Problems.NPComplete.NPC_CLIQUE.Inherited;

namespace API.Problems.NPComplete.NPC_SAT3.ReduceTo.NPC_CLIQUE;

class SipserReduction : IReduction<SAT3, SipserClique>
{

    // --- Fields ---
    private string _reductionDefinition = "Sipsers reduction converts clauses from 3SAT into clusters of nodes in a graph for which CLIQUES exist";
    private string _source = "Sipser, Michael. Introduction to the Theory of Computation.ACM Sigact News 27.1 (1996): 27-29.";
    private string[] _contributers = { "Kaden Marchetti", "Alex Diviney" };
    private Dictionary<Object,Object> _gadgetMap = new Dictionary<Object,Object>();

    private SAT3 _reductionFrom;
    private SipserClique _reductionTo;


    // --- Properties ---
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
    public string[] contributers
    {
        get
        {
            return _contributers;
        }
    }
    public Dictionary<Object,Object> gadgetMap {
        get{
            return _gadgetMap;
        }
        set{
            _gadgetMap = value;
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
    public SipserClique reductionTo
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

    // --- Methods Including Constructors ---
    public SipserReduction(SAT3 from)
    {
        _reductionFrom = from;
        _reductionTo = reduce();

    }
    public SipserClique reduce()
    {
        SAT3 SAT3Instance = _reductionFrom;

        //number literals of sat before reduction
        List<List<String>> newClauses = new List<List<string>>();
        foreach(var clause in SAT3Instance.clauses){
            List<String> temp = new List<String>();
            foreach(var element in clause){
                temp.Add(element);
            }
            newClauses.Add(temp);
        }
        for (int i = 0; i < SAT3Instance.clauses.Count; i++){
            for (int j = 0; j < SAT3Instance.clauses[i].Count; j++){
                int count = 0;
                for( int k = 0; k<i; k++){
                    foreach(var element in SAT3Instance.clauses[k]){
                        if(element == SAT3Instance.clauses[i][j]){
                            count ++;
                        }
                    }
                }
                for( int k = 0; k<j; k++){
                    if(SAT3Instance.clauses[i][j] == SAT3Instance.clauses[i][k]){
                        count ++;
                    }
                }
                if(count >0){
                    newClauses[i][j] = SAT3Instance.clauses[i][j] + "_" +count;
                }
                else{
                    newClauses[i][j] = SAT3Instance.clauses[i][j];
                }
            }
        }
        SAT3Instance.clauses = newClauses;
        SipserClique reducedCLIQUE = new SipserClique();
        // SAT3 literals become nodes.
        reducedCLIQUE.nodes = SAT3Instance.literals;
        List<KeyValuePair<string, string>> edges = new List<KeyValuePair<string, string>>();
        List<string> usedNames = new List<string>(); // Used to track what names have been used for nodes

        // define what makes the edges. Not in same cluster & not inverse

        // I is the cluster
        for (int i = 0; i < SAT3Instance.clauses.Count; i++)
        {
            reducedCLIQUE.numberOfClusters = SAT3Instance.clauses.Count;
            for (int j = 0; j < SAT3Instance.clauses[i].Count; j++)
            {
                string nodeFrom = SAT3Instance.clauses[i][j];
                // nodeFrom = duplicateName(nodeFrom, usedNames, 1, nodeFrom);

                SipserNode newNode = new SipserNode(nodeFrom, i.ToString());
                reducedCLIQUE.clusterNodes.Add(newNode);
                // usedNames.Add(nodeFrom);
                //Four loops? Sounds efficent
                for (int a = 0; a < SAT3Instance.clauses.Count; a++)
                {

                    for (int b = 0; b < SAT3Instance.clauses[a].Count; b++)
                    {
                        
                        string nodeTo = SAT3Instance.clauses[a][b];
                        bool inverse = false;
                        bool samecluser = false;

                        // Check if nodes are inverse of one another
                        if (nodeFrom != nodeTo && nodeFrom.Replace("!", "") == nodeTo.Replace("!", ""))
                        {
                            inverse = true;
                        }
                        // Check if nodes belong to same cluster
                        if (i == a)
                        {
                            samecluser = true;
                        }

                        if (!inverse && !samecluser && nodeFrom != nodeTo)
                        {
                            KeyValuePair<string, string> fullEdge = new KeyValuePair<string, string>(nodeFrom, nodeTo);
                            // Console.WriteLine("i:{0} a:{1} edge:{2}",i,a,fullEdge);
                            if(i == 0 && a ==1 && j == 0 && b == 1){
                                foreach(var name in usedNames){
                                    Console.WriteLine(name);
                                }
                            }
                            edges.Add(fullEdge);
                        }
                    }
                }
            }
        }
        reducedCLIQUE.edges = edges;
        reducedCLIQUE.K = SAT3Instance.clauses.Count;

        // --- Generate G string for new CLIQUE ---
        string nodesString = "";
        string literalName = String.Empty;
        List<string> usedNamesLiterals = new List<string>();
        foreach (string literal in SAT3Instance.literals)
        {
            literalName = duplicateName(literal, usedNamesLiterals, 1, literal);
            nodesString += literalName + ",";
            usedNamesLiterals.Add(literalName);
        }

        string edgesString = "";
        foreach (KeyValuePair<string, string> edge in edges)
        {
            edgesString += "(" + edge.Key + "," + edge.Value + ")" + " & ";
        }
        edgesString = edgesString.Trim(' ').TrimEnd('&');

        int kint = SAT3Instance.clauses.Count;
        // "{{1,2,3,4} : {(4,1) & (1,2) & (4,3) & (3,2) & (2,4)} : 1}";
        string G = "{{" + nodesString + "} : {" + edgesString + "} : " + kint.ToString() + "}";

        // Assign and return
        //Console.WriteLine(G);
        reducedCLIQUE.cliqueAsGraph = new CliqueGraph(G); //ALEX NOTE: Since undirected graphs are backwards compatible, I am able to take in an old format string here. This is a bandaid solution
        reducedCLIQUE.instance = reducedCLIQUE.cliqueAsGraph.formalString(); //Outputs a standard graph notation instance.
        reductionTo = reducedCLIQUE;
        return reducedCLIQUE;
    }

    private string duplicateName(string name, List<string> usedNames, int version, string originalName)
    {
        if (usedNames.Contains(name))
        {
            // usedNames.Add(name);
            string newName = originalName + '_' + version;
            version = version + 1;
            return duplicateName(newName, usedNames, version, originalName);
        }

        return name;
    }

    /// <summary>
    ///  Given a solution string and a reduced to problem instance, map the solution to the problem. 
    /// </summary>
    /// <param name="problemInstance"></param>
    /// <param name="sat3SolutionString"></param>
    /// <returns> A Sipser Clique with a cluster nodes attribute (list of SipserNodes) that has a solution state mapped to each node.</returns>
    public SipserClique solutionMappedToClusterNodes(SipserClique sipserInput, Dictionary<string, bool> solutionDict)
    {

        SipserClique sipserClique = sipserInput;
        //Console.WriteLine("TEST CLIQUE INTERNAL");
        List<SipserNode> clusterNodes = sipserClique.clusterNodes;
        int numberOfClusters = sipserClique.numberOfClusters; //Not guaranteed to not be null, this value is set by the reduction that should have happened to sipserInput previously.
        foreach (SipserNode s in clusterNodes) 
        //This loop will set all nodes matching the format true. This means that we can have multiple nodes in a single "clause" true, but we need to 
        // instead arbitrarily pick one per clause (per cluster). A cleanup loop will follow and choose one candidate per cluster.
        {
            bool result = false;
            if (solutionDict.ContainsKey(s.name))
            { //if the name of the node is in the dictioary
                if (solutionDict.TryGetValue(s.name, out result))
                { //given key, get the value of the node kvp 
                    s.solutionState = result.ToString(); //We know this is true.
                    List<string> searchList = getclusterNodeSearchList(s.name, numberOfClusters); //Get the list of equivalent names to turn true;

                    foreach (string equivalentNode in searchList)
                    { //For each equivalent name (name that has the same start but has a _1, _2 etc suffix)
                      //Console.Write(equivalentNode);
                        foreach (SipserNode sInner in clusterNodes) //Look for any node in clusterNode that has shares the prefix.
                        { 
                            //Console.WriteLine(" S inner: " + sInner + " equivalentNode: " + equivalentNode);
                            if (sInner.name.Equals(equivalentNode)) //if it has one of the prefix's
                                sInner.solutionState = true.ToString(); //We already know its true. 
                        }
                    }
                }
            }

        }

        //Cleanup loops. First we sort the nodes into sublists by cluster. 
        List<SipserNode>[] nodeListSortedByCluster = new List<SipserNode>[numberOfClusters];
        for (int i = 0; i < numberOfClusters;i++){
            nodeListSortedByCluster[i] = new List<SipserNode>();
        }
        foreach (SipserNode s in clusterNodes){
            
            int clusterNum = Int32.Parse(s.cluster); //gets the number
            nodeListSortedByCluster[clusterNum].Add(s); //array position is mapped to cluster. Every node in this sublist has the same cluster.
        }
        //then we look through sublists and unset duplicates.
        foreach(List<SipserNode> subList in nodeListSortedByCluster){
            bool foundTrueFlag = false;
            foreach(SipserNode cNode in subList){
                if(cNode.solutionState.Equals("True") && !foundTrueFlag){
                    foundTrueFlag = true; //This is the first true node found, dont edit it.
                }
                else if(cNode.solutionState.Equals("True") && foundTrueFlag){
                    cNode.solutionState = ""; //This is another true node in a cluster so we will set it back to false. 
                }
            }
        }

        return sipserClique;

    }

    /// <summary>
    ///  This maps a name prefix, ie. x1, to the possible clusters that it could appear in, ie. [x1_1, x1_2] and returns that list
    /// </summary>
    /// <param name="primaryName"></param>
    /// <param name="amountOfClusters"></param>
    /// <returns> A list of possible names</returns>
    private List<string> getclusterNodeSearchList(string primaryName, int amountOfClusters)
    {
        List<string> searchList = new List<string>();
        searchList.Add(primaryName);
        for (int i = 1; i < amountOfClusters; i++)
        {
            searchList.Add(primaryName + "_" + i);
            //Console.WriteLine(primaryName + "_" + i);
        }
        return searchList;

    }

}
// return an instance of what you are reducing to