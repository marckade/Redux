using API.Interfaces;
using API.Problems.NPComplete.NPC_GRAPHCOLORING;

namespace API.Problems.NPComplete.NPC_GRAPHCOLORING.Verifiers;

class GenericVerifier : IVerifier
{



    #region Fields
    private string _verifierName = "Generic Verifier";
    private string _verifierDefinition = "This is a verifier for GRAPHCOLORING";
    private string _source = " ";

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


    #endregion 

    #region Constructors
    public GenericVerifier()
    {

    }

    #endregion

    #region Methods

    public void verify()
    {

    }



    /*

      Runs the Depth-first search on the connected graph
      starting from the first node.


    */
    public bool DFS(GRAPHCOLORING g)
    {

        bool[] visited = new bool[g.nodes.Count];
        string node = g.nodes[0];

        //Checks if node has a color in k
        if (g.validColor(g.getNodeColor(node)))
        {

            return checkNeighbors(g, 0, g.getNodeColor(node), visited);
        }
        return false;
    }


    /*
      checkNeighbors (GRAPHCOLORING g, int v, string color,  bool[] visited):
      Takes the graph coloring problem, current vertex index, current vertex color,
      list of visited nodes
      Checks if connected adjacent have a different colors, and if 
      the color is in K
    */
    private bool checkNeighbors(GRAPHCOLORING g, int v, string color, bool[] visited)
    {

        visited[v] = true;

        List<string> list = g.getAdjNodes(g.nodes[v]);

        for (int i = 0; i < list.Count; i++)
        {
            string newColor = g.getNodeColor(list[i].ToLower());

            //  checks if node has been visited, 
            if (!visited[i])
            {

                //checks if node has the same color as an adjacent node,
                //checks if node has a color in k
                if (!(newColor.Equals(color.ToLower())) && (g.validColor(newColor)))
                {

                    return checkNeighbors(g, i, newColor, visited);

                }
                else
                {

                    return false;
                }
            }
        }
        return false;
    }



    private Dictionary<string, string> parseCertificate(string certificate)
    {

        Dictionary<string, string> nodeColoring = new Dictionary<string, string>();
        

        if (certificate.Length != 0)
        {
            string[] nodes = certificate.Split(',');

            foreach (string node in nodes)
            {

                string [] nodeColor  = node.Split(':');
               
                    
                nodeColoring.Add(nodeColor[0], nodeColor[1]);
                
            }

        }

        return nodeColoring;
    }

    #endregion


    // --- Methods Including Constructors ---

}