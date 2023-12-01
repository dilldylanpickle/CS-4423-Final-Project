using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 100f;
    public float jumpForce = 15f;
    public GameObject BulletPrefab;
    public float bulletSpeed = 10f;
    public AudioClip shootingSound;

    private bool isJumping = false;
    private Rigidbody2D rb;
    private float lastMoveX = 1;
    public GameObject pauseMenu;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (pauseMenu != null)
            pauseMenu.SetActive(false);
    }

    private void Update()
    {
        float moveX = Input.GetAxis("Horizontal");
        if (moveX != 0)
        {
            lastMoveX = moveX;
        }
        rb.velocity = new Vector2(moveX * speed, rb.velocity.y);

        if (Input.GetKeyDown(KeyCode.W) && !isJumping)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            isJumping = true;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePauseMenu();
        }
    }

    private void TogglePauseMenu()
    {
        if (pauseMenu != null)
        {
            bool isCurrentlyActive = pauseMenu.activeSelf;
            pauseMenu.SetActive(!isCurrentlyActive);

            Time.timeScale = isCurrentlyActive ? 1f : 0f;
        }
    }

    public void Spin()
    {
        StartCoroutine(SpinCoroutine());
    }

    private IEnumerator SpinCoroutine()
    {
        float duration = 2.0f;
        float elapsedTime = 0;
        float rotationSpeed = 360.0f;

        while (elapsedTime < duration)
        {
            float rotationAmount = rotationSpeed * Time.deltaTime;
            transform.Rotate(0, 0, rotationAmount);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }

    void Shoot()
    {
        Vector2 shootDirection = new Vector2(lastMoveX, 0).normalized;
        GameObject bullet = Instantiate(BulletPrefab, transform.position + (Vector3)shootDirection, Quaternion.identity);
        Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
        bulletRb.velocity = shootDirection * bulletSpeed;

        AudioSource audio = GetComponent<AudioSource>();
        if (audio != null && shootingSound != null)
        {
            audio.PlayOneShot(shootingSound);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Floor") || collision.gameObject.CompareTag("SafeFloor"))
        {
            isJumping = false;
        }
    }
}
