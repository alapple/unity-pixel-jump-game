using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScript : MonoBehaviour
{
    public Rigidbody2D body;
    public float jumpHeight = 6f;
    private Controls controls;


    // Start is called before the first frame update
    void Awake()
    {
        controls = new Controls();
        controls.player.jump.performed += OnJump;
    }
    
    void OnEnable()
    {
        controls.Enable();
    }
    void OnDisable()
    {
        controls.Disable();
    }

    void OnJump(InputAction.CallbackContext context)
    {
        Debug.Log("OnJump called");
        body.velocity += new Vector2(0, jumpHeight);
        transform.Translate(new Vector3(0, 0, 0));
    }
}