using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public float speed = 30f;
    public Rigidbody2D body;
    public float jumpHeight = 6f;
    private Controls _controls;
    private float _moveDirection;
	public LayerMask groundLayer;



    // Start is called before the first frame update
    void Awake()
    {        
        _controls = new Controls();
        _controls.player.jump.performed += _ =>
        {
            if (!IsGrounded()) return;
            body.AddForce(new Vector2(0, jumpHeight), ForceMode2D.Impulse);
        };
    }

    void FixedUpdate()
    {
        float direction = _controls.player.move.ReadValue<float>(); // A: -1; D: 1; 0: not moving
        if (direction != 0)
        {
            transform.position += new Vector3(direction, 0, 0) * Time.fixedDeltaTime * speed;
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
    	Vector2 boxSize = new Vector2(0.13f, 0.5f); 
    	
    	RaycastHit2D hit = Physics2D.BoxCast(position, boxSize, 0f, direction, distance, groundLayer);
        
        Color debugColor = hit.collider != null ? Color.green : Color.red;
        Debug.DrawRay(position, direction * distance, debugColor, 0.5f);
        
        return hit.collider != null;   
	}
}