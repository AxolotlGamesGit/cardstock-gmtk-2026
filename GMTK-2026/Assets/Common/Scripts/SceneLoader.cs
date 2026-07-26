using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour {
  public static void LoadLoseScreen() {
    SceneManager.LoadScene("Lose Screen", LoadSceneMode.Single);
  }

  public static void LoadWinScreen() {
    SceneManager.LoadScene("Win Screen", LoadSceneMode.Single);
  }

  public static void StartGame() {
    SceneManager.LoadScene("Intro", LoadSceneMode.Single);
  }

  public static void LoadStartScreen() {
    SceneManager.LoadScene("Start Screen", LoadSceneMode.Single);
  }

  public static void LoadWireMinigame() {
    SceneManager.LoadScene("Wire Minigame", LoadSceneMode.Single);
  }
}
