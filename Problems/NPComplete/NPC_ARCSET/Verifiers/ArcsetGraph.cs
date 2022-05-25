using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using API.Interfaces.Graphs;
namespace API.Problems.NPComplete.NPC_ARCSET;

class ArcsetGraph:DirectedGraph{

    public ArcsetGraph(string arcInput) : base (arcInput){
        
    }

    public ArcsetGraph(string arcInput, bool decoy) : base (arcInput, decoy){
    
    }




}