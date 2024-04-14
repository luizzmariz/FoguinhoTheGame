using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayCycleManager : MonoBehaviour
{
    [Header("CycleRotation")]
    Vector3 rotation = Vector3.zero;
    public float degreesPerSecond;
    public float rotatingDuration;
    public bool isRotating;
    public bool isDay;
    public int dayPhase;
    public GameObject sunLight;

    [Header("ActionTokens")]
    public float currentActionTokensTaken;
    public float currentAreaActionTokens;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isRotating)
        {
            if(rotatingDuration > 0)
            {
                rotatingDuration -= Time.deltaTime;
                rotation.x = degreesPerSecond * Time.deltaTime;
                transform.Rotate(rotation, Space.World);
            }
            else
            {
                rotatingDuration = 0;
                ControlDayCycle();
            }
        }
    }

    public void UpdateActionTokens(float actionTokens)
    {
        currentActionTokensTaken += actionTokens;
        if(currentActionTokensTaken % (currentAreaActionTokens/3) == 0)
        {
            ControlDayCycle();
        }
    }

    public void ControlDayCycle()
    {
        if(isRotating)
        {
            isRotating = false;
        }
        else
        {
            if(isDay)
            {
                if(dayPhase == 2)
                {
                    isDay = false;
                    Debug.Log("issss night baby");
                }
                degreesPerSecond = 15;
                rotatingDuration = 4;
            }
            else
            {
                Debug.Log("hehepa");
            }
        }
    }
}