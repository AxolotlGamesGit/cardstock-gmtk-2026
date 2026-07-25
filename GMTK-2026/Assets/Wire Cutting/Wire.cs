using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Wire : MonoBehaviour, IPointerClickHandler {
  [HideInInspector] public bool ShouldCut = false;
  public UnityEvent OnCut;
  public AudioClip wireSound;

  [SerializeField] string wireName;

  private bool isCut = false;

  private void Awake() {
    OnCut.AddListener(() => { Debug.Log("Wire cut: " + wireName);
                              isCut = true; });
  }

  public void OnPointerClick(PointerEventData eventData) {
    OnCut?.Invoke();
  }

  public string GetName() {
    return wireName;
  }

  public bool IsCut() {
    return isCut;
  }
}
