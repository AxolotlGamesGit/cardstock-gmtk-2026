using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Wire : MonoBehaviour, IPointerClickHandler {
  [HideInInspector] public bool ShouldCut;
  public UnityEvent OnCut;

  [SerializeField] string wireName;

  public void OnPointerClick(PointerEventData eventData) {
    Debug.Log("Wire cut: " + name);
    OnCut?.Invoke();
  }
}
