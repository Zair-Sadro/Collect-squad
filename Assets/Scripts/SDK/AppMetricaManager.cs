using UnityEngine;

public class AppMetricaManager : MonoBehaviour
{
    [SerializeField] private int _currentArena;

    private void Start()
    {
        LevelStartEvent();
        GameController.OnLevelFinish += LevelFinishEvent;
    }

    private void LevelStartEvent()
    {
        AppMetrica.Instance.SendEventsBuffer();
        AppMetrica.Instance.ReportEvent("level_start " + _currentArena);
        Debug.Log("AppMetrica : level_start " + _currentArena);
    }

    private void LevelFinishEvent()
    {
        AppMetrica.Instance.SendEventsBuffer();
        AppMetrica.Instance.ReportEvent("level_finish " + _currentArena);
        Debug.Log("AppMetrica : level_finish " + _currentArena);
    }

}
