using Gameplay;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Player
{
    public class PlayerScript : MonoBehaviour
    {
        private static readonly int IsWalking = Animator.StringToHash("isWalking");
        public float speed = 30f;
        public Rigidbody2D body;
        public float jumpHeight = 6f;
        private Controls _controls;
        private float _moveDirection;
        public LayerMask groundLayer;
        public int maxHealth = 100;
        public int currentHealth;
        public System.Action<int, int> OnHealthChanged;

        [SerializeField]
        private Animator animator;
    
        void Awake()
        {        
            currentHealth = maxHealth;
            _controls = new Controls();
            _controls.player.jump.performed += _ =>
            {
                if (!IsGrounded()) return;
                body.AddForce(new Vector2(0, jumpHeight), ForceMode2D.Impulse);
            };

            // Notify listeners of initial health value
            OnHealthChanged?.Invoke(currentHealth, maxHealth);
        }

        void FixedUpdate()
        {
            float direction = _controls.player.move.ReadValue<float>(); // A: -1; D: 1; 0: not moving
            if (direction != 0)
            {
                transform.position += new Vector3(direction, 0, 0) * (Time.fixedDeltaTime * speed);
                animator.SetBool(IsWalking, true);
                if (direction > 0)
                {
                    transform.localScale = new Vector3(5, 5, 1); // Normal
                }
                else if (direction < 0)
                {
                    transform.localScale = new Vector3(-5, 5, 1); // Alles gespiegelt
                }
            }
            else
            {
                animator.SetBool(IsWalking, false);
            }
        }

        void OnEnable()
        {
            _controls.Enable();
        }

        void OnDisable()
        {
            _controls.Disable();
        }
        
        bool IsGrounded() {
            Vector2 position = transform.position;
            Vector2 direction = Vector2.down;
            float distance = 0.5f; 
            Vector2 boxSize = new Vector2(2f, 2f); 
    	
            RaycastHit2D hit = Physics2D.BoxCast(position, boxSize, 0f, direction, distance, groundLayer);
        
            Color debugColor = hit.collider != null ? Color.green : Color.red;
            Debug.DrawRay(position, direction * distance, debugColor, 0.5f);
        
            return hit.collider != null;   
        }

        public void ChangeHealth(int amount)
        {
            currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
            OnHealthChanged?.Invoke(currentHealth, maxHealth);
            if (currentHealth == 0)
            {
                Respawn.Instance.RespawnPlayer();
            }
            CheckHealth();
        }

        private void CheckHealth()
        {
            if (currentHealth <= 0)
            {
                SceneManager.LoadScene("RespawnMenu");
            }
        }
    }
}