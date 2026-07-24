using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class Timer : MonoBehaviour {
  public UnityEvent OnTimeUp;

  [SerializeField] TextMeshProUGUI tmp;
  [SerializeField] float startingSeconds = 30;

  private float secondsLeft = 0;
  private bool isRunning = false;

  private void Awake() {
    secondsLeft = startingSeconds;
    if (tmp == null) {
      tmp = GetComponent<TextMeshProUGUI>();
    }
    if (tmp == null) {
      Debug.LogError("No text mesh pro found on timer");
    }
    OnTimeUp.AddListener(() => isRunning = false);
  }

  private void Start() {
    StartMinigame();
  }

  private void Update() {
    if (isRunning) {
      secondsLeft -= Time.deltaTime;
    }
    if (secondsLeft < 0) {
      OnTimeUp?.Invoke();
    }
    if (Math.Round(secondsLeft) < 60) {
      tmp.text = Math.Round(secondsLeft).ToString();
    }
    else {
      TimeSpan _timespan = TimeSpan.FromSeconds(Math.Round(secondsLeft));
      tmp.text = _timespan.Minutes + ":" + _timespan.Seconds.ToString("D2");
    }
  }

  public void StartMinigame() {
    isRunning = true;
  }
}
