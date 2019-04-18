public class LocalDataWrapper {

    private static Player player;

    public static void setPlayer(Player p)
    {
        player = p;
    }

    public static Player getPlayer()
    {
        return player;
    }
}
