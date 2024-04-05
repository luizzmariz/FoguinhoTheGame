using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterOrientation : MonoBehaviour
{
    private Vector3 lastOrientation;
    //private string spriteOrientation;
    //public SpriteChanger sc;
    public Animator animator;
    public SpriteRenderer spriteRenderer;

    void Start()
    {
        lastOrientation = new Vector3(0, 0, -1);
        // spriteOrientation = "forward";
        animator = transform.GetComponent<Animator>();
        spriteRenderer = transform.GetComponent<SpriteRenderer>();
    }

    public void ChangeOrientation(Vector3 targetPoint)
    {
        Transform handTransform = transform.GetChild(0);
        Vector3 relativePos = targetPoint - handTransform.position;
        // Debug.DrawLine(targetPoint, transform.position, Color.red, 50);

        if(relativePos != Vector3.zero)
        {
            Quaternion LookAtRotation = Quaternion.LookRotation(relativePos);
            handTransform.rotation = Quaternion.Euler(handTransform.rotation.eulerAngles.x, LookAtRotation.eulerAngles.y, handTransform.rotation.eulerAngles.z);

            lastOrientation = relativePos;
            CheckSpriteOrientation(LookAtRotation.eulerAngles.y);
            // CheckSpriteOrientation(relativePos); --- Old way of changing sprites
        }
        else
        {
            Quaternion LookAtRotation = Quaternion.LookRotation(lastOrientation);
            handTransform.rotation = Quaternion.Euler(handTransform.rotation.eulerAngles.x, LookAtRotation.eulerAngles.y, handTransform.rotation.eulerAngles.z);
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
            // spriteOrientation = "back";
            spriteRenderer.flipX = false;
            animator.SetInteger("orientationNumber", 3);
        }
        else if(yAngle > 46f && yAngle <= 135f)
        {
            // spriteOrientation = "right";
            spriteRenderer.flipX = false;
            animator.SetInteger("orientationNumber", 0);
        }
        else if(yAngle > 135f && yAngle <= 225f)
        {
            // spriteOrientation = "forward";
            spriteRenderer.flipX = false;
            animator.SetInteger("orientationNumber", 2);
        }
        else if(yAngle > 225f && yAngle <= 315f)
        {
            // spriteOrientation = "left";
            spriteRenderer.flipX = true;
            animator.SetInteger("orientationNumber", 1);
        }
        //sc.ChangeSprite(spriteOrientation);
    }
}
