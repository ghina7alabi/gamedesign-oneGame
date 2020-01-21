using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnOpponents : MonoBehaviour
{
    public GameObject opponent;

    void Start()
    {
        Instantiate(opponent, transform.position, Quaternion.identity);
    }
    
}
