using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolAudioSource : MonoBehaviour
{
    [SerializeField] private int _poolSize = 3;
    private Queue<AudioSource> _audioSources = new();
    private List<AudioSource> _activeAudioSources = new();

    public void Initialized()
    {
        CreatePool();
    }

    public void CreatePool()
    {
        for ( int i = 0; i < _poolSize; i++ )
        {
            AudioSource newAudioSource = gameObject.AddComponent<AudioSource>();
            newAudioSource.playOnAwake = false;
            _audioSources.Enqueue( newAudioSource );
        }
    }

    public AudioSource GetAudioSource()
    {
 
        int initialCount = _audioSources.Count;
        for ( int i = 0; i < initialCount; i++ )
        {
            AudioSource source = _audioSources.Dequeue();
            if ( !source.isPlaying )
            {
                _activeAudioSources.Add( source );
                return source;
            }
            _audioSources.Enqueue( source );
        }

  
        if ( _activeAudioSources.Count > 0 )
        {
            AudioSource oldestSource = _activeAudioSources[ 0 ];
            _activeAudioSources.RemoveAt( 0 );
            oldestSource.Stop();
            oldestSource.clip = null;
            _activeAudioSources.Add( oldestSource );
            return oldestSource;
        }

    
        return null;
    }

    public void ReturnAudioSource( AudioSource source )
    {
        source.Stop();
      
        _activeAudioSources.Remove( source );
        _audioSources.Enqueue( source );
    }
}
