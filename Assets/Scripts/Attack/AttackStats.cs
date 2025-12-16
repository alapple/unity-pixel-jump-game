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
        public Animator animator { get; set; }
        
        public void Attack()
        {
            if (AttackBlocked) return;
            animator.SetTrigger(AnimationTrigger);
            AttackBlocked = true;
            StartCoroutine(DelayAttack());
        }

        private IEnumerator DelayAttack()
        {
            yield return new WaitForSeconds(AttackDelay);
            AttackBlocked = false;
        }
    }
}