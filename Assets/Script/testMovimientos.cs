using UnityEngine;
using UnityEngine.InputSystem;
public class testMovimientos : MonoBehaviour
{
    Rigidbody2D body;
    [SerializeField] float velocity = 2f;
    [SerializeField] float jumpForce = 7f;

    [SerializeField] CheckGrounded _groundDetector; // referencia a nuestro objeto de salto 

    InputSystem_Actions _playerInput;
    InputAction _move;
    InputAction _jump;
    Vector2 _inputDir;
    Animator anim;
    bool _jumpRequested;
    SpriteRenderer spriteRenderer;

    [Header("Sonido")]
    [SerializeField] AudioClip jumpSound;
    AudioSource audioSource;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        _playerInput = new InputSystem_Actions();
        _playerInput.Enable();
        _move = _playerInput.Player.Move;
        _jump = _playerInput.Player.Jump;
        anim = GetComponent<Animator>();
        _jump.performed += OnJump;
        audioSource = GetComponent<AudioSource>();
    }

    private void OnDestroy(){
        _jump.performed -= OnJump;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        TryGetComponent<Rigidbody2D>(out body);

        _groundDetector = GetComponentInChildren<CheckGrounded>();
 
    }

    void OnJump(InputAction.CallbackContext ctx)
    {
        if (_groundDetector != null && _groundDetector.isGrounded)
            {                                      
            _jumpRequested = true;
            if(jumpSound != null)
            {
                audioSource.PlayOneShot(jumpSound);
            }
            
        } 
    }


    private void Update()
    {
        _inputDir = _move.ReadValue<Vector2>();
        if (_inputDir.x == 1)
        {
            spriteRenderer.flipX = false;
        }
          if (_inputDir.x == -1)
        {
            spriteRenderer.flipX = true ;
            
        }
        if (_groundDetector.isGrounded)
        {
            anim.SetBool("jump",false);
        }
        else
        {
            anim.SetBool("jump",true);
        }
    }


    private void FixedUpdate()
    {
        body.linearVelocity = new Vector2(_inputDir.x * velocity, body.linearVelocityY);

        if (_jumpRequested)
        {
            body.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            _jumpRequested = false;
        }
    }
 
}