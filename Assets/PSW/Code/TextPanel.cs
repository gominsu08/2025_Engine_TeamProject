using DG.Tweening;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class TextPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI panelText;
    public async void Text(string text)
    {
        transform.DOScale(Vector3.one, 0.5f);
        panelText.text = text;
        await Awaitable.WaitForSecondsAsync(0.5f + 0.8f);
        transform.DOScale(Vector3.zero, 0.5f);
    }
}
