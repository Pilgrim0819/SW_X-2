public class EvadeToken : IToken
{
    private const string NAME = "Evade";
    private int id;

    public int getId()
    {
        return this.id;
    }

    public string getName()
    {
        return NAME;
    }

    public void setId(int id)
    {
        this.id = id;
    }
}
