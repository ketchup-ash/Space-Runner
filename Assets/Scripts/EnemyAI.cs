using UnityEngine;

public class EnemyAI : MonoBehaviour {

    public float moveSpeed;
    public int health;

    public GameObject destroyEffect;
    
    private GameObject player;
    public int scoreToGive = 1;

    private void Awake() {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update() {
        transform.rotation = Quaternion.Euler(0f, 0f, EnemyToPlayerAngle());
        if (Vector3.Distance(player.transform.position, transform.position) > 0.2f)
            transform.position += transform.up * moveSpeed * Time.deltaTime;

        if (health <= 0) {
            player.GetComponent<Player>().score += scoreToGive;
            Die();
        }
        if (Vector3.Distance(player.transform.position, transform.position) > 15f) {
            Destroy(gameObject);
        }
    }

    float EnemyToPlayerAngle() {
        float X = player.transform.position.x - transform.position.x;
        float Y = player.transform.position.y - transform.position.y;
        return Mathf.Atan2(Y, X) * Mathf.Rad2Deg - 90f;
    }

    public void TakeDamage(int damage) {
        health -= damage;
    } 

    void Die() {
        GameObject effect = Instantiate(destroyEffect, transform.position, transform.rotation) as GameObject;
        ParticleSystem parts = effect.GetComponent<ParticleSystem>();
        float totalDuration = parts.main.duration + parts.main.startDelayMultiplier;
        Destroy(effect, totalDuration);
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            Die();
        }
    }

}