using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(AudioSource))]
public class Player : MonoBehaviour
{
    public float runSpeed = 1.5f;
    public float speedModifier = 10.0f;
    public float jumpHeight = 5.0f;

    public AudioClip jumpSound;
    public AudioClip deathSound;

    private bool _preventMovement = false;

    public bool PreventMovement
    {
        get { return _preventMovement; }
        set { _preventMovement = value; }
    }

    private bool _canJump = false;

    private SpriteRenderer _spriteRenderer;
    private Animator _animator;
    private Rigidbody2D _rigidbody;
    private AudioSource _audioSource;

    private LevelManager _levelManager;

    private bool facingRight = true;

    // Start is called before the first frame update
    void Start()
    {
        _spriteRenderer = this.GetComponent<SpriteRenderer>();
        _animator = this.GetComponentInChildren<Animator>();
        _rigidbody = this.GetComponent<Rigidbody2D>();
        _audioSource = this.GetComponentInChildren<AudioSource>();

        _levelManager = FindObjectOfType<LevelManager>();
    }

    // Update is called once per frame
    void Update()
    {
        MovementAnimations();
        CalculateMovement();
    }

    public void Respawn()
    {
        if (!_levelManager.GameOver)
        {
            _audioSource.PlayOneShot(deathSound);
            this.transform.position = _levelManager.respawnLocation.position;
        }
        else
        {
            // Game Over
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<TilemapCollider2D>() != null)
        {
            _canJump = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<TilemapCollider2D>() != null)
        {
            _canJump = false;
        }
    }

    void CalculateMovement()
    {
        if (!PreventMovement)
        {
            //_rigidbody.simulated = true;
            float runModifier = (Input.GetAxis("Fire3") > 0.0f) ? runSpeed : 1.0f;
            float horizontalMovement = Input.GetAxis("Horizontal") * speedModifier * runModifier;
            float max_change = 25 * Time.fixedDeltaTime * runSpeed;

            //_rigidbody.AddForce(new Vector2(horizontalMovement * runSpeed, 0.0f));
            //_rigidbody.velocity.Set(horizontalMovement * runSpeed, _rigidbody.velocity.y);

            _rigidbody.velocity = new Vector2(Mathf.MoveTowards(_rigidbody.velocity.x, horizontalMovement, max_change), _rigidbody.velocity.y);

            if (Input.GetButtonDown("Jump"))
            {
                if (_canJump)
                {
                    _audioSource.PlayOneShot(jumpSound);
                    _rigidbody.velocity = _rigidbody.velocity + (Vector2.up * jumpHeight);
                }
            }
        }
        else
        {
            //_rigidbody.simulated = false;
            _rigidbody.velocity = new Vector2(0.0f, _rigidbody.velocity.y);
        }
    }

    void MovementAnimations()
    {
        if (!PreventMovement)
        {
            float horizontalMovement = Input.GetAxis("Horizontal");

            if (!_canJump)
            {
                _animator.Play("ManJump");
            }
            else
            {
                if (Input.GetAxis("Fire3") > 0.0f)
                {
                    if (horizontalMovement > 0.0f)
                    {
                        _animator.Play("ManRun");
                        facingRight = true;
                    }
                    else if (horizontalMovement < 0.0f)
                    {
                        _animator.Play("ManRun");
                        facingRight = false;
                    }
                }
                else
                {
                    if (horizontalMovement > 0.0f)
                    {
                        _animator.Play("ManWalk");
                        facingRight = true;
                    }
                    else if (horizontalMovement < 0.0f)
                    {
                        _animator.Play("ManWalk");
                        facingRight = false;
                    }
                }
            }

            if (Mathf.Abs(_rigidbody.velocity.magnitude) < 0.05f)
            {
                _animator.Play("Man");
            }

            if (!facingRight)
            {
                _animator.transform.rotation = Quaternion.Euler(0.0f, 180.0f, 0.0f);
            }
            else
            {
                _animator.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
            }
        }
        else
        {
            _animator.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
            _animator.Play("Man");
        }
    }
}

