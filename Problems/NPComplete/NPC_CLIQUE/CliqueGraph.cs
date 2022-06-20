using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using API.Interfaces.Graphs;
namespace API.Problems.NPComplete.NPC_CLIQUE;

class CliqueGraph : UndirectedGraph
{

  public CliqueGraph(string cliqueInput) : base (cliqueInput){
        
    }

    public CliqueGraph(string cliqueInput, bool decoy) : base (cliqueInput, decoy){
    

    }


}