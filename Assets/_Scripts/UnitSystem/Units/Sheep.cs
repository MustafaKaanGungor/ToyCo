using TMPro;
using UnityEngine;

public class Sheep : MapUnit
{
    [SerializeField] private float _fleeDistance = 4f;
    private bool _isFleeing = false;
    private int _sheepCount;
    public int SheepCount => _sheepCount;
    private int _currentSheepCount;
    [Header("Penguen Sallanma Ayarlar�")]
    public float waddleSpeed = 12f;
    public float waddleAngle = 15f;

    [Header("Text")]
    [SerializeField] private TMP_Text _sheepCountText;
    protected override void Start()
    {
        base.Start();
    }
    public void InitializeSheep(int count)
    {
        _sheepCount = count;
        _currentSheepCount = count;
        _sheepCountText.text = _currentSheepCount.ToString();
    }

    private void ForceMoveTo(Vector2 target)
    {
        TargetPosition = target;
        _isWaiting = false;
        _waitTimer = 0f;
    }

    protected override void SelectNextTarget()
    {
        TargetPosition = GetRandomPositionInBounds();
    }

    public override void OnPlayerInteracted(Player player)
    {
        Vector2 fleeDir = (transform.position - player.transform.position).normalized;
        ForceMoveTo((Vector2)transform.position + fleeDir * _fleeDistance);
        _isFleeing = true;
        GameEvents.TriggerPlaySound(SfxType.Sfx_Sheep);
    }

    protected override void Update()
    {
        if (_isFleeing)
        {
            Vector2 currentPos = transform.position;
            Vector2 nextPos = Vector2.MoveTowards(currentPos, TargetPosition, Speed * Time.deltaTime);
            float waddle = Mathf.Sin(Time.time * waddleSpeed) * waddleAngle;
            _visualTransform.rotation = Quaternion.Euler(0, 0, waddle);

            if (IsPositionBlocked(nextPos))
            {
                _isFleeing = false;
                SelectNextTarget();
                return;
            }

            transform.position = nextPos;

            float directionX = TargetPosition.x - transform.position.x;
            if (Mathf.Abs(directionX) > 0.05f)
            {
                _visualTransform.localScale = new Vector3(directionX > 0 ? 1 : -1, 1, 1);
            }

            if (Vector2.Distance(transform.position, TargetPosition) < StopDistance)
            {
                _isFleeing = false;
                SelectNextTarget();
            }
        }
        else
        {
            base.Update();
        }
    }
    public void DestroySheep()
    {
        //bir flagi degistirmeli
        Destroy(gameObject);
    }
    private void ReduceSheepCount(int amount)
    {
        _currentSheepCount -= amount;
        _sheepCountText.text = _currentSheepCount.ToString();
        if (_currentSheepCount <= 0)
        {
            DestroySheep();
        }
    }
    protected override void MoveTowardsTarget()
    {
        base.MoveTowardsTarget();
        float waddle = Mathf.Sin(Time.time * waddleSpeed) * waddleAngle;
        _visualTransform.transform.rotation = Quaternion.Euler(0, 0, waddle);

    }
    protected override void HandleWaiting()
    {
        base.HandleWaiting();
        _visualTransform.transform.rotation = Quaternion.identity;

    }
    public override void OnInteracted(MapUnit initiator)
    {
        if (initiator is Wolf)
        {
            Vector2 distance = transform.position - initiator.transform.position;
            if (distance.magnitude < 1.5f)
            {

                ReduceSheepCount(5);
                GameEvents.TriggerPlaySound(SfxType.Sfx_Sheep);
                Debug.Log("Sheep was attacked by a wolf!");
            }
        }
    }
}
