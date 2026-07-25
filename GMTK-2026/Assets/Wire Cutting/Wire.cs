using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

[RequireComponent(typeof(SpriteRenderer))]
public class Wire : MonoBehaviour, IPointerClickHandler {
  [HideInInspector] public bool ShouldCut = false;
  public UnityEvent OnCut;
  public AudioClip wireSound;

  [SerializeField] string wireName;
  [SerializeField] SpriteRenderer spriteRenderer;

  private bool isCut = false;

  private void Awake() {
    OnCut.AddListener(DefaultOnCut);
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

  private void DefaultOnCut() {
    Debug.Log("Wire cut: " + wireName);
    isCut = true;
    GetComponent<SpriteRenderer>().enabled = false;
  }
}
