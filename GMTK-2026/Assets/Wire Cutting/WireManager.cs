using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class WireManager : MonoBehaviour {
  public UnityEvent OnGameOver;
  public UnityEvent OnWin;

  [SerializeField] Wire[] wires;

  private int idealWireCount = 4;

  private void Awake() {
    OnGameOver.AddListener(() => SceneLoader.LoadLoseScreen());
    OnWin.AddListener(() => SceneLoader.LoadWinScreen());
  }

  private void Start() {
    if (wires.Length != idealWireCount) {
      Debug.LogError($"Not the right amount of wires, currently: {wires.Length}, should be: {idealWireCount}");
    }
    wires = wires.OrderBy(x => UnityEngine.Random.Range(0f,1f)).ToArray();

    wires[0].ShouldCut = true;
    wires[1].ShouldCut = false;
    if (UnityEngine.Random.Range(0, 1) < 0.5) {
      wires[2].ShouldCut = true;
      wires[3].ShouldCut = false;
    }
    else {
      wires[2].ShouldCut = false;
      wires[3].ShouldCut = true;
    }

    foreach (Wire wire in wires) {
      if (!wire.ShouldCut) {
        wire.OnCut.AddListener(() => OnGameOver?.Invoke());
      }
      else {
        wire.OnCut.AddListener(() => {if (IsWon()) { OnWin?.Invoke(); }});
      }
    }

    StartMinigame();
  }

  public void StartMinigame() {
    Debug.Log($"Cut the {wires[0].GetName()} wire, and the {wires[2].GetName()}/{wires[3].GetName()} wire, might have to guess on that one");
  }

  private bool IsWon() {
    foreach (Wire wire in wires) {
      if (wire.ShouldCut ^ wire.IsCut()) {
        return false;
      }
    }
    return true;
  }
}
