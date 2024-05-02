using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [HideInInspector]
    public int currentLevelIndex;
    [HideInInspector]
    public GameStatus gameStatus = GameStatus.None;

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
}

[System.Serializable]
public enum GameStatus
{
    None,
    Playing,
    Pause,
    Failed,
    Complete
}