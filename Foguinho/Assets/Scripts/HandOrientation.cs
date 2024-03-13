using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandOrientation : MonoBehaviour
{
    private Vector3 lastOrientation;
    private string spriteOrientation;
    public SpriteChanger sc;
    // Start is called before the first frame update
    void Start()
    {
        lastOrientation = new Vector3(0, 0, -1);
        if(sc == null)
        {
            sc = transform.parent.GetComponent<SpriteChanger>();
        }
        spriteOrientation = "forward";
    }

    public void ChangeOrientation(Vector3 targetPoint)
    {
        Vector3 relativePos = targetPoint - transform.position;
        // Debug.DrawLine(targetPoint, transform.position, Color.red, 50);

        if(relativePos != Vector3.zero)
        {
            Quaternion LookAtRotation = Quaternion.LookRotation(relativePos);
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, LookAtRotation.eulerAngles.y, transform.rotation.eulerAngles.z);

            lastOrientation = relativePos;
            CheckSpriteOrientation(LookAtRotation.eulerAngles.y);
            // CheckSpriteOrientation(relativePos); --- Old way of changing sprites
        }
        else
        {
            Quaternion LookAtRotation = Quaternion.LookRotation(lastOrientation);
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, LookAtRotation.eulerAngles.y, transform.rotation.eulerAngles.z);
        }
    }

    // void CheckSpriteOrientation(Vector3 orientationVector) --- Old way of changing sprites: the facing forward and facing back sprites had priority  
    // {
    //     if(orientationVector.x > 0f)
    //     {
    //         spriteOrientation = "right";
    //         Debug.Log(orientationVector + " means right");
    //     }
    //     else if(orientationVector.x < 0f)
    //     {
    //         spriteOrientation = "left";
    //         Debug.Log(orientationVector + " means left");
    //     }
    //     if(orientationVector.z > 0f)
    //     {
    //         spriteOrientation = "back";
    //         Debug.Log(orientationVector + " means back");
    //     }
    //     else if(orientationVector.z < 0f)
    //     {
    //         spriteOrientation = "forward";
    //         Debug.Log(orientationVector + " means forward");
    //     }
    //     sc.ChangeSprite(spriteOrientation);
    // }

    void CheckSpriteOrientation(float yAngle)
    {
        if((yAngle > 315f && yAngle <= 360f) || (yAngle >= 0f && yAngle <= 45f))
        {
            spriteOrientation = "back";
            //Debug.Log(yAngle + "° means back");
        }
        else if(yAngle > 46f && yAngle <= 135f)
        {
            spriteOrientation = "right";
            //Debug.Log(yAngle + "° means right");
        }
        else if(yAngle > 135f && yAngle <= 225f)
        {
            spriteOrientation = "forward";
            //Debug.Log(yAngle + "° means forward");
        }
        else if(yAngle > 225f && yAngle <= 315f)
        {
            spriteOrientation = "left";
            //Debug.Log(yAngle + "° means left");
        }
        sc.ChangeSprite(spriteOrientation);
    }
}
