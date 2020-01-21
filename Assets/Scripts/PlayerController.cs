using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Animator anim;
    int currentState = 0;

    public GameObject emotionHolder;
    public float emotionHolderPosX;
    

    //follow opponent variables
    private Transform followPosition;
    public float speed = 50;
    bool followOpponent = false;


    //color change
    float lerpColorTimer = 0;
    public static float lerpColorTime = 5;
    bool changeColor = false;
    GameObject collidedGO;

    public static int score = 0;
    public static int heartCount = 3;

    public ParticleSystem particles;

    bool collided = false;


    public AudioSource powerupSound;

    // Start is called before the first frame update
    void Start()
    {
        anim = this.GetComponent<Animator>();
        emotionHolder.GetComponent<SpriteRenderer>().enabled = false;
        emotionHolder.GetComponent<CircleCollider2D>().enabled = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        //AnimationControllers
        if (!(this.GetComponent<Rigidbody2D>().velocity == Vector2.zero))
        {
            currentState = 1; //walk
        }
        if (this.GetComponent<Rigidbody2D>().velocity == Vector2.zero && !changeColor)
        {
            currentState = 0; //idle
        }
        if (this.GetComponent<Rigidbody2D>().velocity.y < -0.01 & !followOpponent)
        {
            currentState = 2; //fall
        }


        //EmotionPosition
        if (transform.position.x > 0)
        {
            emotionHolder.transform.position = transform.position + new Vector3(emotionHolderPosX, 0, 0);
            this.GetComponent<SpriteRenderer>().flipX = false;
        }

        if (transform.position.x < 0)
        { 
            emotionHolder.transform.position = transform.position + new Vector3(-emotionHolderPosX, 0, 0);
            this.GetComponent<SpriteRenderer>().flipX = true;
        }

        //FollowOpponent
        if (followOpponent)
        {
            currentState = 1; //walk
            if (transform.position.x > 0)
            {
                this.GetComponent<Rigidbody2D>().velocity = Vector3.right * speed * Time.deltaTime;
            }
            if (transform.position.x < 0)
            {
                this.GetComponent<Rigidbody2D>().velocity = Vector3.left * speed * Time.deltaTime;
            }

        }
        

        //LerpColorWithTime
        if (changeColor)
        {
            
            if (lerpColorTimer < 1)
            {
                lerpColorTimer += Time.deltaTime / lerpColorTime;
                currentState = 4;
            }
            this.GetComponent<SpriteGlow.SpriteGlowEffect>().GlowColor = Color.Lerp(this.GetComponent<SpriteGlow.SpriteGlowEffect>().GlowColor, collidedGO.gameObject.transform.parent.gameObject.GetComponent<SpriteGlow.SpriteGlowEffect>().GlowColor, lerpColorTimer);

            //Leave after lerping
            if (lerpColorTimer > 1)
            {
                collidedGO.gameObject.transform.parent.gameObject.GetComponent<Opponents>().waited = true;
                followOpponent = true;
                
            }

            SwitchAnimations();
        }

        SwitchAnimations();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Trigger" && !collision.gameObject.transform.parent.gameObject.GetComponent<Opponents>().gotAngry && !changeColor)
        {
            if (collision.gameObject.transform.parent.gameObject.GetComponent<Goal>().particleSpriteIndex == this.gameObject.GetComponentInChildren<EmotionController>().spriteIndex)
            {
                followPosition = collision.gameObject.GetComponent<Transform>();

                //emotion
                emotionHolder.GetComponent<SpriteRenderer>().enabled = true;
                emotionHolder.GetComponent<CircleCollider2D>().enabled = true;

                //so opp doesnt leave
                collision.gameObject.transform.parent.gameObject.GetComponent<Opponents>().activeTime = false;

                //so opp timer stops
                collision.gameObject.transform.parent.gameObject.GetComponent<Opponents>().decreaseBar = false;
                //destroy timeBar
                Destroy(collision.transform.parent.gameObject.transform.GetChild(1).gameObject, 2);

                //lerp
                changeColor = true;
                collidedGO = collision.gameObject;

                score++;
                powerupSound.Play();
             
            }

            particles.Play();


            if (!(collision.gameObject.transform.parent.gameObject.GetComponent<Goal>().particleSpriteIndex == this.gameObject.GetComponentInChildren<EmotionController>().spriteIndex) && !collided)
            {
                collision.gameObject.transform.parent.gameObject.GetComponent<Opponents>().waited = true;

                heartCount--;
                collided = true;
                Debug.Log("once");
                particles.Play();

            }

        }
        
    }
    

    void SwitchAnimations()
    {
        switch (currentState)
        {
            case 0:
                anim.SetInteger("currentState", 0); //idle
                break;

            case 1:
                anim.SetInteger("currentState", 1); //walk
                break;

            case 2:
                anim.SetInteger("currentState", 2); //fall
                break;

            case 3:
                anim.SetInteger("currentState", 3); //afraid
                break;

            case 4:
                anim.SetInteger("currentState", 4); //merge
                break;
        }
    }

}
