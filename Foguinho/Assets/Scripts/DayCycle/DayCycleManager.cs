using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayCycleManager : MonoBehaviour
{
    [Header("AmbientColor")]
    public Gradient ambientColor;

    [Header("CycleRotation")]
    Vector3 rotation = Vector3.zero;
    public float degreesPerSecond;
    public float rotatingDuration;
    public bool timeIsPassing;
    public bool isDay;
    public int dayPhase;
    public GameObject sunLight;

    [Header("DayTime")]
    public float dayTime;
    public float newDayTime;
    public float secondsBetweenHours;

    [Header("ActionTokens")]
    public float currentActionTokensTaken;
    public float currentAreaActionTokens;

    // Start is called before the first frame update
    void Start()
    {
        if(sunLight == null)
        {
            sunLight = GameObject.Find("Directional Light"); 
        }
        //360° (volta completa) / 24h (horas do dia) -> 15 graus por hora

        //hora q o jogo começa = 5:30h -> 5.5f
        //dayTime = 5.5f;
        //graus por segundo (3 segundos para percorrer 1 hora no tempo do jogo)
        degreesPerSecond = 5;
        secondsBetweenHours = 3;

        //isRotating = false;
        dayPhase = 0;

        newDayTime = dayTime;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // if(isRotating)
        // {
        //     if(rotatingDuration > 0)
        //     {
        //         rotatingDuration -= Time.deltaTime;
        //         rotation.x = degreesPerSecond * Time.deltaTime;
        //         sunLight.transform.Rotate(rotation, Space.World);
        //         RenderSettings.ambientLight = ambientColor.Evaluate(rotation.x/360);
        //         RenderSettings.ambientLight = ambientColor.Evaluate(dayTime/24);
        //     }
        //     else
        //     {
        //         rotatingDuration = 0;
        //         isRotating = false;
        //     }
        // }

        // if(secondsBetweenHours > 0)
        // {
        //     secondsBetweenHours -= Time.deltaTime;
        // }
        // else if(secondsBetweenHours <= 0)
        // {
        //     dayTime++;
        //     if(dayTime == 24)
        //     {
        //         dayTime = 0;
        //     }
        //     secondsBetweenHours = 3;
        // }

        // dayTime += Time.deltaTime/3;
        // if(dayTime >= 24)
        // {
        //    dayTime = 0; 
        // }

        // rotation.x = degreesPerSecond * Time.deltaTime;
        // sunLight.transform.Rotate(rotation, Space.World);
        // RenderSettings.ambientLight = ambientColor.Evaluate(dayTime/24);

        if(timeIsPassing)
        {
            rotation.x = degreesPerSecond * Time.deltaTime;
            sunLight.transform.Rotate(rotation, Space.World);
            RenderSettings.ambientLight = ambientColor.Evaluate(dayTime/24);
        }
    }

    // public void UpdateActionTokens(float actionTokens)
    // {
    //     currentActionTokensTaken += actionTokens;
    //     if(currentActionTokensTaken % (currentAreaActionTokens/3) == 0)
    //     {
    //         ControlDayCycle();
    //     }
    // }

    // public void ControlDayCycle()
    // {
    //     if(!isRotating)
    //     {
    //         if(isDay)
    //         {
    //             if(dayPhase == 2)
    //             {
    //                 dayPhase = 0;
    //                 isDay = false;
    //                 degreesPerSecond = 5;
    //                 rotatingDuration = 20;
    //             }
    //             else
    //             {
    //                 dayPhase++;
    //                 degreesPerSecond = 15;
    //                 rotatingDuration = 4;
    //                 Debug.Log(sunLight.transform.rotation.eulerAngles);
    //             }
    //             isRotating = true;
    //         }
    //         else
    //         {
    //             sunLight.transform.rotation = new Quaternion(0.42261827f,0,0,0.906307876f);
    //             isDay = true;
    //         }
    //     }
    // }

    public void AdvanceTime(int hours)
    {
        newDayTime = (dayTime + hours)%24;
        //timeIsPassing = true;
        StartCoroutine(Example(hours));
    }

    IEnumerator Example(int hours)
    {
        Debug.Log("Started Coroutine1 at timestamp : " + Time.time);

        while(hours > 0)
        {
            //PassTime();
            timeIsPassing = !timeIsPassing;
            yield return new WaitForSeconds(1.5f);
            //dayTime = (dayTime + Time.deltaTime)%24;
            dayTime ++;
            Debug.Log(dayTime);
            timeIsPassing = !timeIsPassing;
            //PassTime();
            hours--;
        }

        Debug.Log("Finished Coroutine1 at timestamp : " + Time.time);
    }
}