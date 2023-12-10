using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoSingleTon<AudioManager>
{
    [SerializeField]
    private AudioDB _audioDB = null;
    [SerializeField]
    private AudioMixer _audioMixer = null;

    private Dictionary<EAudioType, AudioClip> _audios = null;

    public void Play(EAudioType audioType, float volume = 1f, float pitch = 1f)
    {
        if (_audios == null)
        {
            InitAudioManager();
        }
        AudioPoolable audioPoolable = PoolManager.Instance.Pop(EPoolType.Audio) as AudioPoolable;
        audioPoolable.Play(_audios[audioType], volume, pitch);
    }

    private void InitAudioManager()
    {
        if (_audioDB == null)
        {
            Debug.LogError("AudioDB is Null");
            return;
        }
        _audios = new Dictionary<EAudioType, AudioClip>();
        foreach (var audioData in _audioDB.audioDatas)
        {
            _audios.Add(audioData.audioType, audioData.audioClip);
        }
    }
}
