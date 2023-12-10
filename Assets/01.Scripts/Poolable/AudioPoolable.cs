using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioPoolable : PoolableObject
{
    private AudioSource _audioSource = null;

    public override void PopInit()
    {
        if (_audioSource == null)
        {
            _audioSource = GetComponent<AudioSource>();
        }
    }

    public override void PushInit()
    {
    }

    public override void StartInit()
    {
    }

    public void Play(AudioClip audioClip, float volume = 1f, float pitch = 1f)
    {
        _audioSource.clip = audioClip;
        _audioSource.volume = volume;
        _audioSource.pitch = pitch;
        _audioSource.Play();
        StartCoroutine(WaitAndPush(audioClip.length * 1.05f));
    }

    private IEnumerator WaitAndPush(float delay)
    {
        yield return new WaitForSeconds(delay);
        PoolManager.Instance.Push(this);
    }
}
