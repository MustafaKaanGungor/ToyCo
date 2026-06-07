using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

[RequireComponent(typeof(RectTransform))]
public class SlidePanel : MonoBehaviour
{
    [Header("Slide Settings")]
    [SerializeField] private float _slideDuration = 0.4f;
    [SerializeField] private Ease _slideEase = Ease.OutCubic;
    [SerializeField] private float _hiddenOffsetX = 300f;

    [Header("References")]
    [SerializeField] private Button _toggleButton;

    private RectTransform _rectTransform;
    private Vector2 _visiblePosition;
    private Vector2 _hiddenPosition;
    private bool _isVisible;
    private Tween _slideTween;

    public bool IsVisible => _isVisible;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        _visiblePosition = _rectTransform.anchoredPosition;
        _hiddenPosition = _visiblePosition + new Vector2(_hiddenOffsetX, 0f);
    }

    private void Start()
    {
        _rectTransform.anchoredPosition = _hiddenPosition;
        _isVisible = false;

        if (_toggleButton != null)
        {
            _toggleButton.onClick.AddListener(Toggle);
        }
    }

    private void OnDestroy()
    {
        if (_toggleButton != null)
        {
            _toggleButton.onClick.RemoveListener(Toggle);
        }
    }

    public void Toggle()
    {
        GameEvents.TriggerPlaySound(SfxType.Sfx_Click);

        if (_isVisible)
            SlideOut();
        else
            SlideIn();
    }

    public void SlideIn()
    {
        _slideTween?.Kill();
        _slideTween = _rectTransform.DOAnchorPos(_visiblePosition, _slideDuration).SetEase(_slideEase);
        _isVisible = true;
    }

    public void SlideOut()
    {
        _slideTween?.Kill();
        _slideTween = _rectTransform.DOAnchorPos(_hiddenPosition, _slideDuration).SetEase(_slideEase);
        _isVisible = false;
    }
}
