using UnityEngine;
using UnityEngine.SceneManagement;

public class Win : MonoBehaviour
{
    [SerializeField] AudioSource winSound;

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("Win"))
        {
            winSound.Play();

            Invoke("ChangeScene", 1f);
        }
    }

    void ChangeScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
