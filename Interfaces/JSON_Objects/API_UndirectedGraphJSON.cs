using System.Collections.Generic;
using System.Collections;
using API.Interfaces.Graphs;

namespace API.Interfaces.JSON_Objects.API_UndirectedGraphJSON;

class API_UndirectedGraphJSON
{

    public List<Node> _nodes;
    public List<Edge> _links; 

    public API_UndirectedGraphJSON()
    {
        this._nodes = new List<Node>();
        this._nodes.Add(new Node("DEFAULTNODE"));
        this._links = new List<Edge>();
        this._links.Add(new Edge());

    }
    public API_UndirectedGraphJSON(List<Node> nodes, List<Edge> edges){
        this._nodes = nodes;
        this._links = edges;
    }


public List<Node> nodes {
    get {
        return _nodes;
    }
}
public List<Edge> links {
    get {
        return _links;
    }
}
}