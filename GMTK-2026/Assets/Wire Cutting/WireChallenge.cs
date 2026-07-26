using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class WireChallenge : MonoBehaviour {
  public UnityEvent OnFail;
  public UnityEvent OnComplete;
  public UnityEvent OnOutOfOrder;

  [SerializeField] DisplayManager[] wires;
  [SerializeField] AudioSource audioSource;
  [SerializeField] AudioClip startSound;
  [SerializeField] bool playChildSound;
  [SerializeField] AudioClip endSound;
  [SerializeField] int wiresToCut = 1;

  private bool isActive = false;

  private void Awake() {
    if (wires.Length < wiresToCut) {
      Debug.LogError($"Not enough wires, currently: {wires.Length}, should be at least: {wiresToCut}");
    }

    OnComplete.AddListener(() => audioSource.Stop());
    OnComplete.AddListener(() => Debug.Log($"Challenge completed: {name}"));
  }

  private void Start() {
    wires = wires.OrderBy(x => UnityEngine.Random.Range(0f, 1f)).ToArray();

    foreach (DisplayManager wire in wires) {
      wire.ShouldCut = true;
      wire.OnCut.AddListener(() => OnOutOfOrder?.Invoke());
    }
  }

  private void Update() {
    if (IsComplete()  &&  isActive) {
      isActive = false;
      OnComplete?.Invoke();
    }
  }

  public void StartChallenge() {
    Debug.Log($"Challenge started: {name}");

    OnOutOfOrder.RemoveAllListeners();
    isActive = true;

    for (int i = 0; i < wiresToCut; i++) {
      wires[i].ShouldCut = true;
    }
    for (int i = wiresToCut; i < wires.Length; i++) {
      wires[i].ShouldCut = false;
    }
    foreach (DisplayManager wire in wires) {
      wire.OnCut.RemoveListener(() => OnOutOfOrder?.Invoke());
      if (!wire.ShouldCut) {
        wire.OnCut.AddListener(() => OnFail?.Invoke());
      }
    }

    StartCoroutine(PlayChallengeSounds());
  }

  private IEnumerator PlayChallengeSounds() {
    if (startSound != null) {
      audioSource.PlayOneShot(startSound);
      yield return new WaitForSeconds(startSound.length);
    }

    if (playChildSound  && wires[0].wireSound != null) {
      audioSource.PlayOneShot(wires[0].wireSound);
      yield return new WaitForSeconds(wires[0].wireSound.length);
    }

    if (endSound != null) {
      audioSource.PlayOneShot(endSound);
      yield return new WaitForSeconds(endSound.length);
    }
  }

  private bool IsComplete() {
    foreach (DisplayManager wire in wires) {
      if (wire.ShouldCut ^ wire.IsCut()) {
        return false;
      }
    }
    return true;
  }
}
