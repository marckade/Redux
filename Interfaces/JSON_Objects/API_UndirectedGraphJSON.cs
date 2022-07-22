using System.Collections.Generic;
using System.Collections;
using API.Interfaces.Graphs;
namespace API.Interfaces.JSON_Objects;

class API_UndirectedGraphJSON
{

    public List<Node> _nodes;
    public List<API_Link> _links; 

    public API_UndirectedGraphJSON()
    {
        this._nodes = new List<Node>();
        this._nodes.Add(new Node("DEFAULTNODE"));
        this._links = new List<API_Link>();
        this._links.Add(new API_Link());

    }
    public API_UndirectedGraphJSON(List<Node> nodes, List<Edge> inputEdges){
        this._nodes = nodes;
        _links = new List<API_Link>();
        foreach(Edge e in inputEdges){
            API_Link newLink = new API_Link(e.source.name,e.target.name); //destructures an object with a nested node into an object with straight name reference.
            _links.Add(newLink);
        }
        

    }


public List<Node> nodes {
    get {
        return _nodes;
    }
}
public List<API_Link> links {
    get {
        return _links;
    }
}
}