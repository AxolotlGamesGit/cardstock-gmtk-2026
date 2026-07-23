using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Wire : MonoBehaviour, IPointerClickHandler {
  [HideInInspector] public bool ShouldCut;
  public UnityEvent OnCut;

  [SerializeField] string wireName;

  private bool isCut = false;

  public void OnPointerClick(PointerEventData eventData) {
    Debug.Log("Wire cut: " + wireName);
    isCut = true;
    OnCut?.Invoke();
  }

  public string GetName() {
    return wireName;
  }

  public bool IsCut() {
    return isCut;
  }
}
