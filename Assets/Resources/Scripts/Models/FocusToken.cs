using System.Threading;

public class FocusToken : IToken
{
    private const string NAME = "Focus";
    public int id;

    static int nextId;

    public FocusToken()
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
