using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyMovement : MonoBehaviour
{
    [Header("Settings")]
    private float moveSpeed;
    private float jumpForce;
    public float jumpNodeHeightRequirement;

    [Header("References")]
    public Grid gridManager;
    public AmericanEnemy americanEnemy;
    
    [Header("Combat Settings")]
    public float stoppingDistance;
    public Transform playerTransform;
    
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
        // 0. Safety Check
        if (playerTransform == null) return;

        // 1. DISTANCE CHECK
        // Calculate direct distance to player
        float distToPlayer = Vector2.Distance(transform.position, playerTransform.position);

        // If we are close enough, STOP MOVING and return.
        if (distToPlayer < stoppingDistance)
        {
            // Stop horizontal movement, but keep gravity (y)
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
            return; 
        }
        currentPath = gridManager.path;
        if (currentPath == null || currentPath.Count < 2) return;

        Node nextNode = currentPath[1];

        // CHECK: Is this a walking connection or a jumping connection?
        float distanceToNext = Vector2.Distance(transform.position, nextNode.worldPosition);

        // If the next node is far away (> 2.0f), it's a PARABOLIC JUMP
        if (distanceToNext > 2.0f)
        {
            if (gridManager.IsUnitGrounded(transform.position))
            {
                // STOP walking current velocity
                rb.linearVelocity = Vector2.zero; 

                // CALCULATE exact velocity to hit the target
                // We use a helper function to solve the ballistic arc
                Vector2 jumpVelocity = CalculateJumpVelocity(transform.position, nextNode.worldPosition, 1.0f); // 1.0f = time to hit target
                
                rb.AddForce(jumpVelocity, ForceMode2D.Impulse);
                
                // Hack: Wait a bit so we don't try to jump again immediately
                // (In a real game, use a State Machine: Idle -> Pathing -> Jumping -> Landing)
            }
        }
        else
        {
            // NORMAL WALKING
            MoveToNode(nextNode);
        }
    }

    // MATH WIZARDRY: Calculates force needed to hit a target point
    Vector2 CalculateJumpVelocity(Vector2 start, Vector2 target, float time)
    {
        Vector2 distance = target - start;
        Vector2 distanceXZ = distance; 
        distanceXZ.y = 0;

        float Sy = distance.y;
        float Sxz = distanceXZ.magnitude;

        float Vxz = Sxz / time;
        float Vy = Sy / time + 0.5f * Mathf.Abs(Physics2D.gravity.y * rb.gravityScale) * time;

        Vector2 result = distanceXZ.normalized;
        result *= Vxz;
        result.y = Vy;

        return result;
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
    
    Vector2 CalculateBallisticVelocity(Vector2 start, Vector2 target, float time)
    {
        Vector2 distance = target - start;
        Vector2 distanceXZ = distance; 
        distanceXZ.y = 0;

        float Sy = distance.y;
        float Sxz = distanceXZ.magnitude;

        float Vxz = Sxz / time;
        float Vy = Sy / time + 0.5f * Mathf.Abs(Physics2D.gravity.y) * time;

        Vector2 result = distanceXZ.normalized;
        result *= Vxz;
        result.y = Vy;

        return result;
    }
}