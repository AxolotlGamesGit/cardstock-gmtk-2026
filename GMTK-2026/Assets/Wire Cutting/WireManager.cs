using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class WireManager : MonoBehaviour {
  public UnityEvent OnGameOver;
  public UnityEvent OnWin;

  [SerializeField] WireChallenge[] pinkWireChallenges;
  [SerializeField] WireChallenge[] randomWireChallenges;
  [SerializeField] WireChallenge[] endWireChallenges;

  private List<WireChallenge> wireChallenges;

  private void Awake() {
    OnGameOver.AddListener(() => SceneLoader.LoadLoseScreen());
    OnWin.AddListener(() => SceneLoader.LoadWinScreen());

    wireChallenges.Add(UnityEngine.Random.Range(0, 1) < 0.5 ? pinkWireChallenges[0] : pinkWireChallenges[1]);
    randomWireChallenges = randomWireChallenges.OrderBy(x => UnityEngine.Random.Range(0f, 1f)).ToArray();
    wireChallenges.AddRange(randomWireChallenges);
    wireChallenges.AddRange(endWireChallenges);

    if (wireChallenges.Count == 0) {
      Debug.LogError("No challenges found");
    }
  }

  private void Start() {
    if (wireChallenges.Count > 1) {

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
}
