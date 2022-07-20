using Xunit;
using API.Interfaces.Graphs;
using API.Problems.NPComplete.NPC_VERTEXCOVER.ReduceTo.NPC_ARCSET;
using API.Problems.NPComplete.NPC_VERTEXCOVER.Verifiers;
using API.Problems.NPComplete.NPC_VERTEXCOVER.Solvers;
using API.Problems.NPComplete.NPC_VERTEXCOVER;
namespace redux_tests;

public class VERTEXCOVER_Tests{


    [Fact]
    public void defaultInstance_Test(){
        VERTEXCOVER vCov = new VERTEXCOVER();
        string defaultInstance = vCov.defaultInstance;
        Assert.Equal("{{a,b,c,d,e,f,g},{{a,b},{a,c},{c,d},{c,e},{d,f},{e,f},{e,g}},3}", defaultInstance);
    }


    [Fact] 

    ///<summary>
    ///This test ensures that the vertexcover solver solves an input instance.
    ///We aren't using a random instance here, we are using a graph with 5 nodes that has a 5-clique
    ///ie. every node is connected to every other node. This ensures that when we run this approximation algorithm we only 
    ///get four nodes in the vertexcover output. Essentially, a property of the VC solver is that given a fully connected graph, it will output a 
    ///node list that is a proper subset of that graph (ie. a subset smaller than the full set). 

    public void VCSolver_Test(){
        string fiveClique = "{{{a,b,c,d,e},{{a,b},{a,c},{a,d},{a,e},{b,c},{b,d},{b,e},{c,e},{c,d}},5}}";
        VERTEXCOVER vCov = new VERTEXCOVER(fiveClique);
        VCSolverJanita vcSolver = vCov.defaultSolver;
        List<string> nodeOutput = vcSolver.Solve(vCov);

        //We know from manually computing this using pen and paper that the above graph will always return a set of four nodes as the solution.
        //Note that we cannot tell exactly which nodes these are, since the solver has built in randomness. 
        Assert.Equal(4, nodeOutput.Count); 
    

    }
    





}