using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Opponents : MonoBehaviour
{
    bool spawnedAndWalked = false;
    [HideInInspector] public bool waited = false;
    [HideInInspector] public bool gotAngry = false;
    bool gotMerged = false;

    int speed;

    [HideInInspector] public bool activeTime = true;
    float timer = 0;
    public static float timeWaited = 10;

    int currentState;
    Animator anim;

    public GameObject healthbar;
    [HideInInspector] public bool decreaseBar = true;
    Color randomGlowColor;


    public ParticleSystem particles;

    // Start is called before the first frame update
    void Start()
    {
        speed = Random.Range(30, 50);
        randomGlowColor = Random.ColorHSV();

        anim = this.GetComponent<Animator>();
        this.GetComponent<SpriteGlow.SpriteGlowEffect>().GlowColor = randomGlowColor;
        healthbar.GetComponent<SpriteGlow.SpriteGlowEffect>().GlowColor = randomGlowColor;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x > 0 && spawnedAndWalked == false && gameObject.tag == "VisibleOpponent")
        {
            this.GetComponent<Rigidbody2D>().velocity = Vector3.left * speed * Time.deltaTime;
            this.GetComponent<SpriteRenderer>().flipX = true;
            if (transform.position.x <= 2.2)
            {
                spawnedAndWalked = true;
                this.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            }
        }

        if (transform.position.x < 0 && spawnedAndWalked == false && gameObject.tag == "VisibleOpponent")
        {
            this.GetComponent<Rigidbody2D>().velocity = Vector3.right * speed * Time.deltaTime;
            if (transform.position.x >= -2.2)
            {
                spawnedAndWalked = true;
                this.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            }
        }

        if (spawnedAndWalked)
        {
            if (activeTime)
            {
                timer += Time.deltaTime;
                if (healthbar.transform.localScale.x >= 0.01 && decreaseBar)
                {
                    healthbar.transform.localScale -= new Vector3(Time.deltaTime/timeWaited, 0);
                }
                if (healthbar.transform.localScale.x <= 0.01)
                {
                    healthbar.transform.localScale = new Vector3(0.01f, 0);
                }
            }

            this.GetComponent<Rigidbody2D>().velocity = Vector2.zero;

            //Time went out and he got angry
            if (timer > timeWaited) //waited to merge
            {
                if (!waited)
                {
                    particles.Play();
                    StartCoroutine(waitAngry(2));
                }
            }


            //Leave after waiting
            if (waited)
            {
                currentState = 1; //walk
                if (transform.position.x > 0)
                {
                    this.GetComponent<Rigidbody2D>().velocity = Vector3.right * speed * Time.deltaTime;
                    this.GetComponent<SpriteRenderer>().flipX = false;
                }
                if (transform.position.x < 0)
                {
                    this.GetComponent<Rigidbody2D>().velocity = Vector3.left * speed * Time.deltaTime;
                    this.GetComponent<SpriteRenderer>().flipX = true;
                }

                this.gameObject.tag = "InvisibleOpponent";
            }
        }


        //Animations
        if (!(this.GetComponent<Rigidbody2D>().velocity == Vector2.zero))
        {
            currentState = 1; //walk
        }
        if (this.GetComponent<Rigidbody2D>().velocity == Vector2.zero & !gotAngry)
        {
            currentState = 0; //idle
        }
        if (this.GetComponent<Rigidbody2D>().velocity.y < -0.01)
        {
            currentState = 2; //fall
        }

        SwitchAnimations();
    }

    public IEnumerator waitAngry(float seconds)
    {
        gotAngry = true;
        currentState = 4; //angry
        yield return new WaitForSeconds(seconds);
        waited = true;
        gotAngry = false;

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
                anim.SetInteger("currentState", 4); //angry
                break;
        }
    }
    

    void OnBecameVisible()
    {
        this.gameObject.tag = "VisibleOpponent";
    }


}
