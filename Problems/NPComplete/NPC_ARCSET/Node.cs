
using System;
using System.Collections.Generic;
namespace API.Problems.NPComplete.NPC_ARCSET;

class Node{

//Fields
private string _name;
private bool _preVisit;
private bool _postVisit;

//Constructors
public Node(){
_name = "DEFAULT";
_preVisit = false;
_postVisit = false;

}
public Node(string nm){
    _name = nm;
    _preVisit = false;
    _postVisit = false;
}

//getters and setters
public string name {
        get {
            return _name;
        }
        set {
            _name = value;
        }
    }
public bool preVisit {
    get{
        return _preVisit;
    }
    set{
        _preVisit = value;
    }
}
public bool postVisit {
    get{
        return _postVisit;
    }
    set{
        _postVisit = value;
    }
}

//May want to change to include visit values.
public override string ToString(){

return _name;
}

}