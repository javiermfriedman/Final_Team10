using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
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

      private string sceneName;
      public static string lastLevelDied;

      // Volume Control
      public AudioMixer mixer;
      public static float volumeLevel = 1.0f;

      public void StartGame() {
            SceneManager.LoadScene("Level1");
      }

      public void RestartGame() {
            Time.timeScale = 1f;
            GameHandler_PauseMenu.GameisPaused = false;
            SceneManager.LoadScene("MainMenu");
            playerHealth = StartPlayerHealth;
      }

      public void ReplayLastLevel() {
            Time.timeScale = 1f;
            GameHandler_PauseMenu.GameisPaused = false;
            SceneManager.LoadScene(lastLevelDied);
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

      // SetLevel for Slider in Pause Menu
      public void SetLevel(float sliderValue) {
            mixer.SetFloat("MusicVolume", Mathf.Log10(sliderValue) * 20);
            volumeLevel = sliderValue;
      }
}