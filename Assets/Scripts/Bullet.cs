using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    public float bulletSpeed;
    public float lifeTime;
    public float distance;
    public int damage;

    public GameObject destroyEffect;

    public LayerMask whatIsSolid;

    private void Start() {
        Invoke("DestroyBullet", lifeTime);
    }
    private void Update() {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.up, distance, whatIsSolid);
        if (hitInfo.collider != null) {
            if (hitInfo.collider.CompareTag("Enemy")) {
                hitInfo.collider.GetComponent<EnemyAI>().TakeDamage(damage);
            }
            DestroyBullet();
        }

        transform.position += transform.up * bulletSpeed * Time.deltaTime;
    }

    void DestroyBullet() {
        GameObject effect = Instantiate(destroyEffect, transform.position, transform.rotation) as GameObject;
        ParticleSystem parts = effect.GetComponent<ParticleSystem>();
        float totalDuration = parts.main.duration + parts.main.startDelayMultiplier;
        Destroy(effect, totalDuration);
        Destroy(gameObject);
    }

}