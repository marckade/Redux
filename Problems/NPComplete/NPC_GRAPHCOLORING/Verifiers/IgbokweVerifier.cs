using API.Interfaces;
using API.Problems.NPComplete.NPC_GRAPHCOLORING;

namespace API.Problems.NPComplete.NPC_GRAPHCOLORING.Verifiers;

class IgbokweVerifier : IVerifier
{



    #region Fields
    private string _verifierName = "Igbokwe's Verifier";
    private string _verifierDefinition = "This is a verifier for GRAPHCOLORING made by Daniel Igbokwe. it checks if adjacent vertices are labeled differently, and that their labels are valid using a depth first search";
    private string _source = "";
    private string _complexity = " O(V + E)";
    private string _certificate ="";

    #endregion

    #region Properties
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

    public string complexity {
        get {
            return _complexity;
        }

        set{
            _complexity = value;
        }
    }
     public string certificate {
        get {
            return _certificate;
        }
    }


    #endregion 

#region Constructors
    public IgbokweVerifier() {

}
    #endregion


#region Methods
    public Boolean verify(GRAPHCOLORING problem, string userInput){
        Boolean verified = false;

        // Parse Certificate 

        problem.nodeColoring = parseCertificate(userInput);

        string node =  problem.nodes[0];
        string color = problem.getNodeColor(node);

        if(problem.validColor(color)){

            verified = DFS(problem, node);

        }

        return verified;
}


    private Boolean DFS(GRAPHCOLORING problem, string source){

        Stack<string> stack = new Stack<string>();
        HashSet<string> visited = new HashSet<string>();

    stack.Push(source);

    while(stack.Count > 0){

        string currentNode =  stack.Pop();
        string color = problem.getNodeColor(currentNode);



        if(!visited.Contains(currentNode)){ 
            visited.Add(currentNode);

        }


        foreach(string node in problem.getAdjNodes(currentNode)){

            if(!visited.Contains(node)){
                string newColor  = problem.getNodeColor(node);

              
                if(!newColor.Equals(color) && problem.validColor(newColor)){
                 
                    stack.Push(node);

                }else{

                    return false;
                }

               
            }

        }

    } 

    if(visited.Count == problem.nodes.Count){
        return true;
    }


    return false;
}


    private Dictionary<string, string> parseCertificate(string certificate)
    {

        string parseCertificate = certificate.Replace("(", "").Replace(")","");

        Dictionary<string, string> nodeColoring = new Dictionary<string, string>();
        

        if (parseCertificate.Length != 0)
        {
            string[] nodes = parseCertificate.Split(',');

            foreach (string node in nodes)
            { 
                string [] nodeColor  = node.Split(':');
                nodeColoring.Add(nodeColor[0].ToLower().Trim(), nodeColor[1].ToLower().Trim());

            }

        }

        return nodeColoring;
    }



    

    #endregion
    // --- Methods Including Constructors ---

}