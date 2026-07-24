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
    LoadWireMinigame();
  }

  public static void LoadStartScreen() {
    SceneManager.LoadScene("Start Screen", LoadSceneMode.Single);
  }

  public static void LoadWireMinigame() {
    SceneManager.LoadScene("Wire Minigame", LoadSceneMode.Single);
  }
}
