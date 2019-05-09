using System.Threading;

public class StressToken : IToken
{
    private const string NAME = "Stress";
    private int id;

    static int nextId;

    public StressToken()
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
