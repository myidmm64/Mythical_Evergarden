using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;
using System;

public class PopupPoolObject : PoolableObject
{
    [SerializeField]
    private TextMeshPro _text = null;
    private MeshRenderer _meshRenderer = null;

    private Sequence _seq = null;

    public override void StartInit()
    {
    }

    public override void PopInit()
    {
        if (_meshRenderer == null)
        {
            _meshRenderer = _text.GetComponent<MeshRenderer>();
        }
    }

    public override void PushInit()
    {
        StopAllCoroutines();

        if (_seq != null)
        {
            _seq.Kill();
        }

        transform.position = Vector2.zero;
        transform.localScale = Vector2.one;
        _text.color = Color.white;
    }

    public void ColorSet(Color color)
    {
        _text.color = color;
    }

    public void PopupText(PopupDataSO data, string text, Vector2 pos, Vector2 lastPos, Action Callback = null) // 시작 포지션, 마지막 포지션, 색깔, 폰트사이즈, 사이즈
    {
        _text.SetText(text);
        transform.position = pos;
        _text.color = data.color;
        _text.fontSize = data.fontSize;

        _seq = DOTween.Sequence();
        _seq.Append(transform.DOMove(lastPos, data.duration));
        _seq.AppendCallback(() => { Callback?.Invoke(); });
        _seq.Append(_text.DOFade(0f, data.fadeDuration));
        _seq.AppendCallback(() => { PoolManager.Instance.Push(this); });
    }

    public void PunchPopup(PopupDataSO data, string text, Vector2 pos, Action Callback = null)
    {
        _text.SetText(text);
        _text.fontSize = data.fontSize;
        _text.color = data.color;
        transform.position = pos;
        transform.localScale = Vector2.one * data.punchSize;

        _seq = DOTween.Sequence();
        _seq.Append(transform.DOScale(1f, data.duration));
        _seq.AppendCallback(() => { Callback?.Invoke(); });
        _seq.Append(_text.DOFade(0f, data.fadeDuration));
        _seq.AppendCallback(() => { PoolManager.Instance.Push(this); });
    }

    public void DropPopup(PopupDataSO data, string text, Vector2 pos, Vector2 endPos, Action Callback = null)
    {
        _text.SetText(text);
        _text.fontSize = data.fontSize;
        _text.color = data.color;
        transform.position = pos;
        transform.localScale = Vector2.one * data.punchSize;

        _seq = DOTween.Sequence();
        _seq.Append(transform.DOJump(endPos, 2f, 1, data.duration));
        _seq.AppendCallback(() => { Callback?.Invoke(); });
        _seq.Append(_text.DOFade(0f, data.fadeDuration));
        _seq.AppendCallback(() => { PoolManager.Instance.Push(this); });
    }
}