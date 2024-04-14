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
        isRotating = false;
        dayPhase = 0;
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
                sunLight.transform.Rotate(rotation, Space.World);
            }
            else
            {
                rotatingDuration = 0;
                isRotating = false;
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
        if(!isRotating)
        {
            if(isDay)
            {
                if(dayPhase == 2)
                {
                    dayPhase = 0;
                    isDay = false;
                    degreesPerSecond = 5;
                    rotatingDuration = 20;
                }
                else
                {
                    dayPhase++;
                    degreesPerSecond = 15;
                    rotatingDuration = 4;
                    Debug.Log(sunLight.transform.rotation.eulerAngles);
                }
                isRotating = true;
            }
            else
            {
                sunLight.transform.rotation = new Quaternion(0.42261827f,0,0,0.906307876f);
                isDay = true;
            }
        }
    }
}