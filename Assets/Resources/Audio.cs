using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio : MonoBehaviour
{
    [System.Serializable]
    public class Sounds
    {
        public string name;
        public AudioClip clip;
        private AudioSource source;
        [Range(0f, 1f)]
        public float volume = 1f;
        [Range(0.5f, 1.5f)]
        public float pitch = 1f;
        public bool loop;
        public void SetSource(AudioSource _source)
        {
            source = _source;
            source.clip = clip;
        }
        public void Play()
        {
            source.volume = volume;
            source.pitch = pitch;
            source.loop = loop;
            source.Play();
        }
        public void Pause()
        {
            source.Pause();
        }

    }
    [System.Serializable]
    public class BGM
    {
        public string name;
        public AudioClip clip;
        private AudioSource source;
        [Range(0f, 1f)]
        public float volume = 1f;
        [Range(0.5f, 1.5f)]
        public float pitch = 1f;
        public void SetSource(AudioSource _source)
        {
            source = _source;
            source.clip = clip;
        }
        public void Play()
        {
            source.volume = volume;
            source.pitch = pitch;
            source.loop = loop;
            source.Play();
        }
        public void Pause()
        {
            source.Pause();
        }
        public bool loop;
    }
    public static Audio self;
    [SerializeField]
    Sounds[] sounds;
    [SerializeField]
    BGM bgm;
    private void Awake()
    {
        if (self == null)
        {
            self = this;
            DontDestroyOnLoad(this);
        }
        else if (this != self)
        {
            Destroy(gameObject);
        }
        for (int i = 0; i < sounds.Length; i++)
        {
            GameObject _go = new GameObject("sounds" + i + "_" + sounds[i].name);
            _go.transform.SetParent(this.transform);
            sounds[i].SetSource(_go.AddComponent<AudioSource>());
            sounds[i].volume = 0;
        }
        GameObject _bgm = new GameObject("bgm" + bgm.name);
        _bgm.transform.SetParent(this.transform);
        bgm.SetSource(_bgm.AddComponent<AudioSource>());
    }

    void Start()
    {
        BGMon();
        SEon();
    }
    public void PlaySound(string _name)
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            if (sounds[i].name == _name)
            {
                sounds[i].Play();
                return;
            }
        }
    }
    public void PauseSound(string _name)
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            if (sounds[i].name == _name)
            {
                sounds[i].Pause();
                return;
            }
        }
    }
    public void BGMon()
    {
        bgm.Play();
    }
    public void BGMoff()
    {
        bgm.Pause();
    }
    public void SEoff()
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            sounds[i].volume = 0f;
        }
    }
    public void SEon()
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            sounds[i].volume = 1f;
        }
    }
  
   
}
