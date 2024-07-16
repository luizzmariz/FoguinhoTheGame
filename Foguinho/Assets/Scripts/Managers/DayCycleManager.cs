using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DayPhase
{
    MORNING,
    AFTERNOON,
    EVENING,
    NIGHT,
}

public class DayCycleManager : MonoBehaviour
{
    [Header("DayCycleManager")]
    public static DayCycleManager instance = null;

    [Header("AmbientColor")]
    public Gradient ambientColor;

    [Header("CycleRotation")]
    // Vector3 rotation = Vector3.zero;
    // public float degreesPerSecond;
    // public float rotatingDuration;
    public bool timeIsPassing;
    public bool isDay;
    public DayPhase dayPhase;
    // public GameObject sunLight;

    [Header("DayTime")]
    public float dayTime;
    public float newDayTime;
    public float secondsBetweenHours;

    [Header("ActionTokens")]
    public int currentActionTokensTaken;

    [Header("Player")]
	public GameObject player;
	public PlayerStateMachine playerStateMachine;

    void Start()
    {
        if (instance == null) {
			instance = this;
		} else if (instance != this) {
			Destroy(gameObject);
		}

        player = GameObject.Find("Player");
		playerStateMachine = player.GetComponent<PlayerStateMachine>();

        currentActionTokensTaken = 0;

        WorldData data = SaveSystem.LoadGame();
        dayPhase = data.currentDayPhase;
        
        CheckCorrectHours();

        // if(sunLight == null)
        // {
        //     sunLight = GameObject.Find("Directional Light"); 
        // }
        //360° (volta completa) / 24h (horas do dia) -> 15 graus por hora

        //hora q o jogo começa = 5:30h -> 5.5f
        //dayTime = 5.5f;
        //graus por segundo (3 segundos para percorrer 1 hora no tempo do jogo)
        // degreesPerSecond = 5;

        //secondsBetweenHours = 3;

        //isRotating = false;

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

        // if(timeIsPassing)
        // {
        //     // rotation.x = degreesPerSecond * Time.deltaTime;
        //     // sunLight.transform.Rotate(rotation, Space.World);
        //     // RenderSettings.ambientLight = ambientColor.Evaluate(dayTime/24);
        // }
    }

    public void UpdateActionTokens(int actionTokens)
    {
        currentActionTokensTaken += actionTokens;

        //Debug.Log(currentActionTokensTaken);
        // Debug.Log(LevelManager.instance.currentLevelPart.actionTokensGap);

        if(currentActionTokensTaken >= LevelManager.instance.currentLevelPart.actionTokensGap)
        {
            currentActionTokensTaken = 0;

            CheckCorrectHours();
        }
    }

    public void CheckCorrectHours()
    {
        if(dayPhase == DayPhase.MORNING)
        {
            AdvanceTimeTo(12);
        }
        else if(dayPhase == DayPhase.AFTERNOON)
        {
            AdvanceTimeTo(18);
        }
        else if(dayPhase == DayPhase.EVENING)
        {
            AdvanceTimeTo(22);
        }
        else if(dayPhase == DayPhase.NIGHT)
        {
            AdvanceTimeTo(5);
        }
    }

    public void SetDayPhase()
    {
        DayPhase oldDayPhase = dayPhase;

        if(dayTime >= 5 && dayTime < 12)
        {
            dayPhase = DayPhase.MORNING;
        }
        else if(dayTime >= 12 && dayTime < 18)
        {
            dayPhase = DayPhase.AFTERNOON;
        }
        else if(dayTime >= 18 && dayTime < 22)
        {
            dayPhase = DayPhase.EVENING;
        }
        else if((dayTime >= 22 && dayTime < 24) || (dayTime >= 0 && dayTime < 5))
        {
            dayPhase = DayPhase.NIGHT;
        }

        if(oldDayPhase != dayPhase)
        {
            LevelManager.instance.currentLevelPart.RefreshLevelPart();
        }
    }

    public void AdvanceTime(int hours)
    {
        newDayTime = (dayTime + hours)%24;

        if(!timeIsPassing)
        {
            StartCoroutine(PassHours(hours));
        }
    }

    public void AdvanceTimeTo(int hours)
    {
        float hoursToAdvance = (hours%24) - dayTime;

        if(hoursToAdvance < 0)
        {
            hoursToAdvance += 24;
        }
        
        newDayTime = hours%24;

        if(!timeIsPassing)
        {
            StartCoroutine(PassHours(hoursToAdvance));
        }
    }

    IEnumerator PassHours(float hours)
    {        
        timeIsPassing = true;

        yield return new WaitForSeconds(0.01f);

        playerStateMachine.ChangeState(playerStateMachine.interactState);

        while(hours > 0)
        {   
            yield return new WaitForSeconds(secondsBetweenHours);

            dayTime = (dayTime+1)%24;

            RenderSettings.ambientLight = ambientColor.Evaluate(dayTime/24);
            //Debug.Log(dayTime);
        
            hours--;
        }

        SetDayPhase();
        
        playerStateMachine.interactState.ExitState();

        timeIsPassing = false;
    }
}