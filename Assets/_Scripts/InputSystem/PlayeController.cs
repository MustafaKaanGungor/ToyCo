using UnityEngine;

public class PlayeController : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 5f;
    private Vector2 _moveInput;
    private Rigidbody2D _rb;
    private void OnEnable()
    {
        GameEvents.OnMoveInput += OnMove;
    }
    private void OnDisable()
    {
        GameEvents.OnMoveInput -= OnMove;
    }
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        _rb.MovePosition(_rb.position + _moveInput * _moveSpeed * Time.fixedDeltaTime);
    }
    private void OnMove(Vector2 value)
    {
        _moveInput = value;
        FlipSprite(_moveInput);
    }
    private void FlipSprite(Vector2 value)
    {
        if (value.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (value.x > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }
}
