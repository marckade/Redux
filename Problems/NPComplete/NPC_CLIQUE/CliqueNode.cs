using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using API.Interfaces.Graphs;
namespace API.Problems.NPComplete.NPC_CLIQUE;

class CliqueNode : Node
{
string _clique;
    public CliqueNode(string name, string clique){
        this._name = name;
        this._clique = clique;

    }

public string clique{
    get{
            return _clique;
        }
}
}