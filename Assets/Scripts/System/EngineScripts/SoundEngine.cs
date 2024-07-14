using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.VisualScripting.Member;


public class SoundEngine : MonoBehaviour
{
 
    private static SoundEngine _instance;

    [SerializeField] private PoolAudioSource _poolSFXloop;

    [SerializeField] private AudioSource _sourceMusic; // для музыки
    [SerializeField] private AudioSource _sourceSFX; // для звуковых эффектов
    [SerializeField] private AudioSource _sourceUI; // для звуков UI

    private List<AudioSource> _activeSFXSources = new();

    public static SoundEngine Instance
    {
        get
        {
            if ( _instance == null )
            {
                _instance = FindObjectOfType<SoundEngine>();
                if ( _instance == null )
                {
                    GameObject soundEngineObject = new GameObject( "SoundEngine" );
                    _instance = soundEngineObject.AddComponent<SoundEngine>();
                }
            }
            return _instance;
        }
    }

    private void Awake()
    {
        if ( _poolSFXloop == null )
        {
            _poolSFXloop = GetComponent<PoolAudioSource>();
            _poolSFXloop.Initialized();
        }

        if ( _instance != null && _instance != this )
        {
            Destroy( gameObject );
            return;
        }

        _instance = this;
        DontDestroyOnLoad( gameObject );

        InitializeAudioSource( ref _sourceMusic , true );
      //  InitializeAudioSource( ref _sourceSFX , false );
        //InitializeAudioSource( ref _sourceUI , false );
    }

    private void InitializeAudioSource( ref AudioSource source , bool loop )
    {
        if ( source == null )
        {
            source = gameObject.AddComponent<AudioSource>();
            source.loop = loop;
            source.playOnAwake = false;
        }
    }

    public void PlaySound( AudioClip clip , SoundType soundType , bool loop = false )
    {
        switch ( soundType )
        {
            case SoundType.Music:
                PlayMusic( clip );
                break;
            case SoundType.SFX:
                PlaySFX( clip , loop );
                break;
            case SoundType.UI:
                PlayUI( clip );
                break;
        }
    }

    public void StopSound( SoundType soundType , AudioClip clip = null )
    {
        switch ( soundType )
        {
            case SoundType.Music:
                _sourceMusic.Stop();
                break;
            case SoundType.SFX:
                StopSFX( clip );
                break;
            case SoundType.UI:
                _sourceUI.Stop();
                break;
        }
    }

    public void SetVolume( float volume , SoundType soundType )
    {
        switch ( soundType )
        {
            case SoundType.Music:
                _sourceMusic.volume = volume;
                break;
            case SoundType.SFX:
               
                _sourceSFX.volume = volume;
                break;
            case SoundType.UI:
                _sourceUI.volume = volume;
                break;
        }
    }

    private void PlayMusic( AudioClip clip )
    {
        _sourceMusic.clip = clip;
        _sourceMusic.Play();
    }

    private void PlaySFX( AudioClip clip , bool loop )
    {
        AudioSource source = _poolSFXloop.GetAudioSource();
        source.clip = clip;
        source.loop = loop;
        source.Play();
        _activeSFXSources.Add( source );
        StartCoroutine( ReturnToPool( source ) );
    }

    public void StopSFX( AudioClip clip )
    {
        List<AudioSource> sourcesToRemove = new List<AudioSource>();

        foreach ( var source in _activeSFXSources )
        {
            if ( source.clip == clip )
            {
               
              
                sourcesToRemove.Add( source );
                _poolSFXloop.ReturnAudioSource( source );
            }
        }

        foreach ( var source in sourcesToRemove )
        {
            _activeSFXSources.Remove( source );
        }
    }

    private void PlayUI( AudioClip clip )
    {
        _sourceUI.PlayOneShot( clip );
    }

    private IEnumerator ReturnToPool( AudioSource source )
    {
        yield return new WaitWhile( () => source.isPlaying );
        _activeSFXSources.Remove( source );
        _poolSFXloop.ReturnAudioSource( source );
    }


    
}

