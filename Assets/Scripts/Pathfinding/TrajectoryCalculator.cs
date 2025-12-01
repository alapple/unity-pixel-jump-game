using System;
using Player;
using UnityEngine;

namespace Pathfinding
{
    public class TrajectoryCalculator : MonoBehaviour
    {
        public Rigidbody2D rb;
        
        [Header("Debug Settings")]  
        public bool logLandingPosition = false;

        private float g;
        private AmericanEnemy _americanEnemy;

        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            g = Physics2D.gravity.y;
        }

        void CalculateLandingPos(Vector3 initPosition, Vector3 velocity)
        {
            float v0X = rb.GetPointVelocity(transform.position).x;
            float v0Y = _americanEnemy.jumpForce;
            float t = v0Y / v0X;

            float vx = v0X;
            float vy = v0Y - g * t;
            
            
            
        }
        
        
    }
}