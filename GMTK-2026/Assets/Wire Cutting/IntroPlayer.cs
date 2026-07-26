using System.Collections;
using UnityEngine;

public class IntroPlayer : MonoBehaviour {
  [SerializeField] AudioSource audioSource;
  [SerializeField] AudioClip intro;

  private void Start() {
    StartCoroutine(PlayIntro());
  }

  private IEnumerator PlayIntro() {
    audioSource.PlayOneShot(intro);
    yield return new WaitForSeconds(intro.length);
    SceneLoader.LoadWireMinigame();
  }
}
