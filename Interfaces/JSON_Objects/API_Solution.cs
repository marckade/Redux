using System.Collections.Generic;
using System.Collections;

namespace API.Interfaces.JSON_Objects.API_Solution;

class API_Solution
{

    public string _stringInstance;
    public ArrayList _apiInstance; 

    public API_Solution()
    {
        this._stringInstance = "Default";
        this._apiInstance = new ArrayList();
        this._apiInstance.Add("Default");

    }
    public API_Solution(string strInstance,ArrayList apiData){
        this._stringInstance = stringInstance;
        this._apiInstance = apiData;
    }


public string stringInstance {
    get {
        return _stringInstance;
    }
}
public ArrayList apiInstance {
    get {
        return _apiInstance;
    }
}
}