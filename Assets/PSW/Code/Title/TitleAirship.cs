using DG.Tweening;
using System.Collections;
using UnityEngine;

public class TitleAirship : MonoBehaviour
{
    [SerializeField] private float upDownValue;
    [SerializeField] private float moveTime;

    private WaitForSeconds _wait;

    private bool _isUp;
    private Vector3 _moveDir;

    private void Start()
    {
        _wait = new WaitForSeconds(moveTime);
        _moveDir = transform.localPosition;
        StartCoroutine(UpDownMove());
    }

    private IEnumerator UpDownMove()
    {
        if(_isUp)
            _moveDir.y += upDownValue;
        else
            _moveDir.y -= upDownValue;

        transform.DOLocalMove(_moveDir, moveTime).SetEase(Ease.InOutBack);
        yield return _wait;
        _isUp = !_isUp;
        StartCoroutine(UpDownMove());

    }
}
