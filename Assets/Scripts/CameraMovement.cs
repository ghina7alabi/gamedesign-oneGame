using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("IncreaseLevel", 10, 10);
        
    }

    // Update is called once per frame
    void Update()
    {
        this.gameObject.transform.Translate(Vector3.up * Time.deltaTime * speed);
    }


    void IncreaseLevel() //Fix numbers later
    {
        speed += 0.1f; // 0.7
       // Opponents.timeWaited -= 0.1f; //15
        if (Opponents.timeWaited < 2)
        {
            //Opponents.timeWaited = 2;
        }
    }
}