using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class WireManager : MonoBehaviour {
  public UnityEvent OnGameOver;
  public UnityEvent OnWin;

  [SerializeField] WireChallenge[] pinkWireChallenges;
  [SerializeField] WireChallenge[] randomWireChallenges;
  [SerializeField] WireChallenge[] endWireChallenges;
  [SerializeField] DisplayManager timer;

  private List<WireChallenge> wireChallenges = new List<WireChallenge>();
  private int challengeIndex = 0;

  private void Awake() {
    OnGameOver.AddListener(() => SceneLoader.LoadLoseScreen());
    OnWin.AddListener(() => SceneLoader.LoadWinScreen());
    
    if (Random.Range(0f, 1f) < 0.5f) {
      wireChallenges.Add(pinkWireChallenges[0]);
      pinkWireChallenges[1].OnOutOfOrder.AddListener(() => OnGameOver?.Invoke());
    }
    else {
      wireChallenges.Add(pinkWireChallenges[1]);
      pinkWireChallenges[0].OnOutOfOrder.AddListener(() => OnGameOver?.Invoke());
    }

    randomWireChallenges = randomWireChallenges.OrderBy(x => UnityEngine.Random.Range(0f, 1f)).ToArray();
    wireChallenges.AddRange(randomWireChallenges);

    wireChallenges.AddRange(endWireChallenges);

    Debug.Log($"There are {wireChallenges.Count()} challenges");

    if (wireChallenges.Count == 0) {
      Debug.LogError("No challenges found");
    }
  }

  private void Start() {
    timer.OnTimeUp.AddListener(() => OnGameOver?.Invoke());

    if (wireChallenges.Count > 1) {
      for (int i = 0; i < wireChallenges.Count - 1; i++) {
        wireChallenges[i].OnComplete.AddListener(NextChallenge);
      }
    }

    foreach (WireChallenge challenge in wireChallenges) {
      challenge.OnFail.AddListener(() => OnGameOver?.Invoke());
      challenge.OnOutOfOrder.AddListener(() => OnGameOver?.Invoke());
    }

    wireChallenges[wireChallenges.Count - 1].OnComplete.AddListener(() => OnWin?.Invoke());

    StartMinigame();
  }

  public void StartMinigame() {
    wireChallenges[0].StartChallenge();
  }

  private void NextChallenge() {
    wireChallenges[challengeIndex].OnOutOfOrder.AddListener(() => OnGameOver?.Invoke());

    challengeIndex++;

    wireChallenges[challengeIndex].StartChallenge();
  }
}
