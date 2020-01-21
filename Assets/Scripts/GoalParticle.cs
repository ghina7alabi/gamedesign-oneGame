using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalParticle : MonoBehaviour
{
    public float gravitationalForce;
    public float forceAmountForRotation;

    private Vector3 directionOfParticleFromObject;
    private Vector3 directionOfObjectFromParticle;

    // Start is called before the first frame update
    void Start()
    {
        directionOfParticleFromObject = Vector3.zero;
        directionOfObjectFromParticle = Vector3.zero;

        this.gameObject.GetComponent<Rigidbody2D>().AddForce(transform.right * forceAmountForRotation);
    }

    private void FixedUpdate()
    {
            GravitateAroundObject(gravitationalForce);
    }


    void GravitateAroundObject(float gravitationalForce)
    {
        directionOfParticleFromObject = (transform.parent.position - transform.position).normalized;
        this.gameObject.GetComponent<Rigidbody2D>().AddForce(directionOfParticleFromObject * gravitationalForce);

        directionOfObjectFromParticle = transform.position - transform.parent.position;
        transform.right = Vector3.Cross(directionOfObjectFromParticle, Vector3.forward);
    }


}
