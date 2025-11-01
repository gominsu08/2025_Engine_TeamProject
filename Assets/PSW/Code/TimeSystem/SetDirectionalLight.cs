using DG.Tweening;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class SetDirectionalLight : MonoBehaviour
{
    [SerializeField] private float morningStartRotationXValue;
    [SerializeField] private float minLightXValue = 24.5f;
    [SerializeField] private Light directionalLight;

    private Vector3 _morningRotation;
    private Vector3 _morningStartRotation;
    private Vector3 _nightRotation;

    private bool _isMorning = true;
    private float _morningTime;

    public void Init(float time)
    {
        _morningTime = time / 2;
        
        _morningRotation = directionalLight.transform.eulerAngles;
        _nightRotation = _morningRotation;
        _morningStartRotation = _morningRotation;
        
        _morningStartRotation.x = morningStartRotationXValue;
        _nightRotation.x -= minLightXValue;
        SetLight();
    }


    public async void SetLight()
    {
        directionalLight.transform.DOLocalRotate(_isMorning ? _nightRotation : _morningRotation, _morningTime);
        directionalLight.DOIntensity(_isMorning ? 0 : 1, _morningTime);

        await Awaitable.WaitForSecondsAsync(_morningTime);
        _isMorning = !_isMorning;

        if (_isMorning == false)
            directionalLight.transform.DOLocalRotate(_morningStartRotation, 0);

        SetLight();
    }
}
