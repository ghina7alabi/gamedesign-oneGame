using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    int randomNumber;
    GameObject[] goalParticles;

    public GameObject GoalParticle;

    public Sprite[] GoalParticleSprite;
    private Sprite currentSprite;

    Vector3 randomIstantiationPosition;
    public float randomPositionRange;
    public int minimumRandomRange;
    public int maximumRandomRange;

    [HideInInspector] public int particleSpriteIndex;

    // Start is called before the first frame update
    void Start()
    {
        randomNumber = Random.Range(minimumRandomRange, maximumRandomRange);
        goalParticles = new GameObject[randomNumber];


        particleSpriteIndex = Random.Range(0, GoalParticleSprite.Length);
        currentSprite = GoalParticleSprite[particleSpriteIndex];

        for (int i = 0; i < goalParticles.Length; i++)
        {
            GameObject newParticle = Instantiate(GoalParticle, new Vector3(
                transform.position.x +  Random.Range(-randomPositionRange, randomPositionRange), 
                transform.position.y +  Random.Range(-randomPositionRange, randomPositionRange), 
                transform.position.z +  Random.Range(-randomPositionRange, randomPositionRange)),
                Quaternion.identity);

            newParticle.transform.parent = gameObject.transform;
            newParticle.GetComponent<SpriteRenderer>().sprite = currentSprite;
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "GameManager")
        {
            Destroy(this.gameObject);
        }
    }
}