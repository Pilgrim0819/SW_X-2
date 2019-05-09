using System.Threading;

public class TargetLockToken : IToken
{
    // Is it better, to have this token duplicated (with same ID!) and the "own" bool set OR
    // to have the "targetId" attribute, which holds the id of the paired token????

    private const string NAME = "Target Lock";
    public int id;

    static int nextId;

    public bool own { get; set; }
    public int targetId { get; set; }

    public TargetLockToken()
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
