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

    void Start(){
        player = GameObject.FindWithTag("Player");
        sceneName = SceneManager.GetActiveScene().name;
        playerHealth = StartPlayerHealth;
        updateStatsDisplay();
    }

    public void playerGetTokens(int newTokens){
        gotTokens += newTokens;
        updateStatsDisplay();
    }

    public void playerGetHit(int damage){
        // Debug.Log("in playerGetHit");
        // Debug.Log("Damage currently: " + damage);
        // Debug.Log("Is defending: " + isDefending);
        if (!isDefending){
            // Debug.Log("In is defending");
            // Debug.Log("The damage passed in: " + damage);
            playerHealth -= damage;
            // Debug.Log("The playerHealth after damage sub: " + playerHealth);
            if (playerHealth >= 0){
                updateStatsDisplay();
            }
            if (damage > 0){
                player.GetComponent<PlayerHurt>().playerHit();  //play GetHit animation
            }
        }

        // What does this part mean?
        if (playerHealth > StartPlayerHealth){
            playerHealth = StartPlayerHealth;
            updateStatsDisplay();
        }

        if (playerHealth <= 0){
            playerHealth = 0;
            updateStatsDisplay();
            playerDies();
        }
    }

    public void updateStatsDisplay(){
        Text healthTextTemp = healthText.GetComponent<Text>();
        healthTextTemp.text = "HEALTH: " + playerHealth;

        Text tokensTextTemp = tokensText.GetComponent<Text>();
        Debug.Log("The frag count: " + gotTokens);
        tokensTextTemp.text = "FRAGMENTS: " + gotTokens;
    }

    public void playerDies(){
        player.GetComponent<PlayerHurt>().playerDead();  //play Death animation
        lastLevelDied = sceneName;  //allows replaying the Level where you died
        StartCoroutine(DeathPause());
    }

    IEnumerator DeathPause(){
        player.GetComponent<playerMove>().isAlive = false;
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene("EndLose");
    }

    public void StartGame() {
        SceneManager.LoadScene("Tutorial");
    }

    // Return to MainMenu
    public void RestartGame() {
        Time.timeScale = 1f;
        playerHealth = StartPlayerHealth;  // Reset health for new games
        SceneManager.LoadScene("MainMenu");
    }

    // Replay the Level where you died
    public void ReplayLastLevel() {
        Time.timeScale = 1f;
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

    public void LoadLevel2() { 
        SceneManager.LoadScene("Scene2");
    }

    public void LoadLevel3() { 
        SceneManager.LoadScene("Scene3");
    }

}
