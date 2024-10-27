using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameHandler : MonoBehaviour {

      private GameObject player;
      public static int playerHealth = 100;
      public int StartPlayerHealth = 100;
      public GameObject healthText;

      public static int gotTokens = 0;
      public GameObject tokensText;

      public bool isDefending = false;

      public static bool stairCaseUnlocked = false;
      //this is a flag check. Add to other scripts: GameHandler.stairCaseUnlocked = true;

      private string sceneName;
      public static string lastLevelDied;  //allows replaying the Level where you died

      public void StartGame() {
            SceneManager.LoadScene("Level1");
      }

      // Return to MainMenu
      public void RestartGame() {
            Time.timeScale = 1f;
            GameHandler_PauseMenu.GameisPaused = false;
            SceneManager.LoadScene("MainMenu");
             // Reset all static variables here, for new games:
            playerHealth = StartPlayerHealth;
      }

      // Replay the Level where you died
      public void ReplayLastLevel() {
            Time.timeScale = 1f;
            GameHandler_PauseMenu.GameisPaused = false;
            SceneManager.LoadScene(lastLevelDied);
             // Reset all static variables here, for new games:
            playerHealth = StartPlayerHealth;
      }

      public void QuitGame() {
                #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
                #else
                Application.Quit();
                #endif
      }

      public void Credits() {
            SceneManager.LoadScene("Credits");
      }
}