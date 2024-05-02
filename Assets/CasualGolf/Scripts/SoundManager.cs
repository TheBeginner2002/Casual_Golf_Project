using UnityEngine;
public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    public AudioSource fxSource;
    public AudioClip gameOverFx, gameCompleteFx, shotFx;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    public void PlayFx(FxTypes fxTypes)
    {
        switch (fxTypes)
        {
            case FxTypes.Gameoverfx:
                fxSource.PlayOneShot(gameOverFx);
                break;
            case FxTypes.Gamecompletefx:
                fxSource.PlayOneShot(gameCompleteFx);
                break;
            case FxTypes.Shotfx:
                fxSource.PlayOneShot(shotFx);
                break;
        }
    }
}

public enum FxTypes
{
    Gameoverfx, 
    Gamecompletefx, 
    Shotfx
}
