using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlatformManager : MonoBehaviour
{

    public GameObject platform;
    public int xPlatform = 2; //two columns
    public static int yPlatform = 50; //rows

    float xOffset = 9;
    public float yOffset = 2.4f;
    GameObject[,] platforms;
    GameObject newPlatform;




    // Start is called before the first frame update
    void Start()
    {
        setPlatforms();
    }


    void setPlatforms()
    {
        platforms = new GameObject[xPlatform, yPlatform];

        for (int y = 0; y < yPlatform; y++) //yAxis
        {

            for (int x = 0; x < xPlatform; x++) //xAxis
            {
                newPlatform = Instantiate(platform, new Vector3(transform.position.x + (xOffset * x), transform.position.y + (yOffset * y), 0), platform.transform.rotation);
                platforms[x, y] = newPlatform;
            }
        }

    }

}

    
