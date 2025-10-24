using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering.Universal;

public class PlusMinusButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private bool isPlus;
    [SerializeField] private float addDelay = 0.1f;

    private bool _isAdd;
    private WaitForSeconds _wait;
    private Coroutine holdRoutine;
    private SaleBox _saleBox;

    public void Init(SaleBox box)
    {
        _wait = new WaitForSeconds(addDelay);
        _saleBox = box;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        print("À¸¾û");
        _isAdd = true;
        holdRoutine = StartCoroutine(PlusMinus());
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _isAdd = false;
        if(holdRoutine != null)
        {
            StopCoroutine(holdRoutine);
            holdRoutine = null;
        }
    }

    private IEnumerator PlusMinus()
    {
        while(_isAdd)
        {
            print("¤·");
            _saleBox.SetCount(isPlus);
            yield return _wait;
        }
    }
}
