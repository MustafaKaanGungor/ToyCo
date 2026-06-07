using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

[RequireComponent(typeof(Button))]
public class BackClickable : MonoBehaviour
{
    [SerializeField] private CharacterSelection _selection;

    private Button _button;
    private Vector3 _originalScale;

    private void Awake()
    {
        _button = GetComponent<Button>();
        _originalScale = transform.localScale;
        _button.onClick.AddListener(OnBackClicked);
    }

    private void OnEnable()
    {
        transform.localScale = Vector3.zero;
        transform.DOScale(_originalScale, 0.3f).SetEase(Ease.OutBack);
    }

    private void OnBackClicked()
    {
        GameEvents.TriggerPlaySound(SfxType.Sfx_Click);

        if (_selection == null)
            return;

        _selection.CancelSelection();
    }

    private void OnDestroy()
    {
        if (_button != null)
            _button.onClick.RemoveListener(OnBackClicked);
    }
}
