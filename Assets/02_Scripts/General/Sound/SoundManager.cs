using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public enum BGMType
{
    BGM_PlayScene
}

public enum SFXType
{
    SFX_HeroDamaged
}

public class SoundManager : SingletonBase<SoundManager>
{
    [SerializeField] private AudioMixer _baseMixer;             // 베이스 오디오 믹서

    [Title("BGM", bold: false)]
    [SerializeField] private AudioMixerGroup _bgmMixerGroup;    // BGM 오디오 믹서 그룹
    [SerializeField] private AudioSource _bgmSource;            // BGM 오디오 소스
    [SerializeField] private List<AudioClip> _bgmClipList;      // BGM 오디오 클립 리스트
    private Dictionary<BGMType, AudioClip> _bgmClipDict = new Dictionary<BGMType, AudioClip>(); // BGM 오디오 클립 딕셔너리 

    [Title("SFX", bold: false)]
    [SerializeField] private AudioMixerGroup _sfxMixerGroup;    // SFX 오디오 믹서 그룹
    [SerializeField] private AudioSource _sfxSource;            // SFX 오디오 소스
    [SerializeField] private List<AudioClip> _sfxClipList;      // SFX 오디오 클립 리스트
    [SerializeField] private Transform _sfxSourcePoolParent;    // SFX 오디오 소스 풀 생성할 Transform
    [SerializeField] private int _poolCount = 20;               // 생성할 풀 갯수
    private Queue<AudioSource> _sfxSourcePool = new Queue<AudioSource> ();  // SFX 오디오소스 풀
    private Dictionary<SFXType, AudioClip> _sfxClipDict = new Dictionary<SFXType, AudioClip>(); // SFX 오디오 클립 딕셔너리

    /// <summary>
    /// Awake
    /// </summary>
    protected override void Awake()
    {
        base.Awake();

        Set_BGMClipDict();
        Set_SFXClipDict();
        Fill_SFXSourcePool();
    }

    #region BGM
    /// <summary>
    /// BGM 플레이
    /// </summary>
    public void PlayBGM(BGMType bgmType)
    {
        if (_bgmClipDict.ContainsKey(bgmType) == false)
            return;

        _bgmSource.outputAudioMixerGroup = _bgmMixerGroup;
        _bgmSource.clip = _bgmClipDict[bgmType];
        _bgmSource.loop = true;
        _bgmSource.Play();
    }

    /// <summary>
    /// BGM 볼륨 조절 (매개변수값을 데시벨로 변환 후 bgm볼륨으로 조정 (매개변수는 0~1사이의 값))
    /// </summary>
    public void SetBGMVolume(float volume)
    {
        float db;

        if (volume < 0)
            db = -80f; // 음소거 처리 (AudioMixer의 최소값 (무음))
        else
            db = Mathf.Log10(volume) * 20; // 데시벨로 변환 (공식)

        _baseMixer.SetFloat("BGMVolume", db); // BGM그룹 볼륨 조절 (파라미터명 BGMVolume)
    }

    /// <summary>
    /// BGM클립 딕셔너리 셋팅
    /// </summary>
    private void Set_BGMClipDict()
    {
        foreach (AudioClip bgmClip in _bgmClipList)
        {
            string name = bgmClip.name;
            if (Enum.TryParse(name, out BGMType bgmType))
                _bgmClipDict[bgmType] = bgmClip;
            else
                Debug.Log($"{name} 오디오클립 딕셔너리로 변환실패!");
        }
    }
    #endregion

    #region SFX
    /// <summary>
    /// SFX 플레이
    /// </summary>
    public void PlaySFX(SFXType sfxType)
    {
        if (_sfxClipDict.ContainsKey(sfxType) == false)
            return;

        AudioSource sfxSource = _sfxSourcePool.Count > 0 ? _sfxSourcePool.Dequeue() : Fill_SFXSourcePool();
        sfxSource.outputAudioMixerGroup = _sfxMixerGroup;
        sfxSource.clip = _sfxClipDict[sfxType];
        sfxSource.gameObject.SetActive(true);
        sfxSource.Play();

        StartCoroutine(ReturnSFXAsync(sfxSource));
    }

    /// <summary>
    /// SFX 오디오 소스 풀 채우기
    /// </summary>
    private AudioSource Fill_SFXSourcePool()
    {
        for (int i = 0; i < _poolCount; i++)
        {
            AudioSource sfxSource = Instantiate(_sfxSource, _sfxSourcePoolParent);
            sfxSource.outputAudioMixerGroup = _sfxMixerGroup;
            sfxSource.gameObject.SetActive(false);
            _sfxSourcePool.Enqueue(sfxSource);
        }

        return _sfxSourcePool.Dequeue();
    }

    /// <summary>
    /// SFX 오디오 소스 다시 풀에 반환
    /// </summary>
    private IEnumerator ReturnSFXAsync(AudioSource sfxSource)
    {
        yield return new WaitForSeconds(sfxSource.clip.length);

        sfxSource.gameObject.SetActive(false);
        _sfxSourcePool.Enqueue(sfxSource);
    }

    /// <summary>
    /// SFX 볼륨 조절 (매개변수값을 데시벨로 변환 후 sfx볼륨으로 조정 (매개변수는 0~1사이의 값))
    /// </summary>
    public void SetSFXVolume(float volume)
    {
        float db;

        if (volume < 0)
            db = -80f; // 음소거 처리 (AudioMixer의 최소값 (무음))
        else
            db = Mathf.Log10(volume) * 20; // 데시벨로 변환 (공식)

        _baseMixer.SetFloat("SFXVolume", db); // SFX그룹 볼륨 조절 (파라미터명 SFXVolume)
    }

    /// <summary>
    /// SFX클립 딕셔너리 셋팅
    /// </summary>
    private void Set_SFXClipDict()
    {
        foreach (AudioClip sfxClip in _sfxClipList)
        {
            string name = sfxClip.name;
            if (Enum.TryParse(name, out SFXType sfxType))
                _sfxClipDict[sfxType] = sfxClip;
            else
                Debug.Log($"{name} 오디오클립 딕셔너리로 변환실패!");
        }
    }
    #endregion
}
