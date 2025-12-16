using UnityEngine;

namespace Attack
{
    public class HammerAttack : AttackStats
    {
        private static readonly int HammerAttackTrigger = Animator.StringToHash("HammerAttack");
        
        public override int Damage => 3;
        public override float AttackRange => 2;
        public override float AttackDelay => 1;
        public override int AnimationTrigger => HammerAttackTrigger;
    }
}