using API.Interfaces;

namespace API.Problems.NPComplete.NPC_CLIQUE;
public class CLIQUEGadget : IGadget
{

    private string _reductionType;

    private string _problemType;

    private string _gadgetString;


    public CLIQUEGadget(string reductionType, string gadgetString){
        _reductionType = reductionType;
        _gadgetString = gadgetString;
        _problemType = "CLIQUE";

    }


    public string reductionType
    {
        get
        {
            return _reductionType;
        }
        set
        {
            _reductionType = value;
        }
    }

    public string problemType
    {
        get
        {
            return _problemType;
        }
        set
        {
            _problemType = value;
        }
    }

    public string gadgetString
    {
        get
        {
            return _gadgetString;
        }
        set
        {
            _gadgetString = value;
        }
    }

    override public string? ToString(){

        return _gadgetString;
    }

}