namespace API.Interfaces;

interface ISolver<T> {
    string solverDefinition{get;}
    string source {get;}
    T solverFor{get;}
    
}