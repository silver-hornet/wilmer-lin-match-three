using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    public AudioClip[] musicClips;
    public AudioClip[] winClips;
    public AudioClip[] loseClips;
    public AudioClip[] bonusClips;

    public string musicSourceName = "BackgroundMusic";

    [Range(0, 1)] public float musicVolume = 0.5f;
    [Range(0, 1)] public float fxVolume = 1.0f;

    public float lowPitch = 0.95f;
    public float highPitch = 1.05f;

    void Start()
    {
        PlayMusic(true);
    }

    public AudioSource PlayClipAtPoint(AudioClip clip, Vector3 position, float volume = 1f, bool randomizePitch = true, bool selfdestruct = true)
    {
        if (clip != null)
        {
            // create a new GameObject at the specified world space position
            GameObject go = new GameObject("SoundFX" + clip.name);
            go.transform.position = position;

            // add an AudioSource component and set the AudioClip
            AudioSource source = go.AddComponent<AudioSource>();
            source.clip = clip;

            // change the pitch of the sound within some variation
            if (randomizePitch)
            {
                float randomPitch = Random.Range(lowPitch, highPitch);
                source.pitch = randomPitch;
            }

            // set the volume
            source.volume = volume;

            // play the sound
            source.Play();

            // destroy the AudioSource after the clip is done playing
            if (selfdestruct)
            {
                Destroy(go, clip.length);
            }

            // return our AudioSource out of the method
            return source;
        }

        return null;
    }

    public AudioSource PlayRandom(AudioClip[] clips, Vector3 position, float volume = 1f, bool randomizePitch = true, bool selfdestruct = true)
    {
        if (clips != null)
        {
            if (clips.Length != 0)
            {
                int randomIndex = Random.Range(0, clips.Length);

                if (clips[randomIndex] != null)
                {
                    AudioSource source = PlayClipAtPoint(clips[randomIndex], position, volume, randomizePitch, selfdestruct);
                    return source;
                }
            }
        }

        return null;
    }

    public void PlayMusic(bool dontDestroyOnLoad)
    {
        GameObject musicObject = GameObject.Find(musicSourceName);

        if (musicObject == null)
        {
            AudioSource source = PlayRandom(musicClips, Vector3.zero, musicVolume, false, false);
            source.loop = true;
            source.gameObject.name = musicSourceName;

            if (dontDestroyOnLoad && source != null)
            {
                DontDestroyOnLoad(source.gameObject);
            }
        }
    }

    public void PlayWinSound()
    {
        PlayRandom(winClips, Vector3.zero, fxVolume);
    }

    public void PlayLoseSound()
    {
        PlayRandom(loseClips, Vector3.zero, fxVolume * 0.5f);
    }

    public void PlayBonusSound()
    {
        PlayRandom(bonusClips, Vector3.zero, fxVolume);
    }
}
