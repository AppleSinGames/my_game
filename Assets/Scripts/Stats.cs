using UnityEngine;

public class Spell
{
    public float Duration;
    public float Cooldown;
    public float KnockbackForce;
    public float Speed;
    public float Direction;

    private float _lastUseTime = -999f;
    public bool IsReady => Time.time >= _lastUseTime + Cooldown;

    public void Use()
    {
        _lastUseTime = Time.time;
    }
}

public class Unit 
{
    public int Health;
    public int MaxHealth;
    public float Speed;
    public float DashSpeed;
    public float JumpForce;
    public float AvailableJumps;
    public float KnockbackForce;
    public int Direction;
}


