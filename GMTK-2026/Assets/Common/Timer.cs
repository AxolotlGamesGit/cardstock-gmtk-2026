using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

[RequireComponent(typeof(TextMeshProUGUI))]
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
    tmp.text = Math.Round(secondsLeft).ToString();
  }

  public void StartMinigame() {
    isRunning = true;
  }
}
