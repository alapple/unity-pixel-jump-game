using UnityEngine;
using System.Collections.Generic;

public class Grid : MonoBehaviour
{
    public LayerMask unwalkableMask;
    public Vector2 gridWorldSize;
    public float nodeRadius; 
    Node[,] grid;
    public List<Node> path;

    float nodeDiameter;
    int gridSizeX, gridSizeY;
    
    [Header("Enemy Settings")]
    public int maxSaveFallDistance;
    public int maxJumpDistance;
    
    [Header(("Debug Settings"))]
    public bool displayGridGizmos = true;
    public bool displayCircleGizmos = true;
    public bool displayPathGizmos = true;

    void Awake()
    {
        nodeDiameter = nodeRadius * 2;
        gridSizeX = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter);
        gridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter);
        CreateGrid();
    }

    void CreateGrid()
    {
        grid = new Node[gridSizeX, gridSizeY];

        Vector3 worldBottomLeft = transform.position;

        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                // Calculate center of each node starting from the anchor
                Vector3 worldPoint = worldBottomLeft 
                                     + Vector3.right * (x * nodeDiameter + nodeRadius) 
                                     + Vector3.up * (y * nodeDiameter + nodeRadius);
                                     
                bool wall = Physics2D.OverlapCircle(worldPoint, nodeRadius - 0.1f, unwalkableMask);
                grid[x, y] = new Node(wall, worldPoint, x, y);
            }
        }
    }

    public Node NodeFromWorldPoint(Vector3 worldPosition)
    {
        // FIX: Math is now relative to the transform.position (Anchor)
        float percentX = (worldPosition.x - transform.position.x) / gridWorldSize.x;
        float percentY = (worldPosition.y - transform.position.y) / gridWorldSize.y;
        
        percentX = Mathf.Clamp01(percentX);
        percentY = Mathf.Clamp01(percentY);

        int x = Mathf.RoundToInt((gridSizeX - 1) * percentX);
        int y = Mathf.RoundToInt((gridSizeY - 1) * percentY);
        return grid[x, y];
    }
    public List<Node> GetNeighbors(Node node, int maxJumpHeight)
    {
        List<Node> neighbors = new List<Node>();
        bool isGrounded = IsUnitGrounded(node.worldPosition);

        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                if (x == 0 && y == 0) continue;

                int checkX = node.gridX + x;
                int checkY = node.gridY + y;

                if (checkX >= 0 && checkX < gridSizeX && checkY >= 0 && checkY < gridSizeY)
                {
                    Node neighbor = grid[checkX, checkY];

                    if (neighbor.isWall) continue;
                    
                    if (y > 0 && !isGrounded) continue;
                    
                    bool isNeighborSafe = IsNodeSafe(neighbor);

                    if (isNeighborSafe)
                    {
                        neighbors.Add(neighbor);
                    }
                    else
                    {
                        if (isGrounded && y == 0) 
                            continue;

                        if (x != 0 && IsLandAhead(neighbor, x))
                        {
                            neighbors.Add(neighbor);
                        }
                    }
                }
            }
        }
        return neighbors;
    }

    bool IsNodeSafe(Node node)
    {
        if (node.isWall) return true;

        for (int i = 1; i < maxSaveFallDistance; i++)
        {
            int checkY = node.gridY - i;

            if (checkY < 0) return false;

            if (grid[node.gridX, checkY].isWall) return true;
        }

        return false;
    }

    bool IsLandAhead(Node node, int x)
    {
        for (int i = 1; i <= maxJumpDistance; i++)
        {
            int targetX = node.gridX + (x * i);
            int targetY = node.gridY;

            int targetLower = node.gridY - 1;
            int targetUpper = node.gridY + 1;

            if (targetX >= 0 && targetX < gridSizeX)
            {
                if (grid[targetX, targetY].isWall) return true;
                if (targetLower >= 0 && grid[targetX, targetLower].isWall) return true;
                if (targetUpper < gridSizeY && grid[targetX, targetUpper].isWall) return true;
            }
        }
        return false;
    }
    
    public bool IsUnitGrounded(Vector3 unitWorldPosition)
    {
        Node currentNode = NodeFromWorldPoint(unitWorldPosition);
        
        int checkX = currentNode.gridX;
        int checkY = currentNode.gridY - 1;

        if (checkY < 0) return false;

        if (grid[checkX, checkY].isWall)
        {
            return true;
        }
        return false;
    }

    void OnDrawGizmos()
    {
        // Draw the outline starting from the anchor
        Gizmos.color = Color.yellow;
        // The center of the drawn cube needs to be offset by half size to match the anchor logic
        Vector3 center = transform.position + new Vector3(gridWorldSize.x / 2, gridWorldSize.y / 2, 0);
        Gizmos.DrawWireCube(center, new Vector3(gridWorldSize.x, gridWorldSize.y, 1));

        if (grid != null && displayGridGizmos)
        {
            foreach (Node n in grid)
            {
                Gizmos.color = (n.isWall) ? new Color(1,0,0,0.5f) : new Color(1,1,1,0.5f);
                Gizmos.DrawCube(n.worldPosition, Vector3.one * (nodeDiameter - .1f));

                if (displayCircleGizmos)
                {
                    Gizmos.color = Color.cyan;
                    Gizmos.DrawWireSphere(n.worldPosition, nodeRadius - 0.1f);
                }
            }
        }

        if (path != null && displayPathGizmos)
        {
            Gizmos.color = Color.green;
            for (int i = 0; i < path.Count - 1; i++)
            {
                Node currentNode = path[i];
                Node nextNode = path[i + 1];
                
                Gizmos.DrawLine(currentNode.worldPosition, nextNode.worldPosition);
                
                Gizmos.DrawSphere(currentNode.worldPosition, nodeRadius - 0.1f);
            }
        }
    }
}