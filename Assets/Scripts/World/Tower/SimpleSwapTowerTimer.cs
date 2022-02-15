using System.Collections;
using UnityEngine;
using System;

public class SimpleSwapTowerTimer : MonoBehaviour
{
    [SerializeField] private float delayTime;

    public event Action<float> OnTimerChanged;
    public event Action<bool> OnCanSwap;

    private float _currentTime;
    private bool _canSwap = true;

    public float CurrentSwapTime => _currentTime;
    public bool CanSwap => _canSwap;

    public void StartTimer()
    {
        StartCoroutine(Timer(delayTime));
    }

    private IEnumerator Timer(float time)
    {
        _canSwap = false;
        OnCanSwap?.Invoke(false);
        for (float i = time; 0f <= _currentTime; i -= Time.deltaTime)
        {
            _currentTime = i;
            OnTimerChanged?.Invoke(_currentTime);
            yield return new WaitForEndOfFrame();
        }
        _currentTime = 0;
        _canSwap = true;
        OnCanSwap?.Invoke(true);
    }

   
}

