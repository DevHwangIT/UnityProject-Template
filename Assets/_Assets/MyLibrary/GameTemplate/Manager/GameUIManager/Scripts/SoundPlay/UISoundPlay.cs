using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.EventSystems;

public class UISoundPlay : MonoBehaviour, IPointerClickHandler
{
    AudioSource _source;
    [SerializeField] private AudioClip _clip;
    [SerializeField] private AudioMixerGroup _mixer;

    private void Awake()
    {
        _source = this.gameObject.AddComponent<AudioSource>();
        _source.playOnAwake = false;
        _source.clip = _clip;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (_source.isPlaying)
            _source.Stop();
        _source.Play();
    }
}
