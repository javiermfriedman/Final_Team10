using System.Collections;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    private GameHandler gameHandlerObj;
    public int damage = 1;
    public float speed = 10f;
    private Transform playerTrans;
    private Vector2 target;
    public GameObject hitEffectAnim;
    public float SelfDestructTime = 2.0f;

    void Start()
    {
        // Get the player's position
        playerTrans = GameObject.FindGameObjectWithTag("Player").transform;
        target = new Vector2(playerTrans.position.x, playerTrans.position.y);

        if (gameHandlerObj == null)
        {
            gameHandlerObj = GameObject.FindWithTag("GameHandler").GetComponent<GameHandler>();
        }

        // Extend trajectory beyond the player's position
        Vector2 startPos = transform.position;
        float distance = Vector2.Distance(startPos, target);
        distance = distance * 10f; // Extend target distance
        Vector2 difference = target - startPos;
        difference = difference.normalized * distance;
        target = startPos + difference;
    }

    void Update()
    {
        // Move towards the target position
        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
    }


    public void OnCollisionEnter2D(Collision2D other){


        if (other.gameObject.tag != "bossWitch") {
              Destroy(gameObject); 
        }
    }

    IEnumerator selfDestruct()
    {
        // Destroy the projectile after a set time
        yield return new WaitForSeconds(SelfDestructTime);
        Destroy(gameObject);
    }
}
