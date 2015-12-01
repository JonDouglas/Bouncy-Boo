using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    private bool isGrounded;
    private float radius = 0.7f;
    private float force = 300;
    public Transform coinsParent;
    public Transform groundPoint;
    public LayerMask ground;
    public AudioSource audioSource;
    public AudioClip jump;
    public AudioClip coin;
    public AudioClip win;
    public Animator animator;
    public int coins;
    public TextMesh score;
    public bool jumped;
    private float jumpTime = 0;
    private float jumpDelay = 0.5f;
    private int jumpCount = 0;

	// Use this for initialization
	void Start ()
	{
	    animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update ()
	{
	    isGrounded = Physics2D.OverlapCircle(groundPoint.position, radius, ground);
	    if (isGrounded)
	    {
	        if (Input.GetMouseButtonDown(0))
	        {
	            jumpCount = 1;
	            GetComponent<Rigidbody2D>().AddForce(Vector2.up*force);
	            audioSource.clip = jump;
	            audioSource.Play();
	            jumpTime = jumpDelay;
	            jumped = true;
	            animator.SetTrigger("Jumped");
	        }

	    }

	    jumpTime -= Time.deltaTime;

	    if (jumped && jumpTime < 0 && isGrounded)
	    {
            animator.SetTrigger("Landed");
	        jumpCount = 0;
	        jumped = false;
	        jumpTime = 0;
	    }
    }

    void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.tag == "Coin")
        {
            coins++;
            score.text = coins + "";
            Destroy(collider2D.gameObject);
            audioSource.clip = coin;
            audioSource.Play();
        }
        else if (collider2D.tag == "DeadZone")
        {
            Application.LoadLevel("level1");
        }
        else if (collider2D.tag == "Finish")
        {
            //gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
            audioSource.clip = win;
            audioSource.Play();
        }
    }
}
