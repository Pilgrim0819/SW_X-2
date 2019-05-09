using System.Threading;

public class EvadeToken : IToken
{
    private const string NAME = "Evade";
    public int id;

    static int nextId;

    public EvadeToken()
    {
        id = Interlocked.Increment(ref nextId);
    }

    public string getName()
    {
        return NAME;
    }

    public int getId()
    {
        return this.id;
    }
}
