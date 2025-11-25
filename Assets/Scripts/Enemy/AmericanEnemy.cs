using UnityEngine;
using UnityEngine.AI;

public class AmericanEnemy : MonoBehaviour
{
    public int maxHealth;
    public float speed;
    public float jumpForce;
    private float _currantHealth;
    public float standardAttackDamage;
    public float specialAttackDamage;
    public float specialAttackKnockback;
    public LayerMask groundLayer;
    public Rigidbody2D body;
    public float jumpCooldown;
    private float _nextJumpTime;
    
    public System.Action<float, float> OnHealthChanged;

    private void Awake()
    {
        _currantHealth = maxHealth;
    }

    void Start()
    {
        _nextJumpTime = Time.time;
    }

    private void Update()
    {
       // Jump();
        
    }

    bool IsGrounded() {
        Vector2 position = transform.position;
        Vector2 direction = Vector2.down;
        float distance = 0.5f; 
        Vector2 boxSize = new Vector2(0.13f, 0.5f); 
    	
        RaycastHit2D hit = Physics2D.BoxCast(position, boxSize, 0f, direction, distance, groundLayer);
        
        Color debugColor = hit.collider != null ? Color.green : Color.red;
        Debug.DrawRay(position, direction * distance, debugColor, 0.5f);
        
        return hit.collider != null;   
    }

    bool StandingAtBlock()
    {
        Vector2 position = transform.position;
        Vector2 directionRight = Vector2.right;
        Vector2 directionLeft = Vector2.left;
        float distance = 1f; 
        Vector2 boxSize = new Vector2(1f, 1f); 
    	
        RaycastHit2D hitLeft = Physics2D.BoxCast(position, boxSize, 0f, directionLeft, distance, groundLayer);
        
        Color debugColorLeft = hitLeft.collider != null ? Color.green : Color.red;
        Debug.DrawRay(position, directionLeft * distance, debugColorLeft, 0.5f);
        
        RaycastHit2D hitRight = Physics2D.BoxCast(position, boxSize, 0f, directionRight, distance, groundLayer);
        
        Color debugColorRight = hitRight.collider != null ? Color.green : Color.red;
        Debug.DrawRay(position, directionRight * distance, debugColorRight, 0.5f);

        if ((hitLeft.collider != null || hitRight.collider != null) && IsGrounded())
        {
         return true;   
        }
        return false;
    }

    void Jump()
    {
        if (StandingAtBlock())
        {
           if (Time.time >= _nextJumpTime)
           {
                Debug.LogWarning("jumping");
           //     body.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
                _nextJumpTime = Time.time + jumpCooldown;
           }
        }
    }
    
    public void ChangeHealth(int amount)
    {
        _currantHealth = Mathf.Clamp(_currantHealth + amount, 0, maxHealth);
        // Notify listeners whenever health changes
        OnHealthChanged?.Invoke(_currantHealth, maxHealth);
        if (_currantHealth == 0)
        {
        }
    }
}
