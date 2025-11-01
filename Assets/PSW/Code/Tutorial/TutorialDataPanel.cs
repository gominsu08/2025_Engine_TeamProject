using DG.Tweening;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TutorialDataPanel : MonoBehaviour
{
    [SerializeField] private List<Image> ImageList;
    [SerializeField] private List<TutorialDataSO> tutorialDataList;

    [SerializeField] private RectTransform textMaskTrm;
    [SerializeField] private TextMeshProUGUI tutorialText;
    [SerializeField] private float changeTime = 0.4f;

    private bool _isIndex;
    private bool _isEndChange = true;
    private int _dataIndex = 0;

    private Vector3 _upMaskSize;

    private void Start()
    {
        _upMaskSize = textMaskTrm.sizeDelta;
        TutorialDataSO tempData = tutorialDataList[0];
        ImageList[1].sprite = tempData.tutorialImage;
        tutorialText.text = tempData.tutorialText;
        tutorialText.ForceMeshUpdate();
    }

    public void NextImage(bool isLeft)
    {
        if (_isEndChange == false) return;

        _isEndChange = true;

        int index = _isIndex ? 1 : 0;
        int origin = isLeft ? 1 : 0;

        SetIndex(isLeft);
        TutorialDataSO tempData = tutorialDataList[_dataIndex];

        ImageList[index].sprite = tempData.tutorialImage;
        ImageList[index].fillAmount = 0;
        ImageList[index].fillOrigin = origin;

        int lastIndex = ImageList[index].transform.parent.childCount - 2;
        ImageList[index].transform.SetSiblingIndex(lastIndex);

        ChangeText(tempData.tutorialText);
        ImageList[index].DOFillAmount(1, changeTime).OnComplete(() => _isEndChange = true);
        _isIndex = !_isIndex;
    }

    private void SetIndex(bool isLeft)
    {
        if(isLeft)
        {
            _dataIndex++;
            if (_dataIndex >= tutorialDataList.Count)
                _dataIndex = 0;
        }
        else
        {
            _dataIndex--;
            if (_dataIndex < 0)
                _dataIndex = tutorialDataList.Count-1;
        }
    }

    private async void ChangeText(string text)
    {
        textMaskTrm.DOSizeDelta(Vector3.zero, changeTime /2);
        await Awaitable.WaitForSecondsAsync(changeTime / 2);
        tutorialText.text = text;
        tutorialText.ForceMeshUpdate();
        textMaskTrm.DOSizeDelta(_upMaskSize, changeTime / 2);
    }
}
