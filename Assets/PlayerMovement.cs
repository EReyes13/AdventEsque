using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public SpriteRenderer SR;
    public Rigidbody2D RB;
    public Collider2D Coll;
    
    public float Speed = 5;
    public float JumpPower = 10;
    public float Gravity = 3;
     public bool OnGround = false;
    public bool FacingLeft = false;

    public bool Floored = false;

    // Update is called once per frame
    [System.Obsolete]
    void Update()
    {
         Vector2 vel = RB.velocity;

        if (Input.GetKey(KeyCode.RightArrow))
        { 
            //If I hit right, move right
            vel.x = Speed;
            //If I hit right, mark that I'm not facing left
            FacingLeft = false;
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        { 
            //If I hit left, move right
            vel.x = -Speed;
            //If I hit left, mark that I'm facing left
            FacingLeft = true;
        }
        else
        {  //If I hit neither, come to a stop
            vel.x = 0;
        }

        //If I hit Z and can jump, jump
        if (Input.GetKeyDown(KeyCode.Z ) && (Floored = true))
        { 
            vel.y = JumpPower;
            }
         RB.velocity = vel;
        //Use my FacingLeft variable to make my sprite face the right way
        SR.flipX = FacingLeft;

    }

     private void OnCollisionEnter2D(Collision2D other)
    {  
        //If I collide with something solid, mark me as being on the ground
        if(other.gameObject.CompareTag("Floor"))
        {
           Floored = true;
        }
        else
        {
            Floored = false;
        }

    }
    private void OnCollisionExit2D(Collision2D other)
    {
        Floored = false;
    }
}
