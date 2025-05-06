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

    public float Xpos = 0;
    public float Ypos = 0;

    public float YposC = 0;

    public float PosCheck = 0;
    

    public float falling = 0;
    public bool flagged = false;

    public bool cripple = false;

    public float Ruby = 0;

    public GameObject prefab;

    // Update is called once per frame
    [System.Obsolete]

    // Update is called once per frame
    void Start()
    {
        Xpos = transform.position.x;
        Ypos = transform.position.y;
    }

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
        if (Input.GetKeyDown(KeyCode.Space))
        { 
            if(Floored)
            {
                vel.y = JumpPower;
            }
            }
         RB.velocity = vel;
        //Use my FacingLeft variable to make my sprite face the right way
        SR.flipX = FacingLeft;

        //falling mechanics 
        /* PosCheck += Time.deltaTime;

         if(PosCheck >= 0.5f)
         {
             YposC = transform.position.y;
             PosCheck = 0;
         }

         float y = (transform.position.y);
         if (YposC > y)
         {
             falling += Time.deltaTime;

         }
         else if(Floored)
         { falling = 0; }



         if (falling >= 1)
         {
             Debug.Log("ME CAGOOOO");

             cripple = true;

             falling = 0;
         }*/
         //respawn beacon thing

         if(Input.GetKeyDown(KeyCode.R))
         {

                 if(flagged)
                 {
                 cripple = false;
                     Vector2 pos = transform.position;
                     pos.x = Xpos;
                     pos.y = Ypos;
                     transform.position = pos;
                     flagged = false;
                     ks.Activate = true;
                 }
                 else
                 {
                 if (Floored)
                     {
                         Xpos = transform.position.x;
                         Ypos = transform.position.y;
                         flagged = true;
                         Instantiate(prefab,transform.position,Quaternion.identity);
                     }

                 }

         }
        //new falling mechanic
        Ruby = RB.linearVelocityY;
        if (Ruby < 0)
        { 
            falling+= Time.deltaTime; 
        
        }
        else 
        {
            falling = 0;
        }
        if(falling > 1) 
        {
            Debug.Log("We cooked");
            falling = 0;   
        }
    }

     private void OnCollisionEnter2D(Collision2D other)
    {  
        //If I collide with something solid, mark me as being on the ground
        if(other.gameObject.CompareTag("Floor"))
        {
           Floored = true;
           falling = 0;
            if (cripple)
            {
                Debug.Log("You messed up");

                cripple = false;
            }
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
