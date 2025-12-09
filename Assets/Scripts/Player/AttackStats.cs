namespace Player
{
    [System.Serializable]
    public class AttackStats
    {
        public int damage;
        public float attackRange;
        public float attackRate;
        public int animationTrigger;

        public AttackStats(int damage, float attackRange, float attackRate, int animationTrigger)
        {
            this.damage = damage;
            this.attackRange = attackRange;
            this.attackRate = attackRate; 
            this.animationTrigger = animationTrigger;
        }
    }
}