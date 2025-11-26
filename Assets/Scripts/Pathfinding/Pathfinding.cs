using System.Collections.Generic;
using UnityEngine;

namespace Pathfinding
{
    public class Pathfinding : MonoBehaviour

    {
        Grid grid;
        public Transform seeker, target;
        private int _jumpHeight;
        public AmericanEnemy americanEnemy;

        void Awake()
        {
            grid = GetComponent<Grid>();
        }

        void Start()
        {
            _jumpHeight = americanEnemy.jumpForce;
            Debug.Log(_jumpHeight);
        }

        void Update()
        {
            FindPath(seeker.position, target.position);
        }
        
        void FindPath(Vector3 startPos, Vector3 endPos)
        {
            Node startNode = grid.NodeFromWorldPoint(startPos);
            Node targetNode = grid.NodeFromWorldPoint(endPos);
            
            List<Node> openSet = new List<Node>();
            HashSet<Node> closedSet = new HashSet<Node>();
            openSet.Add(startNode);

            while (openSet.Count > 0)
            {
                Node currentNode = openSet[0];
                for (int i = 0; i < openSet.Count; i++)
                {
                    if (openSet[i].fCost < currentNode.fCost ||
                        openSet[i].fCost == currentNode.fCost && openSet[i].hCost < currentNode.hCost)
                    {
                        currentNode = openSet[i];
                    }
                }
                openSet.Remove(currentNode);
                closedSet.Add(currentNode);

                if (currentNode == targetNode)
                {
                    RetracePath(startNode, targetNode);
                    return;
                }

                foreach (Node neighbor in grid.GetNeighbors(currentNode, _jumpHeight))
                {
                    int moveCost = GetDistance(currentNode, neighbor);
                    if (neighbor.gridY > currentNode.gridY)
                    {
                        moveCost += 100;
                    }
                    if (neighbor.isWall || closedSet.Contains(neighbor)) continue;
                    
                    int newMovementConstToNeighbor = currentNode.gCost + moveCost;
                    if (newMovementConstToNeighbor < neighbor.gCost || !openSet.Contains(neighbor))
                    {
                        neighbor.gCost = newMovementConstToNeighbor;
                        neighbor.hCost = GetDistance(neighbor, targetNode);
                        neighbor.parent = currentNode;

                        if (!openSet.Contains(neighbor))
                        {
                            openSet.Add(neighbor);
                        }
                    }
                }
            }
        }

        void RetracePath(Node startNode, Node endNode)
        {
            List<Node> path = new List<Node>();
            Node currentNode = endNode;
            
            while (currentNode != startNode)
            {
                path.Add(currentNode);
                currentNode = currentNode.parent;
            }
            
            path.Reverse();
            
            grid.path = path;
        }

        int GetDistance(Node nodeA, Node nodeB)
        {
            int dstX = Mathf.Abs(nodeA.gridX - nodeB.gridX);
            int dstY = Mathf.Abs(nodeA.gridY - nodeB.gridY);
            if (dstX > dstY)
                return 14 * dstY + 10 * (dstX - dstY);
            return 14 * dstX + 10 * (dstY - dstX);
        }
    }
}