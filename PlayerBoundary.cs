using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerBoundaries : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Die();
        }
    }

    void Die()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
