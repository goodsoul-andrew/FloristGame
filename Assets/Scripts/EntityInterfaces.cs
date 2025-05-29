public interface IMoving
{
    public float MaxSpeed { get; set; }
    public float Speed { get; set; }
}

public interface IDamageable
{
    public Health HP {get; set;}
}