using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    private Transform _transform;
    private Rigidbody2D _rigidbody;
    private Animator _animator;
    private float _move;
    private float _jump;
    private bool _IsFacingRight;
    private bool _IsGrounded;

    public float Velocity = 10f;
    public float JumpForce = 100f;
    public Camera camera;
    public Transform SpawnPoint;
    

    [Header("Sound Clips")]
    public AudioSource JumpSound;
    public AudioSource DeathSound;

    // Use this for initialization
    void Start ()
    {
        this._initialize();
	
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        if (this._IsGrounded)
        {
            this._move = Input.GetAxis("Horizontal");
            if (this._move > 0f)
            {
                this._animator.SetInteger("PlayerState", 1);
                this._move = 1;
                this._IsFacingRight = true;
                this._flip();
            }
            else if (this._move < 0f)
            {
                this._animator.SetInteger("PlayerState", 1);
                this._move = -1;
                this._IsFacingRight = false;
                this._flip();
            }
            else
            {
                this._animator.SetInteger("PlayerState",0);
                this._move = 0f;
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                this._jump = 1f;
                this.JumpSound.Play();
            }

            this._rigidbody.AddForce(new Vector2(this._move * this.Velocity, this._jump * this.JumpForce), ForceMode2D.Force);


        }
        else
        {
            this._move = 0f;
            this._jump = 0f;
        }

        
        this.camera.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, -10f);
	
	}
   

    private void _initialize()
    {
        this._transform = GetComponent<Transform>();
        this._rigidbody = GetComponent<Rigidbody2D>();
        this._animator = GetComponent<Animator>();
        this._move = 0f;
        this._IsFacingRight = true;
        this._IsGrounded = false;
    }

    private void _flip()
    {
        if (this._IsFacingRight)
        {
            this._transform.localScale = new Vector2(1f, 1f);
        }
        else
        {
            this._transform.localScale = new Vector2(-1f, 1f);
        }

          
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("DeathPlane"))
        {
            this._transform.position = this.SpawnPoint.position;
            this.DeathSound.Play();
        }
    }
    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Platform"))
        {
            this._IsGrounded = true;
        }
    }
    private void OnCollisionExit2D(Collision2D other)
    {
        this._animator.SetInteger("PlayerState",2);
        this._IsGrounded = false;
    }
}
