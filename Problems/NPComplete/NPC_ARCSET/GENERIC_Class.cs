// using API.Interfaces;
// using API.Problems.NPComplete.NPC_SAT3.Solvers;
// using API.Problems.NPComplete.NPC_SAT3.Verifiers;

// namespace API.Problems.NPComplete.NPC_SAT3;

// class SAT3 : IProblem<GenericSolver,GenericVerifier>{

//     // --- Fields ---
//     private string _problemName = "3SAT";
//     private string _formalDefinition = "PHI | PHI is a satisfiabile Boolean forumla in 3CNF";
//     private string _problemDefinition = "3SAT, or the Boolean satisfiability problem, is a problem that asks for a given formula in Boolean algebra (with unknown number of variables) whether it is satisfiable, that is, whether there is some combination of the (binary) values of the variables that will give 1";

//     // How we want format
//     private string _source = "Karp, Richard M. Reducibility among combinatorial problems. Complexity of computer computations. Springer, Boston, MA, 1972. 85-103.";
//     private string _defaultInstance = "(x1,x2*,x3) ^ (x1*,x3,x1) ^ (x2,x3*,x1)";
//     private string _phi = string.Empty;
//     private GenericSolver _defaultSolver = new GenericSolver();
//     private GenericVerifier _defaultVerifier = new GenericVerifier();

//     // --- Properties ---
//     public string problemName {
//         get {
//             return _problemName;
//         }
//     }
//     public string formalDefinition {
//         get {
//             return _formalDefinition;
//         }
//     }
//     public string problemDefinition {
//         get {
//             return _problemDefinition;
//         }
//     }

//     public string source {
//         get {
//             return _source;
//         }
//     }
//     public string defaultInstance {
//         get {
//             return _defaultInstance;
//         }
//     }
//     public string phi {
//         get {
//             return _phi;
//         }
//         set {
//             _phi = value;
//         }
//     }
//     public GenericSolver defaultSolver {
//         get {
//             return _defaultSolver;
//         }
//     }
//     public GenericVerifier defaultVerifier {
//         get {
//             return _defaultVerifier;
//         }
//     }


//     // --- Methods Including Constructors ---
//     public SAT3() {
//         _phi = defaultInstance;
//     }
//     public SAT3(string phiInput) {
//         _phi = phiInput;
//     }

//     public void ParseProblem(string phiInput) {

//     }
// }