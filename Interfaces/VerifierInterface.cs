namespace API.Interfaces;

interface IVerifier<T> {
    string verifierName{get;}
    string verifierDefinition{get;}
    string source {get;}
    
}