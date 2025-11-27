using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyMovement : MonoBehaviour
{
    [Header("Settings")]
    private float moveSpeed;
    private float jumpForce;
    
    // We only jump if the target is this much higher than us
    public float jumpNodeHeightRequirement;

    [Header("References")]
    // Assign your Grid object here in the Inspector
    public Grid gridManager;
    public AmericanEnemy americanEnemy;
    
    Rigidbody2D rb;
    List<Node> currentPath;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
        // Auto-find grid if not assigned
        if (gridManager == null) 
            gridManager = FindObjectOfType<Grid>();

        moveSpeed = americanEnemy.speed;
        jumpForce = americanEnemy.jumpForce;
    }

    void FixedUpdate()
    {
        // 1. Get the latest path from the Grid
        // (Assuming your Pathfinding script updates 'gridManager.path' every frame)
        currentPath = gridManager.path;

        // 2. Safety Checks
        // We need at least 2 nodes: [0] is current pos, [1] is next step
        if (currentPath == null || currentPath.Count < 2) 
            return;

        // 3. Get the Next Step (Index 1)
        // We ignore Index 0 because that is the node we are currently standing on
        Node nextNode = currentPath[1];

        // 4. Move to Target
        MoveToNode(nextNode);
    }

    void MoveToNode(Node targetNode)
    {
        Vector2 targetPos = targetNode.worldPosition;
        bool test;
        
        float directionX = 0;
        if (transform.position.x < targetPos.x - 0.1f) directionX = 1;
        else if (transform.position.x > targetPos.x + 0.1f) directionX = -1;

        Vector2 velocity = rb.linearVelocity;
        velocity.x = directionX * moveSpeed;
        rb.linearVelocity = velocity;
        
        bool isTargetAbove = targetPos.y > transform.position.y + jumpNodeHeightRequirement;

        if (gridManager.IsUnitGrounded(transform.position))
        {
            test = true;
        }
        else
        {
            test = false;
        }

        if (!gridManager.IsUnitGrounded(targetPos) && test)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0); 
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
        
        if (isTargetAbove)
        {
            if (gridManager.IsUnitGrounded(transform.position))
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0); 
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            }
        }
    }
}