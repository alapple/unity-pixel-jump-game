using System.Collections;
using Player;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

public class AmericanEnemy : MonoBehaviour
{
    public int maxHealth;
    public float speed;
    public int jumpForce;
    private float _currantHealth;
    public int standardAttackDamage;
    public float specialAttackDamage;
    public float specialAttackKnockback;
    public LayerMask groundLayer;
    public Rigidbody2D body;
    public float AttackDelay;
    private float _nextJumpTime;
    public SpriteRenderer spriteRenderer;
    private Vector2 BoxSize = new Vector2(1, 1);
    [FormerlySerializedAs("enemyLayer")] 
    [SerializeField]
    private LayerMask playerLayer;
    public System.Action<float, float> OnHealthChanged;

    private Transform _playerTarget;
    private bool _isAttacking;

    private void Awake()
    {
        _currantHealth = maxHealth;
    }

    private void Start()
    {
        var player = FindObjectOfType<PlayerScript>();
        if (player != null)
        {
            _playerTarget = player.transform;
        }
    }

    private void FixedUpdate()
    {
        if (_currantHealth <= 0 || _playerTarget == null) return;

        float distanceToPlayer = Vector2.Distance(transform.position, _playerTarget.position);
        float attackRange = 1.8f; 

        if (distanceToPlayer <= attackRange)
        {
            float direction = transform.position.x < _playerTarget.position.x ? 1 : -1;
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x) * direction, transform.localScale.y, transform.localScale.z);

            if (!_isAttacking)
            {
                StartCoroutine(HitDetectionWindow());
            }
        }
    }

    public void ChangeHealth(int amount)
    {
        _currantHealth = Mathf.Clamp(_currantHealth + amount, 0, maxHealth);
        OnHealthChanged?.Invoke(_currantHealth, maxHealth);
        if (_currantHealth == 0)
        {
            spriteRenderer.color = new Color(1f, 1f, 1f, 0f);
            standardAttackDamage = 0;
            specialAttackDamage = 0;
        }
        Debug.Log(_currantHealth);
    }

    private IEnumerator HitDetectionWindow()
    {
        _isAttacking = true;
        float elapsed = 0f;

        while (elapsed < AttackDelay)
        {
            float direction = Mathf.Sign(transform.lossyScale.x);
            
            Vector2 flippedOffset = new Vector2((BoxSize.x * 0.5f) * direction, 0);
            Vector2 scanPosition = (Vector2)transform.position + flippedOffset;

            Collider2D[] hitEnemies = Physics2D.OverlapBoxAll(scanPosition, BoxSize, 0f, playerLayer);
            
            foreach (Collider2D enemy in hitEnemies)
            {
                Debug.Log("Enter Loop");
                if (enemy.TryGetComponent<PlayerScript>(out var target))
                {
                    target.ChangeHealth(-standardAttackDamage);
                    Debug.Log($"Hit {enemy.name}!");
                    
                    yield return new WaitForSeconds(1.0f);
                    _isAttacking = false;
                    yield break;
                }
            }

            elapsed += Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(0.5f);
        _isAttacking = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        float direction = Mathf.Sign(transform.lossyScale.x);
        Vector2 flippedOffset = new Vector2((BoxSize.x * 0.5f) * direction, 0);
        Gizmos.DrawWireCube((Vector2)transform.position + flippedOffset, BoxSize);
    }
}