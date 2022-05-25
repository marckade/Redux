using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using API.Interfaces.Graphs;
namespace API.Problems.NPComplete.NPC_VERTEXCOVER;

class VertexCoverGraph:UndirectedGraph{

    public VertexCoverGraph(string vertInput) : base (vertInput){
        
    }

    public VertexCoverGraph(string vertInput, bool decoy) : base (vertInput, decoy){
    
    }
}