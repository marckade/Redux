namespace API.Interfaces;

interface IProblem<T,U> {
    string problemName{get;}

    string formalDefinition{get;}
    string problemDefinition{get;}
    string source {get;}
    string defaultInstance{get;}
    T defaultSolver{get;}
    U defaultVerifier{get;}

    
}
