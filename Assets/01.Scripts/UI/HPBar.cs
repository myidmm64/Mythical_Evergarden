using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HPBar : MonoBehaviour
{
    [SerializeField]
    private Slider _slider = null;
    [SerializeField]
    private TextMeshProUGUI _text = null;

    [SerializeField]
    private float _animationDuration = 0.2f;

    private Sequence _animationSeq = null;

    public void HpInit(int maxHp)
    {
        _slider.minValue = 0;
        _slider.maxValue = maxHp;
        _slider.value = maxHp;
    }

    public void HpUpdate(int targetValue)
    {
        if (_animationSeq != null)
        {
            _animationSeq.Kill();
        }

        if (targetValue > _slider.maxValue)
        {
            targetValue = (int)_slider.maxValue;
        }
        else if (targetValue < _slider.minValue)
        {
            targetValue = (int)_slider.minValue;
        }

        _animationSeq = DOTween.Sequence();
        DOTween.To(() => (int)_slider.value,
            x =>
            {
                _slider.value = x;
                _text.SetText($"{x}/{(int)_slider.maxValue}");
            }, targetValue, _animationDuration);
    }
}
