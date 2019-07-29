using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour {

    public GameObject bullet;
    public Transform shotPoint;
    public Transform cam;
    public Joystick joystick;
    public Joystick joystick1;
    public TextMeshProUGUI textPro;
    public Image[] healthImage;
    public TextMeshProUGUI highscore;
    public GameObject destroyEffect;
    public Canvas UI;
    public Canvas DieUI;
    public TextMeshProUGUI deathScore;

    Vector3 movement;

    public int health;
    public int score;

    public float moveSpeed;
    public float rotationOffset;

    public float timeBtwShots;
    public float startTimeBtwShots;

    float angle;

    public Vector3 offset;

    private void Start() {
        score = 0;
        angle = 90f;
        highscore.text = PlayerPrefs.GetInt("Highscore", 0).ToString();
        Time.timeScale = 1f;
    }

    private void Update() {
        cam.position = transform.position + offset;

        RotatePlayerJoystick();

        movement = new Vector3(joystick.Horizontal, joystick.Vertical, 0f);

        if (movement.magnitude > 1)
            movement.Normalize();

        transform.position += movement * Time.deltaTime * moveSpeed;

        Shoot();

        HealthBar();

        if (health <= 0) {
            Die();
        }

        textPro.text = score.ToString();
    }

    void Shoot() {
        if (timeBtwShots <= 0) {
            if (Input.GetMouseButton(0)) {
                Instantiate(bullet, shotPoint.position, transform.rotation);
                timeBtwShots = startTimeBtwShots;
            }
        } else
            timeBtwShots -= Time.deltaTime;
    }

    void RotatePlayerJoystick() {
        Vector3 input = new Vector3(joystick1.Horizontal, joystick1.Vertical, 0f);
        if (input != new Vector3(0f, 0f, 0f)) {
            angle = Mathf.Atan2(input.y, input.x) * Mathf.Rad2Deg;
        }
        transform.rotation = Quaternion.Euler(0f, 0f, angle + rotationOffset);
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Enemy")) {
            health -= 1;
        }
    }

    void Die() {
        SetScore();
        UI.gameObject.SetActive(false);
        DieUI.gameObject.SetActive(true);
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        deathScore.text = score.ToString();
        Time.timeScale = 0f;
    }

    void HealthBar() {
        for (int i = 0; i < healthImage.Length; i++) {
            if (i < health)   
                healthImage[i].enabled = true;
            else
                healthImage[i].enabled = false;
        }
    }

    public void SetScore() {
        if (score > PlayerPrefs.GetInt("Highscore", 0)) {
            PlayerPrefs.SetInt("Highscore", score);
            highscore.text = score.ToString();
        }
    }

}