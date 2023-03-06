using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    private Animator anim;
    private SpriteRenderer pSprite;

    [SerializeField] string idleName;
    [SerializeField] string runName;
    [SerializeField] string jumpName;

    float horizontalMovement;

    bool isGrounded = false;

    private void Awake() 
    {
        anim = GetComponentInChildren<Animator>();
        pSprite = GetComponentInChildren<SpriteRenderer>();
    }

    private void Update() 
    {
        horizontalMovement = Input.GetAxisRaw("Horizontal");
        isGrounded = GetComponent<PlayerController>().CheckGround();

        if(horizontalMovement > 0)
        {
            if(isGrounded)
            {
                anim.Play(runName);
            }
            else
            {
                anim.Play(jumpName);
            }

            pSprite.flipX = false;
        }
        else if(horizontalMovement < 0)
        {
            if(isGrounded)
            {
                anim.Play(runName);
            }
            else
            {
                anim.Play(jumpName);
            }

            pSprite.flipX = true;
        }
        else
        {
            if(isGrounded)
            {
                anim.Play(idleName);
            }
            else
            {
                anim.Play(jumpName);
            }
        }
    }
}
