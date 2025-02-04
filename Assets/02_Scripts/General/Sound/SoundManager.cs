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
    [SerializeField] private AudioMixer _baseMixer;             // ���̽� ����� �ͼ�

    [Title("BGM", bold: false)]
    [SerializeField] private AudioMixerGroup _bgmMixerGroup;    // BGM ����� �ͼ� �׷�
    [SerializeField] private AudioSource _bgmSource;            // BGM ����� �ҽ�
    [SerializeField] private List<AudioClip> _bgmClipList;      // BGM ����� Ŭ�� ����Ʈ
    private Dictionary<BGMType, AudioClip> _bgmClipDict = new Dictionary<BGMType, AudioClip>(); // BGM ����� Ŭ�� ��ųʸ� 

    [Title("SFX", bold: false)]
    [SerializeField] private AudioMixerGroup _sfxMixerGroup;    // SFX ����� �ͼ� �׷�
    [SerializeField] private AudioSource _sfxSource;            // SFX ����� �ҽ�
    [SerializeField] private List<AudioClip> _sfxClipList;      // SFX ����� Ŭ�� ����Ʈ
    [SerializeField] private Transform _sfxSourcePoolParent;    // SFX ����� �ҽ� Ǯ ������ Transform
    [SerializeField] private int _poolCount = 20;               // ������ Ǯ ����
    private Queue<AudioSource> _sfxSourcePool = new Queue<AudioSource> ();  // SFX ������ҽ� Ǯ
    private Dictionary<SFXType, AudioClip> _sfxClipDict = new Dictionary<SFXType, AudioClip>(); // SFX ����� Ŭ�� ��ųʸ�

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
    /// BGM �÷���
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
    /// BGM ���� ���� (�Ű��������� ���ú��� ��ȯ �� bgm�������� ���� (�Ű������� 0~1������ ��))
    /// </summary>
    public void SetBGMVolume(float volume)
    {
        float db;

        if (volume < 0)
            db = -80f; // ���Ұ� ó�� (AudioMixer�� �ּҰ� (����))
        else
            db = Mathf.Log10(volume) * 20; // ���ú��� ��ȯ (����)

        _baseMixer.SetFloat("BGMVolume", db); // BGM�׷� ���� ���� (�Ķ���͸� BGMVolume)
    }

    /// <summary>
    /// BGMŬ�� ��ųʸ� ����
    /// </summary>
    private void Set_BGMClipDict()
    {
        foreach (AudioClip bgmClip in _bgmClipList)
        {
            string name = bgmClip.name;
            if (Enum.TryParse(name, out BGMType bgmType))
                _bgmClipDict[bgmType] = bgmClip;
            else
                Debug.Log($"{name} �����Ŭ�� ��ųʸ��� ��ȯ����!");
        }
    }
    #endregion

    #region SFX
    /// <summary>
    /// SFX �÷���
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
    /// SFX ����� �ҽ� Ǯ ä���
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
    /// SFX ����� �ҽ� �ٽ� Ǯ�� ��ȯ
    /// </summary>
    private IEnumerator ReturnSFXAsync(AudioSource sfxSource)
    {
        yield return new WaitForSeconds(sfxSource.clip.length);

        sfxSource.gameObject.SetActive(false);
        _sfxSourcePool.Enqueue(sfxSource);
    }

    /// <summary>
    /// SFX ���� ���� (�Ű��������� ���ú��� ��ȯ �� sfx�������� ���� (�Ű������� 0~1������ ��))
    /// </summary>
    public void SetSFXVolume(float volume)
    {
        float db;

        if (volume < 0)
            db = -80f; // ���Ұ� ó�� (AudioMixer�� �ּҰ� (����))
        else
            db = Mathf.Log10(volume) * 20; // ���ú��� ��ȯ (����)

        _baseMixer.SetFloat("SFXVolume", db); // SFX�׷� ���� ���� (�Ķ���͸� SFXVolume)
    }

    /// <summary>
    /// SFXŬ�� ��ųʸ� ����
    /// </summary>
    private void Set_SFXClipDict()
    {
        foreach (AudioClip sfxClip in _sfxClipList)
        {
            string name = sfxClip.name;
            if (Enum.TryParse(name, out SFXType sfxType))
                _sfxClipDict[sfxType] = sfxClip;
            else
                Debug.Log($"{name} �����Ŭ�� ��ųʸ��� ��ȯ����!");
        }
    }
    #endregion
}
