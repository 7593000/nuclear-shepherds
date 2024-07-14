using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEngine : MonoBehaviour
{
    private static SoundEngine _instance;

    [SerializeField] private PoolAudioSource _poolAdioSource;
    [SerializeField] private AudioSource _sourceMusic; // для музыки
    [SerializeField] private AudioSource _sourceSFX; // для звуковых эффектов
    [SerializeField] private AudioSource _sourceUI; // для звуков UI

    [SerializeField]
    private float _maxDistance = 50.0f;
    [SerializeField]
    private float _minDistance = 1.0f;
    private Transform _cameraTransform;
   
    private Dictionary<Transform , AudioSource> _activeSource = new();
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
        _cameraTransform = Camera.main.transform;
       
        if ( _poolAdioSource == null )
        {
            _poolAdioSource = GetComponent<PoolAudioSource>();
            _poolAdioSource.Initialized();
        }

        if ( _instance != null && _instance != this )
        {
            Destroy( gameObject );
            return;
        }

        _instance = this;
        DontDestroyOnLoad( gameObject );
        StartCoroutine( UpdateDistanceTarget() );
    }

    public void PlaySound( AudioClip clip , SoundType soundType , bool loop = false , Transform target = null )
    {
        switch ( soundType )
        {
            case SoundType.Music:
                PlayMusic( clip );
                break;
            case SoundType.SFX:
                PlaySFX( clip , loop , target );
                break;
            case SoundType.SFXPlayOne:
                PlayOneShot( clip , target );
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

    private void PlaySFX( AudioClip clip , bool loop , Transform target )
    {
        AudioSource source = _poolAdioSource.GetAudioSource();
        if (   target != null )
        {
            _activeSource[ target ] = source;
        }
        
        source.clip = clip;
        source.loop = loop;
        source.Play();
        _activeSFXSources.Add( source );

      

        StartCoroutine( ReturnToPool( source , target ) );
    }

    private void PlayOneShot( AudioClip clip , Transform target )
    {
        _sourceSFX.volume = GetDistanceForValue( target );
        _sourceSFX.PlayOneShot( clip );
    }

    public void StopSFX( AudioClip clip )
    {
        List<AudioSource> sourcesToRemove = new List<AudioSource>();

        foreach ( AudioSource source in _activeSFXSources )
        {
            if ( source.clip == clip )
            {
                sourcesToRemove.Add( source );
                _poolAdioSource.ReturnAudioSource( source );
            }
        }

        foreach ( AudioSource source in sourcesToRemove )
        {
            _activeSFXSources.Remove( source );
        }
    }

    private void PlayUI( AudioClip clip )
    {
        _sourceUI.PlayOneShot( clip );
    }

    private IEnumerator ReturnToPool( AudioSource source , Transform target )
    {
        yield return new WaitWhile( () => source.isPlaying );

        if ( target != null && _activeSource.ContainsKey( target ) )
        {
            _activeSource.Remove( target );
        }

        _activeSFXSources.Remove( source );
        _poolAdioSource.ReturnAudioSource( source );
    }

    private IEnumerator UpdateDistanceTarget()
    {
        while ( true )
        {
            foreach ( var item in _activeSource )
            {
                Transform target = item.Key;
                AudioSource source = item.Value;

                if ( target.gameObject.activeSelf )
                {
                    float distance = Vector3.Distance( target.position , _cameraTransform.position );
                    float volume = Mathf.Clamp01( 1 - ( distance - _minDistance ) / ( _maxDistance - _minDistance ) );
                    source.volume = volume;
                }
            }
            yield return new WaitForSeconds( 0.5f );  
        }
    }

    private float GetDistanceForValue( Transform target )
    {
        float distance = Vector3.Distance( target.position , _cameraTransform.position );
        return Mathf.Clamp01( 1 - ( distance - _minDistance ) / ( _maxDistance - _minDistance ) );
    }
}
