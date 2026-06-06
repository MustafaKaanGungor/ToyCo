using UnityEngine;

public class PlayeController : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 5f;
    private Vector2 _moveInput;
    private Rigidbody2D _rb;
    private float _baseVisualY;
    [Header("Penguen Efekt Ayarlarý (Görsel)")]
    // Efektin uygulanacaðý child nesnesinin Transformu
    [SerializeField] private Transform _spriteVisual;
    // Ne kadar hýzlý sallanacak?
    [SerializeField] private float _waddleSpeed = 10f;
    // Ne kadar derece yatacak?
    [SerializeField] private float _tiltAngle = 10f;
    // Sallanýrken ne kadar yukarý zýplayacak?
    [SerializeField] private float _hopAmount = 0.05f;
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
        if (_spriteVisual == null)
        {
            Debug.LogError("Lütfen Sprite Visual (Child Nesnesi) Transformunu Inspector'dan atayýn!");
            enabled = false;
        }
        else
        {
            // Orijinal yerel yüksekliði kaydet (zýplama efekti için)
            _baseVisualY = _spriteVisual.localPosition.y;
        }
    }
    private void Update()
    {
        // Görsel efekti hareket varsa Update içinde uyguluyoruz
        //VisualWaddleEffect();
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
    private void VisualWaddleEffect()
    {
        if (_moveInput.sqrMagnitude < 0.01f)
        {
            _spriteVisual.localRotation = Quaternion.Lerp(_spriteVisual.localRotation, Quaternion.identity, Time.deltaTime * 10f);
            Vector3 targetPos = _spriteVisual.localPosition;
            targetPos.y = Mathf.Lerp(targetPos.y, _baseVisualY, Time.deltaTime * 10f);
            _spriteVisual.localPosition = targetPos;
            return;
        }


        float wave = Mathf.Sin(Time.time * _waddleSpeed);

        float tilt = wave * _tiltAngle;
        _spriteVisual.localRotation = Quaternion.Euler(0, 0, tilt);

        // 2. Y Pozisyonunu (Zýplama) Ayarla
        // Abs(wave) kullanýyoruz çünkü hem saða hem sola sallanýrken penguen hafifçe zýplar (yukarý çýkar)
        float hop = Mathf.Abs(wave) * _hopAmount;
        Vector3 newPos = _spriteVisual.localPosition;
        newPos.y = _baseVisualY + hop;
        _spriteVisual.localPosition = newPos;
    }
}
