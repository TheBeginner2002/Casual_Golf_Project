using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private Text volumeNumber;
    [SerializeField] private Slider slider;
    [SerializeField] private Image powerBar;
    [SerializeField] private Text shotText;
    [SerializeField] private GameObject mainMenu, gameMenu, settingsMenu, gameOverPanel, retryBtn, nextBtn;
    [SerializeField] private GameObject container, lvlBtnPrefab;

    public Text ShotText { get { return shotText; } }
    public Image PowerBar { get => powerBar; }

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

        audioSource = GameObject.Find("SoundManager").GetComponent<AudioSource>();

        powerBar.fillAmount = 0;
        var volume = audioSource.volume;
        volumeNumber.text = (int)(volume * 100) + " %";
        slider.value = volume;
    }

    void Start()
    {
        if (GameManager.instance.gameStatus == GameStatus.None)
        {
            CreateLevelButtons();
        }
        else if (GameManager.instance.gameStatus == GameStatus.Failed ||
            GameManager.instance.gameStatus == GameStatus.Complete)
        {
            mainMenu.SetActive(false);
            gameMenu.SetActive(true);
            LevelManager.instance.SpawnLevel(GameManager.instance.currentLevelIndex);
        }
    }

    void CreateLevelButtons()
    {
        for (int i = 0; i < LevelManager.instance.levelDatas.Length; i++)
        {
            GameObject buttonObj = Instantiate(lvlBtnPrefab, container.transform);
            buttonObj.transform.GetChild(0).GetComponent<Text>().text = "" + (i + 1);
            Button button = buttonObj.GetComponent<Button>();
            button.onClick.AddListener(() => OnClick(button));
        }
    }

    void OnClick(Button btn)
    {
        mainMenu.SetActive(false);
        gameMenu.SetActive(true);
        GameManager.instance.currentLevelIndex = btn.transform.GetSiblingIndex();
        LevelManager.instance.SpawnLevel(GameManager.instance.currentLevelIndex);
        Destroy(GameObject.Find("LvlButtonPrefab(Clone)"));
    }
    
    public void SliderVolume()
    {
        var volume = slider.value;
        audioSource.volume = volume;
        volumeNumber.text = (int)(volume * 100) + " %";
    }

    public void SettingsMenuOn()
    {
        settingsMenu.SetActive(true);
        GameManager.instance.gameStatus = GameStatus.Pause;
    }
    
    public void SettingsMenuOff()
    {
        settingsMenu.SetActive(false);
        GameManager.instance.gameStatus = GameStatus.Playing;
    }

    public void ReturnToLevel()
    {
        DeleteLevelButton();
        mainMenu.SetActive(true);
        settingsMenu.SetActive(false);
        gameMenu.SetActive(false);
        LevelManager.instance.DestroyLevel(GameManager.instance.currentLevelIndex);
        GameManager.instance.gameStatus = GameStatus.None;
        CreateLevelButtons();
    }

    void DeleteLevelButton()
    {
        for (int i = 0; i < container.transform.childCount - 1; i++)
        {
            Destroy(container.transform.GetChild(i).gameObject);
        }
    }

    public void GameResult()
    {
        switch (GameManager.instance.gameStatus)
        {
            case GameStatus.Complete:
                gameOverPanel.SetActive(true);
                nextBtn.SetActive(true);
                if (SoundManager.instance)
                {
                    SoundManager.instance.PlayFx(FxTypes.Gamecompletefx);
                }
                break;
            case GameStatus.Failed:
                gameOverPanel.SetActive(true);
                retryBtn.SetActive(true);
                if (SoundManager.instance)
                {
                    SoundManager.instance.PlayFx(FxTypes.Gameoverfx);
                }
                break;
        }
    }
    
    public void HomeBtn()
    {
        GameManager.instance.gameStatus = GameStatus.None;
        SceneManager.LoadScene("MainMenuScene");
    }
    
    public void NextRetryBtn()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }


}
