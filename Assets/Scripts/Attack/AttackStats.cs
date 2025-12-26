using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

namespace Attack
{
    [System.Serializable]
    public abstract class AttackStats : MonoBehaviour
    {
        public abstract int Damage { get; }
        public abstract float AttackRange { get; }
        public abstract float AttackDelay { get; }
        public abstract int AnimationTrigger { get; }
        private bool AttackBlocked { get; set; }
        public Animator Animator { get; set; }
        [SerializeField] 
        private LayerMask enemyLayer;
        public abstract Vector2 BoxSize { get; }
       
        
        public void Attack()
        {
            if (AttackBlocked) return;
            Animator.SetTrigger(AnimationTrigger);
            
            StartCoroutine(HitDetectionWindow());

            AttackBlocked = true;
            StartCoroutine(DelayAttack());
        }
        
        private IEnumerator HitDetectionWindow()
        {
            
            float elapsed = 0f;

            while (elapsed < AttackDelay)
            {
                float direction = Mathf.Sign(transform.lossyScale.x);
                
                Vector2 flippedOffset = new Vector2(BoxSize.x * direction, 0);
                Vector2 scanPosition = (Vector2)transform.position + flippedOffset;

                DrawDebugBox(scanPosition, BoxSize, Color.magenta, Time.deltaTime);

                Collider2D[] hitEnemies = Physics2D.OverlapBoxAll(scanPosition, BoxSize, 0f, enemyLayer);

                foreach (Collider2D enemy in hitEnemies)
                {
                    if (enemy.TryGetComponent<AmericanEnemy>(out var target))
                    {
                        target.ChangeHealth(-Damage);
                        Debug.Log($"Hit {enemy.name}!");
                        yield break; 
                    }
                }

                elapsed += Time.deltaTime;
                yield return null;
            }
        }
        
        private void DrawDebugBox(Vector2 center, Vector2 size, Color color, float duration)
        {
            Vector2 halfSize = size / 2f;
            
            // Calculate the 4 corners of the box
            Vector2 topLeft = center + new Vector2(-halfSize.x, halfSize.y);
            Vector2 topRight = center + new Vector2(halfSize.x, halfSize.y);
            Vector2 bottomLeft = center + new Vector2(-halfSize.x, -halfSize.y);
            Vector2 bottomRight = center + new Vector2(halfSize.x, -halfSize.y);

            // Draw the lines between corners
            Debug.DrawLine(topLeft, topRight, color, duration);
            Debug.DrawLine(topRight, bottomRight, color, duration);
            Debug.DrawLine(bottomRight, bottomLeft, color, duration);
            Debug.DrawLine(bottomLeft, topLeft, color, duration);
            
        }


        private IEnumerator DelayAttack()
        {
            yield return new WaitForSeconds(AttackDelay);
            AttackBlocked = false;
        }
        

    }
}