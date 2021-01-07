using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameControllerTwo : MonoBehaviour
{
    public GameObject wGirlPfb;
    public GameObject pyroPfb;
    public GameObject subZeroPfb;
    public GameObject huntressPfb;

    public GameObject level;
    public Camera theCamera;
    public Transform ground;

    public GameObject startMenu;
    public GameObject winScreen;
    public GameObject pauseMenu;
    public GameObject selectScreen;

    public GameObject p1Health;
    public GameObject p1Magic;
    public GameObject p2Health;
    public GameObject p2Magic;
    Slider p1Slider;
    Slider p2Slider;

    public Text winnerText;
    public Text selectScreenText;
    public RawImage winImage;

    public Button wGirlButton;
    public Button pyroButton;
    public Button subZeroButton;
    public Button huntressButton;

    public Texture wGirlT;
    public Texture pyroT;
    public Texture sZeroT;
    public Texture huntressT;

    public GameObject audioManager;
    public AudioClip menuTheme;
    public AudioClip fightTheme;
    AudioManagerScript songScript;

    bool gameEnded;
    int p1Char, p2Char;

    // Start is called before the first frame update
    void Start()
    {
        gameEnded = true;

        songScript = audioManager.GetComponent<AudioManagerScript>();
        songScript.ChangeBGM(menuTheme);

        p1Slider = p1Health.GetComponent<Slider>();
        p2Slider = p2Health.GetComponent<Slider>();

        startMenu.SetActive(true);
        selectScreen.SetActive(false);
        pauseMenu.SetActive(false);
        winScreen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && gameEnded == false)
        {
            songScript.pauseBGM();
            Time.timeScale = 0;
            pauseMenu.SetActive(true);
        }

        if (p1Slider.value == 0 && gameEnded == false)
        {
            endGame(p2Char);
            gameEnded = true;
        }

        if (p2Slider.value == 0 && gameEnded == false)
        {
            endGame(p1Char);
            gameEnded = true;
        }
    }

    public void startTwoPGame(int p1, int p2)
    {
        gameEnded = false;
        p1Char = p1;
        p2Char = p2;
        Time.timeScale = 1;
        startMenu.SetActive(false);
        selectScreen.SetActive(false);

        p1Health.SetActive(true);
        p1Magic.SetActive(true);
        p2Health.SetActive(true);
        p2Magic.SetActive(true);

        Vector3 playerOneOffset = new Vector3(-4.0f, 1.0f, 5.0f);
        Vector3 playerTwoOffset = new Vector3(4.0f, 1.0f, 5.0f);

        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject pl in players)
        {
            Destroy(pl);
        }
        

        if (p1Char == 1)
        {
            var playerOne = (GameObject)Instantiate(pyroPfb, level.transform.position + playerOneOffset, pyroPfb.transform.rotation);
        }
        else if (p1Char == 2)
        {
            var playerOne = (GameObject)Instantiate(subZeroPfb, level.transform.position + playerOneOffset, subZeroPfb.transform.rotation);
        }
        else if (p1Char == 3)
        {
            var playerOne = (GameObject)Instantiate(wGirlPfb, level.transform.position + playerOneOffset, wGirlPfb.transform.rotation);
        }
        else if (p1Char == 4)
        {
            var playerOne = (GameObject)Instantiate(huntressPfb, level.transform.position + playerOneOffset, huntressPfb.transform.rotation);
        }

        if (p2Char == 1)
        {
            var playerTwo = (GameObject)Instantiate(pyroPfb, level.transform.position + playerTwoOffset, pyroPfb.transform.rotation);
        }
        else if (p2Char == 2)
        {
            var playerTwo = (GameObject)Instantiate(subZeroPfb, level.transform.position + playerTwoOffset, subZeroPfb.transform.rotation);
        }
        else if (p2Char == 3)
        {
            var playerTwo = (GameObject)Instantiate(wGirlPfb, level.transform.position + playerTwoOffset, wGirlPfb.transform.rotation);
        }
        else if (p2Char == 4)
        {
            var playerTwo = (GameObject)Instantiate(huntressPfb, level.transform.position + playerTwoOffset, huntressPfb.transform.rotation);
        }
        songScript.ChangeBGM(fightTheme);
    }

    public void goSelectTwoP()
    {
        pyroButton.onClick.RemoveAllListeners();
        wGirlButton.onClick.RemoveAllListeners();
        subZeroButton.onClick.RemoveAllListeners();
        huntressButton.onClick.RemoveAllListeners();
        startMenu.SetActive(false);
        selectScreen.SetActive(true);
        selectScreenText.text = "P1: Select Player";
        pyroButton.onClick.AddListener(() => playerTwoSelect(1));
        subZeroButton.onClick.AddListener(() => playerTwoSelect(2));
        wGirlButton.onClick.AddListener(() => playerTwoSelect(3));
        huntressButton.onClick.AddListener(() => playerTwoSelect(4));
    }

    public void playerTwoSelect(int p1)
    {
        pyroButton.onClick.RemoveAllListeners();
        wGirlButton.onClick.RemoveAllListeners();
        subZeroButton.onClick.RemoveAllListeners();
        huntressButton.onClick.RemoveAllListeners();
        Debug.Log(p1);
        selectScreenText.text = "P2: Select Player";
        pyroButton.onClick.AddListener(() => startTwoPGame(p1, 1));
        subZeroButton.onClick.AddListener(() => startTwoPGame(p1, 2));
        wGirlButton.onClick.AddListener(() => startTwoPGame(p1, 3));
        huntressButton.onClick.AddListener(() => startTwoPGame(p1, 4));
    }

    public void endGame(int winner)
    {
        if (winner == 1)
        {
            winnerText.text = "Winner: Pyro";
            winImage.texture = pyroT;
        }
        else if (winner == 2)
        {
            winnerText.text = "Winner: Sub-Zero";
            winImage.texture = sZeroT;
        }
        else if (winner == 3)
        {
            winnerText.text = "Winner: Witch Girl";
            winImage.texture = wGirlT;
        }
        else if (winner == 4)
        {
            winnerText.text = "Winner: Huntress";
            winImage.texture = huntressT;
        }
        winScreen.SetActive(true);
        p1Health.SetActive(false);
        p1Magic.SetActive(false);
        p2Health.SetActive(false);
        p2Magic.SetActive(false);
        p1Slider.value = 10;
        p2Slider.value = 10;
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject pl in players)
        {
            Destroy(pl);
        }
        songScript.ChangeBGM(menuTheme);
    }

    public void goMenu()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject pl in players)
        {
            Destroy(pl);
        }

        winScreen.SetActive(false);
        startMenu.SetActive(true);
        songScript.ChangeBGM(menuTheme);
    }

    public void quitGame()
    {
        Application.Quit();
    }

    public void resumeGame()
    {
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
        songScript.resumeBGM();
    }

    public void restartGame()
    {
        winScreen.SetActive(false);
        pauseMenu.SetActive(false);
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject pl in players)
        {
            Destroy(pl);
        }
        startTwoPGame(p1Char, p2Char);
        songScript.ChangeBGM(fightTheme);
    }

    public void endAndMenu()
    {
        pauseMenu.SetActive(false);
        startMenu.SetActive(true);
        p1Health.SetActive(false);
        p1Magic.SetActive(false);
        p2Health.SetActive(false);
        p2Magic.SetActive(false);
        p1Slider.value = 10;
        p2Slider.value = 10;

        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject pl in players)
        {
            Destroy(pl);
        }
        songScript.ChangeBGM(menuTheme);
    }

}
