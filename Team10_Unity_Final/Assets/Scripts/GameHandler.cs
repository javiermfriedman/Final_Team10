using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameHandler : MonoBehaviour {

    private GameObject player;
    public static int playerHealth = 100;
    public int StartPlayerHealth = 100;
    public GameObject healthText;
    private Animator playerAnimator; // Reference to the Animator
    private PlayerHurt playerHurt;  // Reference to the PlayerHurt script

    public static int gotTokens = 0;
    public GameObject tokensText;

    public bool isDefending = false;

    public static bool stairCaseUnlocked = false;
    //this is a flag check. Add to other scripts: GameHandler.stairCaseUnlocked = true;

    private string sceneName;
    public static string lastLevelDied;  //allows replaying the Level where you died

    [SerializeField] private AudioClip hitSound; // Sound to play when hit
    private AudioSource audioSource;            // AudioSource component

    void Start() {
        player = GameObject.FindWithTag("Player");
        sceneName = SceneManager.GetActiveScene().name;
        playerHealth = StartPlayerHealth;
        updateStatsDisplay();
         playerAnimator = player.GetComponentInChildren<Animator>();
        if (playerAnimator == null) {
        Debug.LogWarning("Animator component not found on the player!");
}
        playerHurt = player.GetComponent<PlayerHurt>();

        // Ensure the AudioSource is attached to this GameObject
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null) {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    public void playerGetTokens(int newTokens) {
        gotTokens += newTokens;
        updateStatsDisplay();
    }

    public void testPrint() {
        Debug.Log("javiCheck");
    }

    public void playerGetHit(int damage) {
        playerHealth -= damage;

        // Play the hit sound
        if (hitSound != null && audioSource != null) {
            audioSource.PlayOneShot(hitSound);
        } else {
            Debug.LogWarning("Hit sound or AudioSource is missing!");
        }
        //play hurt animation
        Debug.Log("Triggering GetHurt animation");
        if (playerHurt != null) {
            Debug.Log("Triggering GetHurt animation");
            playerHurt.playerHit();
        }

        // if (playerAnimator != null) {
        //     Debug.Log("Triggering GetHurt animation");
        //  playerAnimator.SetTrigger("gethurt");
// }

        Debug.Log("Player health: " + playerHealth);
        updateStatsDisplay();

        if (playerHealth <= 0) {
            playerHealth = 0;
            updateStatsDisplay();
            playerDies();
        }
    }

    public void updateStatsDisplay() {
        Text healthTextTemp = healthText.GetComponent<Text>();
        healthTextTemp.text = "Health: " + playerHealth;

        Text tokensTextTemp = tokensText.GetComponent<Text>();
        tokensTextTemp.text = "Fragments: " + gotTokens;
    }

    public void playerDies() {
        lastLevelDied = sceneName;  //allows replaying the Level where you died
        StartCoroutine(DeathPause());
    }

    IEnumerator DeathPause() {
        player.GetComponent<playerMove>().isAlive = false;
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene("DeathScene");
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

    public void LoadLevel1() {
        SceneManager.LoadScene("Scene1");
    }

    public void LoadLevel2() {
        SceneManager.LoadScene("Scene2");
    }

    public void LoadLevel3() {
        SceneManager.LoadScene("Scene3");
    }

    public void LoadLevel4() {
        SceneManager.LoadScene("Scene4");
    }

    public void LoadLevel5() {
        SceneManager.LoadScene("scene5");
    }

    public void LoadLevel6() {
        SceneManager.LoadScene("Scene6");
    }

    public void LoadBossLevel() {
        SceneManager.LoadScene("Boss_Level");
    }

    public void LoadWinScene() {
        SceneManager.LoadScene("Win_scene");
    }
}

