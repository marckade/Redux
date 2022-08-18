
using Xunit;
using API.Problems.NPComplete.NPC_GRAPHCOLORING;


namespace redux_tests;
    public class GRAPHCOLORING_Tests {


    [Fact]
    public void defaultInstance_Test()
    {
        GRAPHCOLORING gCov = new GRAPHCOLORING();
        string defaultInstance = gCov.defaultInstance;
        Assert.Equal("{a,b,c,d,e,f,g,h,i},{{a,b},{b,c},{a,c},{d,a},{a,e},{e,d},{a,f},{f,g},{g,a},{h,a},{h,i},{i,a}},3}", defaultInstance);
    }







    }