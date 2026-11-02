using UnityEngine;

public static class UnitsEnv {

    public static void KnockBack(Rigidbody2D RgBody, Collision2D collision)
    {

        RgBody.linearVelocity = Vector2.zero;

        if (collision.GetContact(0).normal.x < 0)
        {
            RgBody.AddForce(new Vector2(-5, 5), ForceMode2D.Impulse);
        }
        if (collision.GetContact(0).normal.x > 0)
        {
            RgBody.AddForce(new Vector2(5, 5), ForceMode2D.Impulse);
        }
    }

    public static void GoDown(Rigidbody2D RgBody)
    {

        RgBody.linearVelocity = new Vector2(0, -10);
    }
    
}




