using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteChanger : MonoBehaviour
{
    private SpriteRenderer sr;
    public Sprite characterForward;
    public Sprite characterBack;
    public Sprite characterRight;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        sr.sprite = characterForward;
    }

    public void ChangeSprite(string orientation)
    {
        switch(orientation)
        {
            case "forward":
                sr.sprite = characterForward;
                sr.flipX = false;
                break;
            case "back":
                sr.sprite = characterBack;
                sr.flipX = false;
                break;
            case "left":
                sr.sprite = characterRight;
                sr.flipX = true;
                break;
            case "right":
                sr.sprite = characterRight;
                sr.flipX = false;
                break;
            default:
                break;
        }
    }
}
