//This API link is needed for object destructuring by d3. Normally graphs in Redux have link attributes that point to node objects.
//However, in serializing links (edges) for the use of d3, we need to point links to purely the names of the nodes, not the nodes themselves
//Author: Alex Diviney

namespace API.Interfaces.JSON_Objects.Graphs;

class API_Link{
    private string _source;
    private string _target;
    public API_Link(){
        this._source = "DEFAULTSOURCE";
        this._target = "DEFAULTTARGET";
    }

    public API_Link(string s, string t){
        _source = s;
        _target = t;
    }
    
    public string source{
        get{
            return _source;
        }
    }
    public string target{
        get{
            return _target;
        }
    }
    
}