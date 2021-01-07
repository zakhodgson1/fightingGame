using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GameObject playerPfb;
    public GameObject dummyPfb;

    public GameObject wGirlPfb;
    public GameObject pyroPfb;
    public GameObject subZeroPfb;
    public GameObject huntressPfb;

    public Camera theCamera;

    public Transform ground;

    public GameObject startMenu;
    public GameObject winScreen;
    public GameObject pauseMenu;
    public GameObject selectScreen;

    public GameObject p1Health;
    public GameObject p1Magic;
    public GameObject enemyHealth;
    public GameObject enemyMagic;

    public GameObject level;

    public Button wGirlButton;
    public Button pyroButton;
    public Button subZeroButton;
    public Button huntressButton;

    Slider p1Slider;
    Slider enemySlider;

    public Text winnerText;
    public Text selectScreenText;
    public RawImage winImage;

    public Texture wGirlT;
    public Texture pyroT;
    public Texture sZeroT;
    public Texture huntressT;
    public Texture cpuWin;

    public GameObject audioManager;
    public AudioClip menuTheme;
    public AudioClip fightTheme;

    AudioManagerScript songScript;


    int selectedCharacter;
    bool gameEnded;

    // Start is called before the first frame update
    void Start()
    {
        songScript = audioManager.GetComponent<AudioManagerScript>();
        songScript.ChangeBGM(menuTheme);

        p1Slider = p1Health.GetComponent<Slider>();
        enemySlider = enemyHealth.GetComponent<Slider>();
        gameEnded = true;

        startMenu.SetActive(true);
        selectScreen.SetActive(false);
        pauseMenu.SetActive(false);
        winScreen.SetActive(false);
        p1Health.SetActive(false);
        p1Magic.SetActive(false);
        enemyHealth.SetActive(false);
        enemyMagic.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && gameEnded == false)
        {
            songScript.pauseBGM();
            Time.timeScale = 0;
            pauseMenu.SetActive(true);
        }
        if(p1Slider.value == 0 && gameEnded == false)
        {
            endGame(5);
            gameEnded = true;
        }
        if(enemySlider.value == 0 && gameEnded == false)
        {
            endGame(selectedCharacter);
            gameEnded = true;
        }
    }

    public void startGame(int character)
    {
        selectedCharacter = character;
        gameEnded = false;
        Time.timeScale = 1;
        startMenu.SetActive(false);
        selectScreen.SetActive(false);
        p1Health.SetActive(true);
        p1Magic.SetActive(true);
        enemyHealth.SetActive(true);
        enemyMagic.SetActive(true);

        Vector3 playerOffset = new Vector3(-4.0f, 1.0f, 5.0f);

        Vector3 dummyOffset = new Vector3(4.0f, 4.0f, 5.0f);

        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject pl in players)
        {
            Destroy(pl);
        }
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject pl in enemies)
        {
            Destroy(pl);
        }


        if (character == 1)
        {
            var activePlayer = (GameObject)Instantiate(pyroPfb, level.transform.position + playerOffset, playerPfb.transform.rotation);
        } else if(character == 2)
        {
            var activePlayer = (GameObject)Instantiate(subZeroPfb, level.transform.position + playerOffset, playerPfb.transform.rotation);
        } else if(character == 3)
        {
            var activePlayer = (GameObject)Instantiate(wGirlPfb, level.transform.position + playerOffset, playerPfb.transform.rotation);
        } else if(character == 4)
        {
            var activePlayer = (GameObject)Instantiate(huntressPfb, level.transform.position + playerOffset, playerPfb.transform.rotation);
        }

        var testDummy = (GameObject)Instantiate(dummyPfb, level.transform.position + dummyOffset, dummyPfb.transform.rotation);
        songScript.ChangeBGM(fightTheme);
    }

    public void startTwoPGame(int p1, int p2)
    {
        gameEnded = false;
        Time.timeScale = 1;
        startMenu.SetActive(false);
        selectScreen.SetActive(false);
        p1Health.SetActive(true);
        p1Magic.SetActive(true);
        enemyHealth.SetActive(true);
        enemyMagic.SetActive(true);

        Vector3 playerOneOffset = new Vector3(-4.0f, 1.0f, 5.0f);
        Vector3 playerTwoOffset = new Vector3(4.0f, 1.0f, 5.0f);

        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject pl in players)
        {
            Destroy(pl);
        }
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject pl in enemies)
        {
            Destroy(pl);
        }

        if (p1 == 1)
        {
            var playerOne = (GameObject)Instantiate(pyroPfb, level.transform.position + playerOneOffset, playerPfb.transform.rotation);
        }
        else if (p1 == 2)
        {
            var playerOne = (GameObject)Instantiate(subZeroPfb, level.transform.position + playerOneOffset, playerPfb.transform.rotation);
        }
        else if (p1 == 3)
        {
            var playerOne = (GameObject)Instantiate(wGirlPfb, level.transform.position + playerOneOffset, playerPfb.transform.rotation);
        }
        else if (p1 == 4)
        {
            var playerOne = (GameObject)Instantiate(huntressPfb, level.transform.position + playerOneOffset, playerPfb.transform.rotation);
        }

        if (p2 == 1)
        {
            var playerTwo = (GameObject)Instantiate(pyroPfb, level.transform.position + playerTwoOffset, dummyPfb.transform.rotation);
        }
        else if (p2 == 2)
        {
            var playerTwo = (GameObject)Instantiate(subZeroPfb, level.transform.position + playerTwoOffset, dummyPfb.transform.rotation);
        }
        else if (p2 == 3)
        {
            var playerTwo = (GameObject)Instantiate(wGirlPfb, level.transform.position + playerTwoOffset, dummyPfb.transform.rotation);
        }
        else if (p2 == 4)
        {
            var playerTwo = (GameObject)Instantiate(huntressPfb, level.transform.position + playerTwoOffset, dummyPfb.transform.rotation);
        }
        songScript.ChangeBGM(fightTheme);
    }

    public void quitGame()
    {
        Application.Quit();
    }

    public void goMenu()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject pl in players)
        {
            Destroy(pl);
        }
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject pl in enemies)
        {
            Destroy(pl);
        }

        winScreen.SetActive(false);
        startMenu.SetActive(true);
        songScript.ChangeBGM(menuTheme);
    }

    public void resumeGame()
    {
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
        songScript.resumeBGM();
    }

    public void goSelect()
    {
        startMenu.SetActive(false);
        selectScreen.SetActive(true);
        pyroButton.onClick.AddListener(() => startGame(1));
        subZeroButton.onClick.AddListener(() => startGame(2));
        wGirlButton.onClick.AddListener(() => startGame(3));
        huntressButton.onClick.AddListener(() => startGame(4));
    }

    public void goSelectTwoP()
    {
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
        selectScreenText.text = "P2: Select Player";
        pyroButton.onClick.AddListener(() => startTwoPGame(p1, 1));
        subZeroButton.onClick.AddListener(() => startTwoPGame(p1, 2));
        wGirlButton.onClick.AddListener(() => startTwoPGame(p1, 3));
        huntressButton.onClick.AddListener(() => startTwoPGame(p1, 4));
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

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject pl in enemies)
        {
            Destroy(pl);
        }

        startGame(selectedCharacter);
        songScript.ChangeBGM(fightTheme);
    }

    public void endAndMenu()
    {
        pauseMenu.SetActive(false);
        startMenu.SetActive(true);
        p1Health.SetActive(false);
        p1Magic.SetActive(false);
        enemyHealth.SetActive(false);
        enemyMagic.SetActive(false);
        p1Slider.value = 10;
        enemySlider.value = 10;


        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject pl in players)
        {
            Destroy(pl);
        }
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject pl in enemies)
        {
            Destroy(pl);
        }

        songScript.ChangeBGM(menuTheme);
    }

    public void endGame(int winner)
    {
        if(winner == 1)
        {
            winnerText.text = "Winner: Pyro";
            winImage.texture = pyroT;
        } else if(winner == 2)
        {
            winnerText.text = "Winner: Sub-Zero";
            winImage.texture = sZeroT;
        } else if(winner == 3)
        {
            winnerText.text = "Winner: Witch Girl";
            winImage.texture = wGirlT;
        } else if(winner == 4)
        {
            winnerText.text = "Winner: Huntress";
            winImage.texture = huntressT;
        } else if(winner == 5)
        {
            winnerText.text = "Winner: CPU";
            winImage.texture = cpuWin;
        }
        winScreen.SetActive(true);
        p1Health.SetActive(false);
        p1Magic.SetActive(false);
        enemyHealth.SetActive(false);
        enemyMagic.SetActive(false);
        p1Slider.value = 10;
        enemySlider.value = 10;
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach( GameObject pl in players)
        {
            Destroy(pl);
        }

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject pl in enemies)
        {
            Destroy(pl);
        }
        songScript.ChangeBGM(menuTheme);
    }
}
