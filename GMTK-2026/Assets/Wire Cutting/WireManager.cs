using System.Linq;
using UnityEngine;

public class WireManager : MonoBehaviour {
  [SerializeField] Wire[] wires;

  private int idealWireCount = 4;
  private Wire firstWire;
  private Wire unsureWireOne;
  private Wire unsureWireTwo;

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
  }

  public void StartMinigame() {
    Debug.Log($"Cut the {wires[0]} wire, and the {wires[2]}/{wires[3]} wire, might have to guess on that one");
  }
}
