using DG.Tweening;
using PolyverseSkiesAsset;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class SetDirectionalLight : MonoBehaviour
{
    [SerializeField] private float morningStartRotationXValue;
    [SerializeField] private float minLightXValue = 24.5f;
    [SerializeField] private PolyverseSkies polyverseSkies;

    private bool _isMorning = true;
    private float _morningTime;

    public void Init(float time)
    {
        _morningTime = time / 2;
        SetLight();
    }

    public async void SetLight()
    {
        //directionalLight.DOIntensity(_isMorning ? 0 : 1, _morningTime);
        //directionalLight.transform.DOLocalRotate(_isMorning ? _nightRotation : _morningRotation, _morningTime);

        _isMorning = !_isMorning;

        DOTween.To(() => polyverseSkies.timeOfDay,
            x => polyverseSkies.timeOfDay = x,
            _isMorning ? 0 : 1,
            _morningTime);

        await Awaitable.WaitForSecondsAsync(_morningTime);
        SetLight();
    }
}
