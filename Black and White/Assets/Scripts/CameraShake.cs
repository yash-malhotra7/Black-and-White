using UnityEngine;

public class CameraShake : MonoBehaviour
{
    GameManager gameManager;

    public float shakeDuration = 0.5f;
    public float shakeMagnitude = 0.1f;

    private float shakeElapsedTime = 0f;
    private bool hasShakeStarted = false;
    private Vector3 originalPos;

    void Start()
    {
        originalPos = transform.localPosition;
        gameManager = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        if (gameManager.playerHasDied && !hasShakeStarted)
        {
            shakeElapsedTime = shakeDuration;
            hasShakeStarted = true;
        }

        if (shakeElapsedTime > 0)
        {
            transform.localPosition = originalPos + Random.insideUnitSphere * shakeMagnitude;

            shakeElapsedTime -= Time.deltaTime;
        }
        else
        {
            shakeElapsedTime = 0f;
            transform.localPosition = originalPos;
            hasShakeStarted = false;
        }
    }
}
