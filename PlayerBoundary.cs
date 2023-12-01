using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerBoundaries : MonoBehaviour
{
    public AudioClip deathSound;
    private bool deathSoundPlayed = false;
    private PlayerMovement playerMovementScript;

    private void Start()
    {
        playerMovementScript = GetComponent<PlayerMovement>();
    }

    void Update()
    {
        Vector2 viewportPosition = Camera.main.WorldToViewportPoint(transform.position);

        if (viewportPosition.x < 0 || viewportPosition.x > 1 || viewportPosition.y < 0 || viewportPosition.y > 1)
        {
            DieWithDelay();
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            DieWithDelay();
        }
    }

    void DieWithDelay()
    {
        if (!deathSoundPlayed)
        {
            AudioSource audio = GetComponent<AudioSource>();
            if (audio != null && deathSound != null)
            {
                audio.PlayOneShot(deathSound);
                deathSoundPlayed = true;
            }
        }

        if (playerMovementScript != null)
        {
            playerMovementScript.Spin();
            playerMovementScript.enabled = false;
        }

        float delayTime = 2.0f;
        StartCoroutine(DelayedSceneChange("MainMenu", delayTime));
    }


    IEnumerator DelayedSceneChange(string sceneName, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);

        SceneManager.LoadScene(sceneName);
    }
}
