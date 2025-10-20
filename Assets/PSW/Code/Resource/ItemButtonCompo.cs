using DG.Tweening;
using GMS.Code.Items;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public abstract class ItemButtonCompo : MonoBehaviour
{
    [SerializeField] protected Image typeImage;

    [SerializeField] protected UpDownSize mouseSize;
    [SerializeField] protected UpDownSize clickSize;

    [SerializeField] protected float mouseUpDownTime;
    [SerializeField] protected float clickMoveTime;

    protected bool _isClickMoveStop = true;
    protected WaitForSeconds wait;

    public virtual void Init(Sprite image)
    {
        typeImage.sprite = image;
        wait = new WaitForSeconds(clickMoveTime);
    }

    public virtual void SetSize(bool isMouseUp)
    {
        transform.DOScale(mouseSize.GetSize(isMouseUp), mouseUpDownTime);
    }

    public virtual void ButtonClick()
    {
        if (_isClickMoveStop == false) return;
        StartCoroutine(ClickSetSize());
    }

    public virtual IEnumerator ClickSetSize()
    {
        _isClickMoveStop = false;

        Vector3[] sizes = { clickSize.GetSize(true), clickSize.GetSize(false), Vector3.one };

        for (int i = 0; i < sizes.Length; ++i)
        {
            transform.DOScale(sizes[i], clickMoveTime);
            yield return wait;
        }

        _isClickMoveStop = true;
        ClickAnimEnd();
    }

    public virtual void ClickAnimEnd()
    {

    }
}

[Serializable]
public struct UpDownSize
{
    public Vector3 UpSize;
    public Vector3 DownSize;

    public Vector3 GetSize(bool isUp)
    {
        if (isUp)
            return UpSize;
        else
            return DownSize;
    }
}
