using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class PlayerScript : MonoBehaviour
{
    public float speed = 30f;
    public Rigidbody2D body;
    public float jumpHeight = 6f;
    private Controls controls;
    private float moveDirection;


    // Start is called before the first frame update
    void Awake()
    {
        controls = new Controls();
        controls.player.jump.performed += ctx => body.AddForce(new Vector2(0, jumpHeight));
        controls.player.move.performed += ctx => Debug.Log($"Move! {ctx.ReadValue<float>()}");
    }

    void Update()
    {
        float direction = controls.player.move.ReadValue<float>(); // A: -1; D: 1; 0: not moving
        if (direction != 0)
        {
            moveDirection = direction;
        }
    }

    private void FixedUpdate()
    {
        MoveCharacter(moveDirection);
    }

    void MoveCharacter(float direction)
    {
        body.AddForce(new Vector2(direction * speed * 10 * Time.fixedDeltaTime, 0));
    }

    void OnEnable()
    {
        controls.Enable();
    }

    void OnDisable()
    {
        controls.Disable();
    }
}