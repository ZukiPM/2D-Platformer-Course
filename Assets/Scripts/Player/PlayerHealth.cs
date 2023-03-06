using UnityEngine;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] GameObject playerGraphics;
    //RESPAWN
    [SerializeField] Transform respawnPosition;

    //UI
    [SerializeField] GameObject gameOverScreen;
    [SerializeField] TMP_Text triesCountText;
    int triesCount = 0;
    [SerializeField] Timer timerGO;

    //SOUNDS
    [SerializeField] AudioSource deathSound;

    [SerializeField] ParticleSystem deathParticles;

    private void Start() 
    {
        gameOverScreen.SetActive(false);

        triesCount = 0;
        triesCountText.text = triesCount.ToString();
    }

    void Death()
    {
        playerGraphics.SetActive(false);

        GetComponent<PlayerController>().DeathChanges();

        //SPAWN UI FOR GAME OVER
        gameOverScreen.SetActive(true);

        deathSound.Play();

        deathParticles.Play();
    }

    public void Respawn()
    {
        //SETS RESPAWN POSITION
        transform.position = respawnPosition.position;

        //DESPAWN GAME OVER UI
        gameOverScreen.SetActive(false);

        //TELLS THE CONTROLLER TO RESET SPEED AND FORCES BEFORE SPAWNING AGAIN
        GetComponent<PlayerController>().RespawnChanges();
        //SETS THE PLAYER GRAPHICS ACTIVE AGAIN
        playerGraphics.SetActive(true);

        triesCount++;
        triesCountText.text = triesCount.ToString();
        timerGO.RestartTimer();

        deathParticles.Stop();
    }

    private void OnCollisionEnter2D(Collision2D col) 
    {
        if(col.gameObject.CompareTag("Enemy"))
        {
            Death();
        }
    }
}
