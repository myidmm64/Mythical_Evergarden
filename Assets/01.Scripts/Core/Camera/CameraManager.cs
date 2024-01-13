using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoSingleTon<CameraManager>
{
    [SerializeField]
    private static CinemachineVirtualCamera _cmVCam = null;

    private CinemachineBasicMultiChannelPerlin _noise = null;

    private Coroutine _zoomCoroutine = null;
    private Coroutine _shakeCoroutine = null;

    private float _currentShakeAmount = 0f;

    private void OnEnable()
    {
        if (_cmVCam == null)
        {
            _cmVCam = FindObjectOfType<CinemachineVirtualCamera>();
        }

        _noise = _cmVCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }
    public void CompletePrevFeedBack()
    {
        if (_shakeCoroutine != null)
        {
            StopCoroutine(_shakeCoroutine);
        }

        _noise.m_FrequencyGain = 0; // Èçµå´Â ºóµµ Á¤µµ
        _noise.m_AmplitudeGain = 0;
        _currentShakeAmount = 0f;
    }

    /// <summary>
    /// Ä«¸Þ¶ó ÈçµéÈçµé
    /// </summary>
    /// <param name="Èçµé¸®´Â Á¤µµ"></param>
    /// <param name="Èçµé¸®´Â ºóµµ"></param>
    /// <param name="Èçµé¸± ½Ã°£"></param>
    public void CameraShake(float amplitude, float intensity, float duration)
    {
        if (_currentShakeAmount > amplitude)
        {
            return;
        }
        CompletePrevFeedBack();

        _noise.m_AmplitudeGain = amplitude; // Èçµé¸®´Â Á¤µµ
        _noise.m_FrequencyGain = intensity; // Èçµå´Â ºóµµ Á¤µµ

        _currentShakeAmount = _noise.m_AmplitudeGain;

        _shakeCoroutine = StartCoroutine(ShakeCorutine(amplitude, duration));
    }

    private IEnumerator ShakeCorutine(float amplitude, float duration)
    {
        float time = duration;
        while (time >= 0)
        {
            _noise.m_AmplitudeGain = Mathf.Lerp(0, amplitude, time / duration);
            yield return null;
            time -= Time.unscaledDeltaTime;
        }
        CompletePrevFeedBack();
    }

    public void ZoomCamera(float maxValue, float time, Action Callback = null)
    {

        _zoomCoroutine = StartCoroutine(ZoomCoroutine(maxValue, time, Callback));
    }

    private IEnumerator ZoomCoroutine(float maxValue, float duration, Action Callback = null)
    {
        float time = 0f;
        float nextLens = 0f;
        float currentLens = _cmVCam.m_Lens.FieldOfView;

        while (time <= duration)
        {
            nextLens = Mathf.Lerp(currentLens, maxValue, time / duration);
            _cmVCam.m_Lens.FieldOfView = nextLens;
            yield return null;
            time += Time.deltaTime;
        }
        _cmVCam.m_Lens.FieldOfView = maxValue;
        Callback?.Invoke();
    }


    public void CameraReset()
    {
        if (_zoomCoroutine != null)
        {
            StopCoroutine(_zoomCoroutine);
        }
    }

    public void ZoomCameraUnscale(float maxValue, float time, Action Callback = null)
    {

        _zoomCoroutine = StartCoroutine(ZoomCoroutineUnscale(maxValue, time, Callback));
    }

    private IEnumerator ZoomCoroutineUnscale(float maxValue, float duration, Action Callback = null)
    {
        float time = 0f;
        float nextLens = 0f;
        float currentLens = _cmVCam.m_Lens.FieldOfView;

        while (time <= duration)
        {
            nextLens = Mathf.Lerp(currentLens, maxValue, time / duration);
            _cmVCam.m_Lens.FieldOfView = nextLens;
            yield return new WaitForSecondsRealtime(0.01f);
            time += Time.unscaledDeltaTime;
        }
        _cmVCam.m_Lens.FieldOfView = maxValue;
        Callback?.Invoke();
    }
}