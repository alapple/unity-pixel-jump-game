using UnityEngine;

public class Node
{
    public bool isWall;
    public Vector3 worldPosition;
    public int gridX;
    public int gridY;

    public int gCost;
    public int hCost;
    public Node parent;

    public Node(bool _isWall, Vector3 _worldPos, int _gridX, int _gridY)
    {
        isWall = _isWall;
        worldPosition = _worldPos;
        gridX = _gridX;
        gridY = _gridY;
    }

    public int fCost
    {
        get { return gCost + hCost; }
    }
}