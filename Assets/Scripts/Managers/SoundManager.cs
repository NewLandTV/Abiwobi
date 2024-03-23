using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;
}

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    // Sounds
    [SerializeField]
    public Sound[] bgmSounds;
    [SerializeField]
    public Sound[] sfxSounds;

    // Audio source
    [SerializeField]
    private AudioSource bgmPlayer;
    [SerializeField]
    private AudioSource[] sfxPlayers;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;

            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // static 변수 초기화
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
    private static void Init()
    {
        instance = null;
    }

    #region Bgm

    public void PlayBgm(string bgmName, bool loop)
    {
        for (int i = 0; i < bgmSounds.Length; i++)
        {
            if (bgmSounds[i].name.Equals(bgmName))
            {
                bgmPlayer.clip = bgmSounds[i].clip;
                bgmPlayer.loop = loop;

                bgmPlayer.Play();

                return;
            }
        }
    }

    public void PauseBgm()
    {
        bgmPlayer.Pause();
    }

    public void StopBgm()
    {
        bgmPlayer.Stop();
    }

    #endregion

    #region Sfx

    public void PlaySfx(string sfxName)
    {
        for (int i = 0; i < sfxSounds.Length; i++)
        {
            if (sfxSounds[i].name.Equals(sfxName))
            {
                for (int j = 0; j < sfxPlayers.Length; j++)
                {
                    if (!sfxPlayers[j].isPlaying)
                    {
                        sfxPlayers[j].clip = sfxSounds[i].clip;

                        sfxPlayers[j].Play();

                        return;
                    }
                }

                return;
            }
        }
    }

    public void PauseAllSfx()
    {
        for (int i = 0; i < sfxPlayers.Length; i++)
        {
            sfxPlayers[i].Pause();
        }
    }

    public void StopAllSfx()
    {
        for (int i = 0; i < sfxPlayers.Length; i++)
        {
            sfxPlayers[i].Stop();
        }
    }

    #endregion
}
