namespace API.Interfaces;

interface ISolver<T> {
    string solverName{get;}
    string solverDefinition{get;}
    string source {get;}
    
}