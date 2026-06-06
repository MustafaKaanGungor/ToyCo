using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

[RequireComponent(typeof(Button))]
public class ConfirmClickable : MonoBehaviour
{
    [SerializeField] private CharacterSelection _selection;

    private Button _button;
    private Vector3 _originalScale;

    private void Awake()
    {
        _button = GetComponent<Button>();
        _originalScale = transform.localScale;
        _button.onClick.AddListener(OnConfirmClicked);
    }

    private void OnEnable()
    {
        transform.localScale = Vector3.zero;
        transform.DOScale(_originalScale, 0.3f).SetEase(Ease.OutBack);
    }

    private void OnConfirmClicked()
    {
        if (_selection == null)
            return;

        _button.interactable = false;
        transform.DOPunchScale(Vector3.one * 0.15f, 0.2f, 2)
            .OnComplete(() => _selection.ConfirmSelection());
    }

    private void OnDestroy()
    {
        if (_button != null)
            _button.onClick.RemoveListener(OnConfirmClicked);
    }
}
