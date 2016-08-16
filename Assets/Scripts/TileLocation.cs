[System.Serializable]
public struct TileLocation
{
    public int x, y;

    public TileLocation(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    public override int GetHashCode()
    {
        return this.x * 100 + this.y;
    }

    public override bool Equals(object obj)
    {
        return base.Equals(obj);
    }

    public static bool operator ==(TileLocation a, TileLocation b)
    {
        return a.x == b.x && a.y == b.y;
    }

    public static bool operator !=(TileLocation a, TileLocation b)
    {
        return !(a == b);
    }
}
