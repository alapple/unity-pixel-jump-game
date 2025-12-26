using UnityEngine;

namespace Attack
{
    public class SickleAttack : AttackStats
    {
        private static readonly int SickleAttackTrigger = Animator.StringToHash("SickleAttack");

        public override int Damage => 1;
        public override float AttackRange => 1.3f;
        public override float AttackDelay => 0.58f;
        public override int AnimationTrigger => SickleAttackTrigger;
        public override Vector2 BoxSize => new Vector2(0.9f, 1f);
        
    }
}