using UnityEngine;

public class AsteroidProperties : MonoBehaviour
{

    private bool canBeMoved;

    public int id;

    public void setCanBeMoved(bool c)
    {
        this.canBeMoved = c;
    }

    public bool isCanBeMoved()
    {
        return this.canBeMoved;
    }

    public void setId(int id)
    {
        this.id = id;
    }

    public int getId()
    {
        return this.id;
    }
}
