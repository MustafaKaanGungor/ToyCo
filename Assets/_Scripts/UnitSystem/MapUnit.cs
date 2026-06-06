using UnityEngine;
[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public abstract class MapUnit : MonoBehaviour , IMapInteractable
{
    [Header("Map Borders")]
    [SerializeField] private float _minX = -10f;
    [SerializeField] private float _maxX = 10f;
    [SerializeField] private float _minY = -10f;
    [SerializeField] private float _maxY = 10f;

    [Header("Movement Seettings")]
    [SerializeField] private float _speed = 2f;
    [SerializeField] private float _stopDistance = 0.1f;

    [Header("Waiting Settings")]
    [SerializeField] private float _minWaitTime = 1f;
    [SerializeField] private float _maxWaitTime = 3f;

    // Private alanların alt sınıflar tarafından okunabilmesi için Encapsulation (Kapsülleme)
    protected float MinX => _minX;
    protected float MaxX => _maxX;
    protected float MinY => _minY;
    protected float MaxY => _maxY;
    protected float Speed => _speed;
    protected float StopDistance => _stopDistance;
    protected float MinWaitTime => _minWaitTime;
    protected float MaxWaitTime => _maxWaitTime;

    // Alt sınıfların hem okuyup hem değiştirebilmesi gereken mülk (Property)
    protected Vector2 TargetPosition { get; set; }

    protected bool _isWaiting = false;
    protected float _waitTimer = 0f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<IMapInteractable>(out var interactable))
        {
            interactable.OnInteracted(this);
        }
        if (other.TryGetComponent<Player>(out var player))
        {
            OnPlayerInteracted(player);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent<IMapInteractable>(out var interactable))
        {
            interactable.OnWithdraw(this);
        }
    }


    protected virtual void Start()
    {
        SelectNextTarget();
    }

    protected virtual void Update()
    {
        if (_isWaiting)
        {
            HandleWaiting();
        }
        else
        {
            MoveTowardsTarget();
        }
    }

    protected abstract void SelectNextTarget();

    protected virtual void MoveTowardsTarget()
    {
        Vector2 currentPos = transform.position;
        transform.position = Vector2.MoveTowards(currentPos, TargetPosition, _speed * Time.deltaTime);

        float directionX = TargetPosition.x - transform.position.x;
        if (Mathf.Abs(directionX) > 0.05f)
        {
            transform.localScale = new Vector3(directionX > 0 ? 1 : -1, 1, 1);
        }

        if (Vector2.Distance(transform.position, TargetPosition) < _stopDistance)
        {
            StartWaiting();
        }
    }

    private void StartWaiting()
    {
        _isWaiting = true;
        _waitTimer = Random.Range(_minWaitTime, _maxWaitTime);
    }

    private void HandleWaiting()
    {
        _waitTimer -= Time.deltaTime;
        if (_waitTimer <= 0)
        {
            _isWaiting = false;
            SelectNextTarget();
        }
    }

    protected Vector2 GetRandomPositionInBounds()
    {
        float randomX = Random.Range(_minX, _maxX);
        float randomY = Random.Range(_minY, _maxY);
        return new Vector2(randomX, randomY);
    }

    protected virtual void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Vector3 center = new Vector3((_minX + _maxX) / 2, (_minY + _maxY) / 2, 0);
        Vector3 size = new Vector3(_maxX - _minX, _maxY - _minY, 0.1f);
        Gizmos.DrawWireCube(center, size);
    }

    public virtual void OnInteracted(MapUnit initiator)
    {
        
    }

    public virtual void OnWithdraw(MapUnit initiator)
    {
    }
    public virtual void OnPlayerInteracted(Player player) { }
}