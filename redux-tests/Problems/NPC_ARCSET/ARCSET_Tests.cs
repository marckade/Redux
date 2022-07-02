///ArcsetTests.cs
using Xunit;
using API.Problems.NPComplete.NPC_ARCSET;


namespace redux_tests;

public class ARCSET_Tests
{
    [Fact]
    public void ARCSET_Generic_Add_Test()
    {
        Assert.Equal(2, (1 + 1));
    }


    [Fact]
    /// <summary>
    /// Takes a String and creates a VertexCoverGraph from it
    /// NOTE: DEPRECATED format, ex: {{a,b,c} : {{a,b} & {b,c}} : 1}
    /// </summary>
    /// <param name="graphStr"> string input</param>
    public void ARCSETGraph_Default_Instantiation_Test(){

        string testValue = "";
        ARCSET testingArc = new ARCSET();
        ArcsetGraph testGraph = testingArc.directedGraph;
        Assert.Equal(testingArc.instance, testGraph.ToString()); //Tests that the arcset instance string is equal to its generated graph string
        Assert.Equal(testingArc.defaultInstance, testGraph.ToString()); //Bonus test that 
    }


}