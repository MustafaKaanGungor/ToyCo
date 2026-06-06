using UnityEngine;

public class Sheep : MapUnit
{
    [SerializeField] private float _fleeDistance = 4f;
    private bool _isFleeing = false;
    private int _sheepCount;
    public int SheepCount => _sheepCount;
    private int _currentSheepCount;
    protected override void Start()
    {
        base.Start();
    }
    public void InitializeSheep(int count)
    {
        _sheepCount = count;
        _currentSheepCount = count;
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
    }

    protected override void Update()
    {
        if (_isFleeing)
        {
            Vector2 currentPos = transform.position;
            Vector2 nextPos = Vector2.MoveTowards(currentPos, TargetPosition, Speed * Time.deltaTime);

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
                transform.localScale = new Vector3(directionX > 0 ? 1 : -1, 1, 1);
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
    public void ReduceSheepCount(int amount)
    {
        _currentSheepCount -= amount;
        if (_currentSheepCount <= 0)
        {
            DestroySheep();
        }
    }
}
