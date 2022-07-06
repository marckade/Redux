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

    





}