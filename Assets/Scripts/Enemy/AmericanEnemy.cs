using System;
using UnityEngine;

public class AmericanEnemy : MonoBehaviour
{
    public int maxHealth;
    public float speed;
    public float jumpForce;
    public Rigidbody2D rb;
    public float currantHealth;
    public float standardAttackDamage;
    public float specialAttackDamage;
    public float specialAttackKnockback;
    public LayerMask groundLayer;
    private GameObject _enemy;
    private GameObject _player = null;
    private float _moveX;
    private float _moveY;
    public Rigidbody2D body;
    
    public System.Action<float, float> OnHealthChanged;

    private void Awake()
    {
        currantHealth = maxHealth;
    }

    private void Start()
    {
        _enemy = GameObject.FindWithTag("Enemy");
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
    
    public void ChangeHealth(int amount)
    {
        currantHealth = Mathf.Clamp(currantHealth + amount, 0, maxHealth);
        // Notify listeners whenever health changes
        OnHealthChanged?.Invoke(currantHealth, maxHealth);
        if (currantHealth == 0)
        {
            Destroy(_enemy);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _player = GameObject.FindWithTag("Player");
            print( "x: "+ _player.transform.position.x + "y: " + _player.transform.position.y);
            
            _moveX = _player.transform.position.x - _enemy.transform.position.x;
            _moveY = _player.transform.position.y - _enemy.transform.position.y;
            print("moveX: " + _moveX + "moveY: " + _moveY);
            if (IsGrounded())
            {
                
            }
            body.AddForce(new Vector2(_moveX, 0) * (Time.fixedDeltaTime * speed), ForceMode2D.Impulse);
        }
    }
}
