using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryBar : MonoBehaviour
{

    public Sprite[] sprites;
    public static int spriteIndex;

    public GameObject[] gameObjectsToSpawn;

    public GameObject slot;
    

    // Start is called before the first frame update
    void Start()
    {
        spriteIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {
        slot.GetComponent<Image>().sprite = sprites[spriteIndex];

    }

    public void OnClickButton()
    {
        Instantiate(gameObjectsToSpawn[spriteIndex].transform, new Vector3(0, Camera.main.transform.position.y + Camera.main.transform.localScale.y * 5, 0), Quaternion.identity);
    }

    public void OnClickRightButton()
    {
        if (!(spriteIndex == sprites.Length-1))
        {
            spriteIndex++;
        }
    }


    public void OnClickLeftButton()
    {
        if (spriteIndex != 0)
        {
            spriteIndex--;
            Debug.Log(spriteIndex);
        }
    }

}
