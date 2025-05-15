using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
   //main stats and vars
    public SpriteRenderer SR;
    public Rigidbody2D RB;
    public Collider2D Coll;
    public Animator ani;

    public AudioClip Recallsfx;

    public AudioClip Tyronesfx;

    public AudioClip invissfx;
    public AudioSource audiosource;

    public AudioSource audiotwo;

    public AudioSource invissource;

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

    //crippple mech
    public float falling = 0;
    public bool flagged = false;

    public bool cripple = false;

    public bool staggering = false;

    public float Duration = 10;

    public float Ruby = 0;
    //recall
    public GameObject prefab;
    //recall fix
    public GameObject RecallEffect;

    //Color current;

    //invis mech 
    public bool Isinvis;
    public float Iduration = 5;
    
    //state machine time
    public enum playerstate {Idle, Moving, Tyrone, Speedboi}
   public playerstate PS;
    public bool statecomplete;
    // Update is called once per frame
    void Start()
    {
        Xpos = transform.position.x;
        Ypos = transform.position.y;
        audiosource = gameObject.GetComponent<AudioSource>();
        audiotwo = gameObject.GetComponent<AudioSource>();
        invissource = gameObject.GetComponent<AudioSource>();
    }

    
    void Update()
    {
               if(statecomplete)
        {
           SelectState();
        }
       
        UpdateState();


        Vector2 vel = RB.linearVelocity;
       

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


        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (Floored)
            {
                vel.y = JumpPower;
            }
        }
        RB.linearVelocity = vel;
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

        if (Input.GetKeyDown(KeyCode.R))
        {

            if (flagged)
            {
                cripple = false;
                Vector2 pos = transform.position;
                pos.x = Xpos;
                pos.y = Ypos;
                transform.position = pos;
                flagged = false;
                //ks.Activate = true;
                audiosource.PlayOneShot(Recallsfx, 1);
                Destroy(RecallEffect);
                //SR.color = current;
            }
            else
            {
                if (Floored)
                {
                    //current = SR.color;
                    Xpos = transform.position.x;
                    Ypos = transform.position.y + 0.1f;
                    flagged = true;
                    RecallEffect = Instantiate(prefab, transform.position, Quaternion.identity);
                    //SR.color = new Color(SR.color.r,SR.color.g,SR.color.b,0.5f);
                }

            }

        }

        //semi invis mechanic

        if (Input.GetKeyDown(KeyCode.E))
        {
            Isinvis = true;
            invissource.PlayOneShot(invissfx, 1);

         }
        
       
        //new falling mechanic
            Ruby = RB.linearVelocityY;
        if (Ruby < 0)
        {
            falling += Time.deltaTime;

        }
        else
        {
            falling = 0;
        }
        if (falling > 1.5f)
        {
            // Debug.Log("We cooked");
            falling = 0;
            cripple = true;
        }
    }

    //state machine shenanigans
    public void UpdateState()
    {
        statecomplete = false;
        switch(PS)
        {
            case playerstate.Idle:
            UpdateIdle();
            break;
            case playerstate.Tyrone:
            UpdateTyrone();
            break;
            case playerstate.Speedboi:
            UpdateSpeedboi();
            break;
        }
    }

    public void SelectState()
    {
        if (staggering)
        {
            PS = playerstate.Tyrone;
        }
        else if (Isinvis)
        {
            PS = playerstate.Speedboi;
        }
        else
        {
            PS = playerstate.Idle;
         }
    }
    public void UpdateIdle() 
    {


        if(staggering)
        {
            statecomplete = true;
        }
        if(Input.GetKeyDown(KeyCode.E))
        {
            statecomplete = true;
        }

       
    }

     public void UpdateTyrone()
        {
             if (staggering)
        {
           
            Speed = 2;
            JumpPower = 4;
            Duration -= Time.deltaTime;
        }

        if (Duration <= 0.001)
        {
            Speed = 5;
            JumpPower = 10;

            staggering = false;
            Duration = 10;
            statecomplete = true;
        }
        }
    public void UpdateSpeedboi()
    {
         {
            Speed = 9;
            JumpPower = 13;
        
        }
        if (Isinvis) 
        {
            SR.color = new Color(0, 0, 0, .5f);
            Iduration -= Time.deltaTime;
        }
        if (Iduration <= 0.01f)
        {
            Speed = 5;
            JumpPower = 10;
            SR.color = new Color(1f, 1f, 1.0f, 1.0f);
            Isinvis = false;
            Iduration = 5;
            statecomplete = true; 
        }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        //If I collide with something solid, mark me as being on the ground
        if (other.gameObject.CompareTag("Floor"))
        {
            Floored = true;
            falling = 0;
            if (cripple)
            {
                // Debug.Log("You messed up");
                 audiotwo.PlayOneShot(Tyronesfx, 1);
                cripple = false;
                staggering = true;

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
    