public class TargetLockToken : IToken
{
    // Is it better, to have this token duplicated (with same ID!) and the "own" bool set OR
    // to have the "targetId" attribute, which holds the id of the paired token????

    private const string NAME = "Target Lock";
    private int id;

    private bool own;
    private int targetId;

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

    public void setOwn(bool own)
    {
        this.own = own;
    }

    public bool isOwn()
    {
        return this.own;
    }

    public void setTargetId(int id)
    {
        this.targetId = id;
    }

    public int getTargetId()
    {
        return this.targetId;
    }
}
