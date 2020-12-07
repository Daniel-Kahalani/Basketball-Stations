using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject mainMenuWindow;

    [SerializeField] private GameObject settingsWindow;

    [SerializeField] private GameObject howToPlayWindow;

    [SerializeField] private GameObject sureWantToQuitGameWindow;

    private void Start()
    {
        enableOnlyMainMenuWindow();
        if (!PlayerPrefs.HasKey("gameDifficulty"))
        {
            PlayerPrefs.SetString("gameDifficulty", "NORMAL");
        }
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.SetString("gameDifficulty", "NORMAL");
    }

    public void OnPlayButtonClick()
    {
        SceneManager.LoadScene("Game");
    }

    public void OnSettingsButtonClick()
    {
        settingsWindow.SetActive(true);
        mainMenuWindow.SetActive(false);
    }

    public void OnHowToPlayButtonClick()
    {
        howToPlayWindow.SetActive(true);
        mainMenuWindow.SetActive(false);
        FindObjectOfType<AudioManager>().Play("ButtonClicked");
    }

    public void OnQuitGameButtonClick()
    {
        sureWantToQuitGameWindow.SetActive(true);
        mainMenuWindow.SetActive(false);
        FindObjectOfType<AudioManager>().Pause("MainMenuMusic");
    }

    public void OnBackButtonClick()
    {
        enableOnlyMainMenuWindow();
    }

    public void OnNormalButtonClick()
    {
        PlayerPrefs.SetString("gameDifficulty", "NORMAL");
    }

    public void OnHardButtonClick()
    {
        PlayerPrefs.SetString("gameDifficulty", "HARD");
    }

    public void OnYesToQuitButtonClick()
    {
        Application.Quit();
    }

    public void OnCancelButtonClick()
    {
        enableOnlyMainMenuWindow();
        FindObjectOfType<AudioManager>().Play("MainMenuMusic");
    }

    private void enableOnlyMainMenuWindow()
    {
        mainMenuWindow.SetActive(true);
        sureWantToQuitGameWindow.SetActive(false);
        settingsWindow.SetActive(false);
        howToPlayWindow.SetActive(false);
    }

    public void OnMusicOnButtonClick()
    {
        FindObjectOfType<AudioManager>().SetMusicVolume(0);
    }

    public void OnMusicOffButtonClick()
    {
        FindObjectOfType<AudioManager>().SetMusicVolume(-80);
    }

    public void OnSoundEffectsOnButtonClick()
    {
        FindObjectOfType<AudioManager>().SetSoundEffectsVolume(0);
    }

    public void OnSoundEffectsOffButtonClick()
    {
        FindObjectOfType<AudioManager>().SetSoundEffectsVolume(-80);
    }
}
