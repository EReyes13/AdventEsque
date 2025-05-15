using UnityEngine;

public class enemmove : MonoBehaviour
{
    public Rigidbody2D RB;

    public bool MF;
    public float timer = 0;

    public Animation anim;

    // Update is called once per frame
    void Update()
    {
        Vector2 vel = RB.linearVelocity;
        if (MF)
        {
            RB.linearVelocityX = -1;
            timer -= Time.deltaTime;
        }
        else
        {
            RB.linearVelocityX = 1;
            timer += Time.deltaTime;
        }
        if (timer >= 2)
        {
            MF = true;
        }
        if (timer <= 0.01)
        {
            MF = false;
        }
    }

   
}
