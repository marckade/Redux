
using Xunit;
using API.Problems.NPComplete.NPC_SAT3.ReduceTo.NPC_CLIQUE;
using API.Problems.NPComplete.NPC_SAT3;
using API.Problems.NPComplete.NPC_CLIQUE.Inherited;
using API.Problems.NPComplete.NPC_CLIQUE.Verifiers;
using API.Problems.NPComplete.NPC_CLIQUE.Solvers;

namespace redux_tests;
#pragma warning disable CS1591

public class SAT3_Tests
{

    //Error happining with sipserclique graph functions


    // [Theory]
    // [InlineData("(x1 | !x2 | x3) %26 (!x1 | x3 | x1) %26 (x2 | !x3 | x1)", "{{x1,!x2,x3,!x1,x3_1,x1_1,x2,!x3,x1_2},{{x1,x3_1},{x1,x1_1},{x1,x2},{x1,!x3},{x1,x1_2},{!x2,!x1},{!x2,x3_1},{!x2,x1_1},{!x2,!x3},{!x2,x1_2},{x3,!x1},{x3,x3_1},{x3,x1_1},{x3,x2},{x3,x1_2},{!x1,!x2},{!x1,x3},{!x1,x2},{!x1,!x3},{!x1,x1_2},{x3_1,x1},{x3_1,!x2},{x3_1,x3},{x3_1,x2},{x3_1,!x3},{x3_1,x1_2},{x1_1,x1},{x1_1,!x2},{x1_1,x3},{x1_1,x2},{x1_1,!x3},{x1_1,x1_2},{x2,x1},{x2,x3},{x2,!x1},{x2,x3_1},{x2,x1_1},{!x3,x1},{!x3,!x2},{!x3,!x1},{!x3,x3_1},{!x3,x1_1},{x1_2,x1},{x1_2,!x2},{x1_2,x3},{x1_2,!x1},{x1_2,x3_1},{x1_2,x1_1}},3}", "(x1:True)", "{x1, x1_1, x1_2}")]
    // public void SipserReduction_solution_mapping(string sat3Instance, string cliqueInstance, string solution, string mapping)
    // {
    //     Console.WriteLine("Caleb");
    //     SAT3 testSat3 = new SAT3(sat3Instance);
    //     SipserClique testClique = new SipserClique(cliqueInstance);
    //     SipserReduction reduction = new SipserReduction(testSat3);
    //     string mappedSolution = reduction.mapSolutions(testSat3,testClique,solution);

    //     Assert.Equal(solution, mapping);
    // }

    // [Theory]
    // [InlineData("(x1 | !x2 | x3) %26 (!x1 | x3 | x1) %26 (x2 | !x3 | x1)", "{{x1,!x2,x3,!x1,x3_1,x1_1,x2,!x3,x1_2},{{x1,x3_1},{x1,x1_1},{x1,x2},{x1,!x3},{x1,x1_2},{!x2,!x1},{!x2,x3_1},{!x2,x1_1},{!x2,!x3},{!x2,x1_2},{x3,!x1},{x3,x3_1},{x3,x1_1},{x3,x2},{x3,x1_2},{!x1,!x2},{!x1,x3},{!x1,x2},{!x1,!x3},{!x1,x1_2},{x3_1,x1},{x3_1,!x2},{x3_1,x3},{x3_1,x2},{x3_1,!x3},{x3_1,x1_2},{x1_1,x1},{x1_1,!x2},{x1_1,x3},{x1_1,x2},{x1_1,!x3},{x1_1,x1_2},{x2,x1},{x2,x3},{x2,!x1},{x2,x3_1},{x2,x1_1},{!x3,x1},{!x3,!x2},{!x3,!x1},{!x3,x3_1},{!x3,x1_1},{x1_2,x1},{x1_2,!x2},{x1_2,x3},{x1_2,!x1},{x1_2,x3_1},{x1_2,x1_1}},3}", "(x1:True)", "{x1, x1_1, x1_2}")]
    // public void SipserReduction_reverse_solution_mapping(string sat3Instance, string cliqueInstance, string solution, string mapping)
    // {
    //     SAT3 testSat3 = new SAT3(sat3Instance);
    //     SipserClique testClique = new SipserClique(cliqueInstance);
    //     SipserReduction reduction = new SipserReduction(testSat3);
    //     string mappedSolution = reduction.reverseMapSolutions(testSat3,testClique,mapping);

    //     Assert.Equal(mapping, solution);
    // }
  

}