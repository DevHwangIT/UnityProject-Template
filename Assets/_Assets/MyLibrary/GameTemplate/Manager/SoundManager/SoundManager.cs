using System.Collections.Generic;
using MyLibrary.DesignPattern;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : Singleton<SoundManager>
{
    [Header("Scriptable Object Data")]
    [SerializeField] private AudioClipData _bgmClipData;
    [SerializeField] private AudioClipData _sfxClipData;
    [Header("How many audio clips do you make into objects for pooling? ")]
    [SerializeField] private int createAudioPrefabCount = 10;
    
    private Dictionary<string, List<GameObject>> clipDictionary = new Dictionary<string, List<GameObject>>();
    private AudioSource _bgmAudioSource;
    
    private const float MuteVolume = -80;
    private const float MinimumVolum = -40;
    private const float MaximumVolum = 0;
    
    private GameObject audioClipDataParentTransform;
    private GameObject clipPoolingParentTransform;

    [SerializeField] private AudioMixer audioMixer;
    private AudioMixer GetMixer => audioMixer;

    public float Volum
    {
        set
        {
            float volum = Mathf.Clamp(value, 0f, 1f);
            AudioListener.volume = volum;
        }
        get
        {
            return AudioListener.volume;
        }
    }
    
    public float MasterVolum
    {
        set
        {
            float volum = value;
            volum = Mathf.Clamp(volum, 0f, 1f);
            volum = Mathf.Lerp(MinimumVolum, MaximumVolum, volum);
            if ((int) volum == MinimumVolum)
                GetMixer.SetFloat("Master", MuteVolume);
            else
                GetMixer.SetFloat("Master", volum);
        }
        get
        {
            float volum = 0f;
            GetMixer.GetFloat("Master", out volum);
            return volum;
        }
    }

    public float BGMVolum
    {
        set
        {
            float volum = value;
            volum = Mathf.Clamp(volum, 0f, 1f);
            volum = Mathf.Lerp(MinimumVolum, MaximumVolum, volum);
            if ((int) volum == MinimumVolum)
                GetMixer.SetFloat("BGM", MuteVolume);
            else
                GetMixer.SetFloat("BGM", volum);
        }
        get
        {
            float volum = 0f;
            GetMixer.GetFloat("BGM", out volum);
            return volum;
        }
    }

    public float SFXVolum
    {
        set
        {
            float volum = value;
            volum = Mathf.Clamp(volum, 0f, 1f);
            volum = Mathf.Lerp(MinimumVolum, MaximumVolum, volum);
            if ((int) volum == MinimumVolum)
                GetMixer.SetFloat("SFX", MuteVolume);
            else
                GetMixer.SetFloat("SFX", volum);
        }
        get
        {
            float volum = 0f;
            GetMixer.GetFloat("SFX", out volum);
            return volum;
        }
    }

    public float UIVolum
    {

        set
        {
            float volum = value;
            volum = Mathf.Clamp(volum, 0f, 1f);
            volum = Mathf.Lerp(MinimumVolum, MaximumVolum, volum);
            if ((int) volum == MinimumVolum)
                GetMixer.SetFloat("UI", MuteVolume);
            else
                GetMixer.SetFloat("UI", volum);
        }
        get
        {
            float volum = 0f;
            GetMixer.GetFloat("UI", out volum);
            return volum;
        }
    }

    private void Awake()
    {
        audioClipDataParentTransform = new GameObject("Sound Controller");
        audioClipDataParentTransform.transform.parent = transform;

        GameObject BGMAudioSource = new GameObject("BGM");
        BGMAudioSource.transform.parent = audioClipDataParentTransform.transform;
        _bgmAudioSource = BGMAudioSource.AddComponent<AudioSource>();
        _bgmAudioSource.outputAudioMixerGroup = GetMixer.FindMatchingGroups("Master")[1];
        _bgmAudioSource.loop = true;

        clipPoolingParentTransform = new GameObject("AudioClip Pool");
        clipPoolingParentTransform.transform.parent = audioClipDataParentTransform.transform;
        
        foreach (var sound in _sfxClipData.GetSounds)
        {
            if (clipDictionary.ContainsKey(sound.GetName) == false)
                clipDictionary.Add(sound.GetName, new List<GameObject>());
            
            for (int i = 0; i < createAudioPrefabCount; i++)
            {
                clipDictionary[sound.GetName].Add(CreateClipGameObject(sound.GetName, sound.GetClip));
            }
        }
        _bgmAudioSource.playOnAwake = true;
    }

    private GameObject CreateClipGameObject(string clipName, AudioClip clip)
    {
        GameObject clipSource = new GameObject(clipName + " Audio");
        clipSource.transform.parent = clipPoolingParentTransform.transform;
        AudioSource source = clipSource.AddComponent<AudioSource>();
        source.outputAudioMixerGroup = GetMixer.FindMatchingGroups("Master")[2];
        source.playOnAwake = false;
        source.clip = clip;

        return clipSource;
    }
    
    public void PlayBGMSound(string clipName)
    {
        AudioClip bgmClip = _bgmClipData.GetSound(clipName).GetClip;
        if (bgmClip != null)
        {
            _bgmAudioSource.Stop();
            _bgmAudioSource.clip = bgmClip;
            _bgmAudioSource.Play();
        }
    }
    
    public void ResumePlayBGMSound()
    {
        if (_bgmAudioSource.clip != null)
            _bgmAudioSource.Play();
    }
    
    public void PauseBGMSound()
    {
        if (_bgmAudioSource.isPlaying)
            _bgmAudioSource.Pause();
    }
    
    public void PlayVFXSound(string clipName)
    {
        Sound sound = _sfxClipData.GetSound(clipName);
        if (sound != null)
        {
            List<GameObject> cList = null;
            if (clipDictionary.TryGetValue(sound.GetName, out cList))
            {
                foreach (var clipObj in cList)
                {
                    AudioSource clipSource = clipObj.GetComponent<AudioSource>();
                    if (clipSource.isPlaying == false)
                    {
                        clipSource.Play();
                        return;
                    }
                }
                AudioSource objAudiosource = CreateClipGameObject(sound.GetName, sound.GetClip).GetComponent<AudioSource>();
                clipDictionary[sound.GetName].Add(objAudiosource.gameObject);
                objAudiosource.Play();
            }
        }
    }

    public void Play3DVFXSound(string clipName, Vector3 position, AudioSource AudioSource3D = null)
    {
        Debug.Log("TODO : 아직 미구현된 기능. 3D 사운드에 대한 관리 고려 중.");
        // Sound sound = _sfxClipData.GetSound(clipName);
        // if (sound != null)
        // {
        //     List<GameObject> cList = null;
        //     if (clipDictionary.TryGetValue(sound.GetClip, out cList))
        //     {
        //         foreach (var clipObj in cList)
        //         {
        //             AudioSource clipSource = clipObj.GetComponent<AudioSource>();
        //             if (clipSource.isPlaying == false)
        //             {
        //                 clipSource.transform.position = position;
        //                 clipSource.Play();
        //                 return;
        //             }
        //         }
        //         AudioSource objAudiosource = CreateClipGameObject(sound.GetName, sound.GetClip).GetComponent<AudioSource>();
        //         clipDictionary[sound.GetClip].Add(objAudiosource.gameObject);
        //         objAudiosource.transform.position = position;
        //         objAudiosource.Play();
        //     }
        // }
    }
}