using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmotionController : MonoBehaviour
{
    public Sprite[] sprites;

    [HideInInspector] public int spriteIndex = InventoryBar.spriteIndex;

    // Start is called before the first frame update
    void Start()
    {
        spriteIndex = InventoryBar.spriteIndex;
        this.gameObject.GetComponent<SpriteRenderer>().sprite = sprites[spriteIndex];
    }
}
