using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    public GameObject ballPrefab; 
    public Vector3 ballSpawnPos;

    public LevelData[] levelDatas;

    private int shotCount = 0;

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
    }

    public void SpawnLevel(int levelIndex)
    {
        Instantiate(levelDatas[levelIndex].levelPrefab, Vector3.zero, Quaternion.identity);
        shotCount = levelDatas[levelIndex].shotCount;
        UIManager.instance.ShotText.text = shotCount.ToString();
        GameObject ball = Instantiate(ballPrefab, ballSpawnPos, Quaternion.identity);
        CameraFollow.instance.SetTarget(ball); 
        GameManager.instance.gameStatus = GameStatus.Playing;
    }

    public void DestroyLevel(int levelIndex)
    {
        Destroy(GameObject.Find(levelDatas[levelIndex].levelPrefab.name+"(Clone)"));
        Destroy(GameObject.Find("Ball(Clone)"));
        CameraFollow.instance.transform.position = Vector3.zero;
    }
    
    public void ShotTaken()
    {
        if (shotCount > 0)
        {
            shotCount--;
            UIManager.instance.ShotText.text = "" + shotCount;

            if (shotCount <= 0)
            {
                LevelFailed();
            }
        }
    }
    
    public void LevelFailed()
    {
        if (GameManager.instance.gameStatus == GameStatus.Playing)
        {
            GameManager.instance.gameStatus = GameStatus.Failed;
            UIManager.instance.GameResult();
        }
    }
    
    public void LevelComplete()
    {
        if (GameManager.instance.gameStatus == GameStatus.Playing)
        {
            if (GameManager.instance.currentLevelIndex < levelDatas.Length)    
            {
                GameManager.instance.currentLevelIndex++;
            }
            else
            {
                GameManager.instance.currentLevelIndex = 0;
            }
            GameManager.instance.gameStatus = GameStatus.Complete;
            UIManager.instance.GameResult();
        }
    }
}
