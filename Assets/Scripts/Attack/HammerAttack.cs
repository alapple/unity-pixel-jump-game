using UnityEngine;

namespace Attack
{
    public class HammerAttack : AttackStats
    {
        private static readonly int HammerAttackTrigger = Animator.StringToHash("HammerAttack");
        
        public override int Damage => 3;
        public override float AttackRange => 1.1f;
        public override float AttackDelay => 1.08f;
        public override int AnimationTrigger => HammerAttackTrigger;
        public override Vector2 BoxSize => new Vector2(0.8f, 1f);
    }
}