using UnityEngine;

public class AudioManager : MonoBehaviour {
  public static AudioManager instance;

  [SerializeField] AudioSource audioSource;
  [SerializeField] AudioClip snip;

  public AudioManager() {
    instance = this;
  }

  public static AudioManager GetInstance() {
    if (instance == null) {
      instance = new AudioManager();
    }
    return instance;
  }

  public void PlaySnip() {
    audioSource.PlayOneShot(snip);
  }
}
