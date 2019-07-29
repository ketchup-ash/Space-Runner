using UnityEngine;
using UnityEngine.Assertions;

public class Starfield : MonoBehaviour {

    public int maxStars = 100;
    public float starSize = 0.1f;
    public float starSizeRange = 0.5f;
    public float fieldWidth = 20f;
    public float fieldHeight = 25f;

    float xOffset, yOffset;

    public float parallaxFactor = 0f;

    public bool colorize = false;

    Transform theCamera;

    ParticleSystem particles;
    ParticleSystem.Particle[] stars;

    private void Start() {
        theCamera = Camera.main.transform;
    }

    private void Awake() {
        stars = new ParticleSystem.Particle[maxStars];
        particles = GetComponent<ParticleSystem>();

        Assert.IsNotNull(particles, "Particle system is missing from object!");

        xOffset = fieldWidth * 0.5f;
        yOffset = fieldHeight * 0.5f;

        for (int i = 0; i < maxStars; i++) {
            float randSize = Random.Range(starSizeRange, starSizeRange + 1f);
            float scaledColor = (true == colorize) ? randSize - starSizeRange : 1f;

            stars[i].position = GetRandomInRectangle(fieldWidth, fieldHeight) + transform.position;
            stars[i].startSize = starSize * randSize;
            stars[i].startColor = new Color(1f, scaledColor, scaledColor, 1f);
        }

        particles.SetParticles(stars, stars.Length);
    }

    private void Update() {

        ParallaxEffect();

        ScrollEffect();
    }

    Vector3 GetRandomInRectangle(float width, float height) {
        float x = Random.Range(0, width);
        float y = Random.Range(0, height);
        return new Vector3(x - xOffset, y - yOffset, 0);
    }

    void ScrollEffect() {
        for (int i = 0; i < maxStars; i++) {
            Vector3 pos = stars[i].position + transform.position;

            if (pos.x < (theCamera.position.x - xOffset)) {
                pos.x += fieldWidth;
            } else if (pos.x > (theCamera.position.x + xOffset)) {
                pos.x -= fieldWidth;
            }

            if (pos.y < (theCamera.position.y - yOffset)) {
                pos.y += fieldHeight;
            } else if (pos.y > (theCamera.position.y + yOffset)) {
                pos.y -= fieldHeight;
            }

            stars[i].position = pos - transform.position;
        }
        particles.SetParticles(stars, stars.Length);
    }

    void ParallaxEffect() {
        Vector3 newPos = theCamera.position * parallaxFactor;               
        newPos.z = 0;                    
        transform.position = newPos;
    }

}
