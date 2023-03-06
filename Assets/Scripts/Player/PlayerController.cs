using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    BoxCollider2D boxCol;

    //RUN
    [SerializeField] float moveSpeed = 1f;
    Vector2 movement;
    //JUMP
    [SerializeField] float jumpForce = 10f;
    [SerializeField] LayerMask platformMask;

    //SOUNDS
    [SerializeField] AudioSource powerUpSound;
    [SerializeField] AudioSource stepsSound;

    //POWERUPS
    List<GameObject> powerUpsUsed = new List<GameObject>();

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        boxCol = GetComponent<BoxCollider2D>();
    }

    private void Start() {
        tempGravity = rb.gravityScale;

        UnpauseGame();
    }

    void Update()
    {
        if(!GameManager.singleton.isPaused)
        {
            UnpauseGame();

            //MOVEMENT
            movement.x = Input.GetAxisRaw("Horizontal");

            //JUMP
            if(CheckGround() && Input.GetKeyDown(KeyCode.Space))
            {
                Jump();
            }

            PlayStepsSound();
        }
        else
        {
            PausedGame();
        }
    }

    void FixedUpdate()
    {
        //rb.velocity = new Vector2(movement.x * moveSpeed, rb.position.y);  
        rb.AddForce(new Vector2(movement.x * moveSpeed,0), ForceMode2D.Impulse);
    }

    public bool CheckGround()
    {
        RaycastHit2D hit = Physics2D.BoxCast(boxCol.bounds.center, boxCol.bounds.size, 0f, Vector2.down, 0.1f, platformMask);
        //Debug.Log(hit.collider);
        return hit.collider != null;
    }

    void Jump()
    {
        //rb.velocity = new Vector2(rb.position.x,jumpForce);
        rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
    }

    float tempSpeed;
    float tempJumpForce;
    float tempGravity;
    public void DeathChanges()
    {
        tempSpeed = moveSpeed;
        moveSpeed = 0;
        tempJumpForce = jumpForce;
        jumpForce = 0;

        rb.gravityScale = 0;
        boxCol.enabled = false;
    }

    public void RespawnChanges()
    {
        moveSpeed = tempSpeed;
        jumpForce = tempJumpForce;
        rb.gravityScale = tempGravity;
        boxCol.enabled = true;

        //RESTORE POWER UPS
        GameObject[] PUUarray = powerUpsUsed.ToArray();

        if(PUUarray.Length > 0)
        {
            for(int i = 0; i < PUUarray.Length; i++)
            {
                PUUarray[i].SetActive(true);
            }
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("Speed")){
            SpeedPowerUp();
            other.gameObject.SetActive(false);

            powerUpsUsed.Add(other.gameObject);
        }

        if(other.gameObject.CompareTag("Win"))
        {
            moveSpeed = 0;
            jumpForce = 0;
        }
    }

    //POWER UPS
    void SpeedPowerUp()
    {
        moveSpeed += 0.5f;
        powerUpSound.Play();
    }

    void PlayStepsSound()
    {
        if(!stepsSound.isPlaying)
        {
            if(movement.x != 0 && CheckGround())
            {
                stepsSound.Play();
            }
        }
    }

    void PausedGame()
    {
        rb.constraints = RigidbodyConstraints2D.FreezePosition;
    }

    void UnpauseGame()
    {
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }
}