public class Coffin : Stackable
{
    private Urn _urnTemplate;
    
    public override StackableType Type => StackableType.Coffin;
    public Urn UrnTemplate => _urnTemplate;

    public void Init(Urn urnTemplate)
    {
        _urnTemplate = urnTemplate;
    }
}