using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(SpriteRenderer), typeof(CapsuleCollider2D))]
public class CharacterDisplay : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    private CharacterSO _characterData;
    private CharacterSelection _selection;
    private Tweener _bobTween;
    private Vector3 _originalPosition;

    public CharacterSO CharacterData => _characterData;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Initialize(CharacterSO data, CharacterSelection selection)
    {
        _characterData = data;
        _selection = selection;
        _spriteRenderer.sprite = data.characterSprite;
        _originalPosition = transform.position;
        Deselect();
    }

    public void Select()
    {
        _bobTween?.Kill();
        transform.position = _originalPosition;
        transform.DOScale(1.15f, 0.2f).SetEase(Ease.OutBack);
        _spriteRenderer.DOColor(Color.white, 0.2f);
    }

    public void Deselect()
    {
        _bobTween?.Kill();
        transform.position = _originalPosition;
        transform.DOScale(1f, 0.2f).SetEase(Ease.OutBack);
        _spriteRenderer.DOColor(new Color(0.55f, 0.55f, 0.55f, 1f), 0.2f)
            .OnComplete(StartBob);
    }

    private void StartBob()
    {
        _bobTween = transform.DOMoveY(_originalPosition.y + 0.12f, 1.5f)
            .SetLoops(-1, LoopType.Yoyo)
            .SetEase(Ease.InOutSine);
    }

    private void OnMouseDown()
    {
        _selection.OnCharacterClicked(this);
    }
}
