using API.Interfaces;
using API.Problems.NPComplete.NPC_GRAPHCOLORING;

namespace API.Problems.NPComplete.NPC_GRAPHCOLORING.Verifiers;

class IgbokweVerifier : IVerifier
{



    #region Fields
    private string _verifierName = "Igbokwe's Verifier";
    private string _verifierDefinition = "This is a verifier for Graph Coloring made by Daniel Igbokwe. it checks if adjacent vertices are labeled differently, and that their labels are valid using a depth first search";
    private string _source = "Daniel Igbokwe";
    private string _complexity = " O(V + E)";
    private string _certificate = " { ( a : 0, b : 1, c : 2, d : 1, e : 2, f : 1, g : 2, h : 1, i : 2 ) :3 }";

    private Dictionary<string, string> _coloring = new Dictionary<string, string>();
    private int _k = 3;

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

    public Dictionary<string, string> coloring{
        get{
            return _coloring;
        }

        set{
            _coloring = value;
        }
    }

    public int k{
        get{
            return _k;
        }

        set{
            _k  = value;
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

         parseCertificate(userInput);

         // check if each dictionary node is a node in the problem.

         problem.nodeColoring = _coloring;

        string node =  problem.nodes[0];
        string color = problem.getNodeColor(node);

        if(problem.validColor(color)){

            verified = DFS(problem, node);

            int count =  getChromaticNumber(_coloring);
            if(_k < count){
                verified = false;
            }

        }

        return verified;
}


  private int getChromaticNumber(Dictionary<string, string> nodeColoring){

        int colors = 0;
        SortedSet<int> colorList = new SortedSet<int>();
        

        foreach(var elem in nodeColoring){
            colorList.Add(Int32.Parse(elem.Value));
        }

        colors = colorList.Count();

        return colors;
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


    private void  parseCertificate(string certificate) {



       // string parseCertificate = certificate.Replace("(", "").Replace(")","");
       string parseCertificate = certificate.Trim().Replace("{", "").Replace("}","").Replace(" ","");
       string [] splitCertificate = parseCertificate.Split("):");
       string dictionary = splitCertificate[0].Replace("(", "").Replace(")","");
       int k = Int32.Parse(splitCertificate[1]);

        Dictionary<string, string> nodeColoring = new Dictionary<string, string>();
        

        if (parseCertificate.Length != 0)
        {
            // string[] nodes = parseCertificate.Split(',');
            string[] nodes = dictionary.Split(',');

            foreach (string node in nodes)
            { 
                string [] nodeColor  = node.Split(':');
               

                string key = nodeColor[0].ToLower().Trim();
                string val = nodeColor[1].ToLower().Trim();


                 // check if dictionary contains key first
                if(!nodeColoring.ContainsKey(key)){
                     nodeColoring.Add(key, val);
                }
                
            }

        }

        _coloring =  nodeColoring;
        _k =  k;
    }



    

    #endregion
    // --- Methods Including Constructors ---

}