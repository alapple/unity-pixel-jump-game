using System;
using UnityEngine;


public class GroundCheck : MonoBehaviour
{
    public bool isGrounded = true;

    private void OnTriggerEnter2D(Collider2D other)
    {
        UpdateIsGrounded(other);
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        isGrounded = false;
    }

    void UpdateIsGrounded(Collider2D other)
    {
        Debug.Log($"compare tag: us: {tag} other: {other.gameObject.tag}");
        isGrounded = other.gameObject.CompareTag("ground");
    }
}