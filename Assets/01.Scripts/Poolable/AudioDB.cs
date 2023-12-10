using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/AudioDB")]
public class AudioDB : ScriptableObject
{
    public List<AudioData> audioDatas = new List<AudioData>();

}
