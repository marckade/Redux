namespace API.Interfaces;

interface IVerifier<T> {
    string verifierDefinition{get;}
    string source {get;}
    T verifierFor{get;}
    
}