using DG.Tweening;
using PSW.Code.Sawtooth;
using UnityEngine;

public class TutorialPopPanel : MonoBehaviour
{
    [SerializeField] private Vector2 popUpSize;
    [SerializeField] private float time;

    private RectTransform _rectTransform;
    private bool _isLeft;
    private bool _isStopMove = true;
    private void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
    }

    public void PopUpDownPanel()
    {
        _isLeft = !_isLeft;

        if (_isStopMove == false) return;
        _isStopMove = true;

        _rectTransform.DOSizeDelta(_isLeft ? popUpSize : Vector2.zero, time).OnComplete(() => _isStopMove = true);
    }
}
