namespace GameBoxProject
{
    public interface IDamagable
    {
        void TakeDamage(float damage);
        float CurrentHP { get; }
    }
}