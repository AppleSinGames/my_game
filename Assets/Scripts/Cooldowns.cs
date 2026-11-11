using UnityEngine;

public class Cooldowns 
{
    public float Duration;
    public float Cooldown;
    private float _lastUseTime = -999f;
    public bool IsReady => Time.time >= _lastUseTime + Cooldown;

    public void Use()
    {
        _lastUseTime = Time.time;
    }
}
