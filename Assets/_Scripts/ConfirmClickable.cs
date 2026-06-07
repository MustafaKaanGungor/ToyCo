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
        _button.interactable = true;
        transform.localScale = Vector3.zero;
        transform.DOScale(_originalScale, 0.3f).SetEase(Ease.OutBack);
    }

    private void OnConfirmClicked()
    {
        GameEvents.TriggerPlaySound(SfxType.Sfx_Click);

        if (_selection == null)
            return;

        bool confirmed = _selection.ConfirmSelection();

        if (!confirmed)
        {
            _button.interactable = false;
            transform.DOShakePosition(0.3f, new Vector3(5f, 0f, 0f), 20, 90f, false, false)
                .OnComplete(() => _button.interactable = true);
        }
    }

    private void OnDestroy()
    {
        if (_button != null)
            _button.onClick.RemoveListener(OnConfirmClicked);
    }
}
