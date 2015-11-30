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

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	    isGrounded = Physics2D.OverlapCircle(groundPoint.position, radius, ground);
	    if (isGrounded)
	    {
	        if (Input.GetMouseButtonDown(0))
	        {
	            GetComponent<Rigidbody2D>().AddForce(Vector2.up * force);
	            audioSource.clip = jump;
                audioSource.Play();
	        }
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
    }
}
