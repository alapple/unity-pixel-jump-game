using UnityEngine;

namespace Attack
{
    public class SickleAttack : AttackStats
    {
        private static readonly int SickleAttackTrigger = Animator.StringToHash("SickleAttack");

        public override int Damage => 1;
        public override float AttackRange => 2;
        public override float AttackDelay => 0.3f;
        public override int AnimationTrigger => SickleAttackTrigger;
        
    }
}