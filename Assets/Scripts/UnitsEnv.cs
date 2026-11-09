using UnityEngine;

public static class UnitsEnv {

    public static void KnockBack(Rigidbody2D _victimRgBody, Vector2 attackerPosition, float _knockbackForce)
    {
        _victimRgBody.linearVelocity = Vector2.zero;

        float directionX = _victimRgBody.position.x - attackerPosition.x;

        if (directionX > 0)
        {
            _victimRgBody.AddForce(new Vector2(_knockbackForce, 3f), ForceMode2D.Impulse);
        }
        else
        {
            _victimRgBody.AddForce(new Vector2(-_knockbackForce, 3f), ForceMode2D.Impulse);
        }
    }

    public static void GoDown(Rigidbody2D RgBody)
    {
        RgBody.linearVelocity = new Vector2(0, -10);
    }

    public static float StartCooldown(float _cd)
    {
        return _cd;
    }
}




