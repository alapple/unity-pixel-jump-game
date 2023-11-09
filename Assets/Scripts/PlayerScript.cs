using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public float speed = 30f;
    public Rigidbody2D body;
    public float jumpHeight = 6f;
    private Controls _controls;
    private float _moveDirection;
    private GroundCheck _groundCheck;


    // Start is called before the first frame update
    void Awake()
    {
        _groundCheck = GameObject.FindGameObjectWithTag("groundCheck").GetComponent<GroundCheck>();
        
        _controls = new Controls();
        _controls.player.jump.performed += _ =>
        {
            if (!_groundCheck.isGrounded) return;
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
}