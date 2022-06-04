using System.Collections.Generic;
using UnityEngine;
using MyLibrary.Attribute;

[System.Serializable]
public class Sound
{
    [SerializeField] private string _name;
    [SerializeField] private AudioClip _clip;
    public string GetName => _name;
    public AudioClip GetClip => _clip;
}

[CreateAssetMenu(fileName = "AudioClipData", menuName = "ScriptableObjects/MyLibrary/Manager/AudioClipData", order = 11)]
public class AudioClipData : ScriptableObject
{
    [StringVariableToElementName("_name")]
    [SerializeField] private List<Sound> Sounds = new List<Sound>();
    public List<Sound> GetSounds => Sounds;
    public Sound GetSound(string clipName)
    {
        foreach (var Sound in Sounds)
        {
            if (Sound.GetName.Equals(clipName))
                return Sound;
        }
        return default;
    }
}
