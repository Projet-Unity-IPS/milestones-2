using System;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using Random = System.Random;



public class GameManager : MonoBehaviour
{
    public GameObject player1;
    public GameObject player2;
    public GameObject sheep;
    public GameObject timeTextMesh;
    public GameObject p1ScoreTextMesh;
    public GameObject p2ScoreTextMesh;
    public GameObject endSplashScreen;
    public GameObject player1WinText;
    public GameObject player2WinText;
    public GameObject equalityText;
    public GameObject losePointSound;
    public GameObject winPointSound;
    public GameObject gem;

    public static bool isPaused;
    public int PausedTime;


    public void pauseGame()
    {
        isPaused = true;
        PausedTime = GetTime();
    }

    public void resumeGame()
    {
        isPaused = false;
    }

    [CanBeNull] public static GameObject GemOwner = null;
    public bool hasStarted = false;
    private const int DelayBetweenGem = 15;
    public static int GameDuration = 2;
    private DateTime _startTime;
    private int _nextGem = DelayBetweenGem;


    private int GetTime()
    {
        return (int)(DateTime.Now - _startTime).TotalSeconds;
    }

    public static int Player1Score = 0;
    public static int Player2Score = 0;

    private static int GetPlayer1Score()
    {
        return Player1Score;
    }

    private static int GetPlayer2Score()
    {
        return Player2Score;
    }

    public void OnSheepInCenter()
    {
        if (IsGameFinished() || !hasStarted) return;
        var sheepPosition = sheep.transform.position;
        var distance1 = Vector3.Distance(player1.transform.position, sheepPosition);
        var distance2 = Vector3.Distance(player2.transform.position, sheepPosition);
        if (distance1 < distance2)
        {
            Player1Score++;
            winPointSound.GetComponent<AudioSource>().Play();
        }
        else if (distance1 > distance2)
        {
            Player2Score++;
            winPointSound.GetComponent<AudioSource>().Play();
        }
        else /*TODO what if distances are equal (now +0 for both)*/ ;
    }

    public void OnGhostTouchPlayer(GameObject player)
    {
        if (IsGameFinished() || !hasStarted) return;
        if (player == player1)
        {
            Player1Score--;
            losePointSound.GetComponent<AudioSource>().Play();
        }

        if (player == player2)
        {
            Player2Score--;
            losePointSound.GetComponent<AudioSource>().Play();
        }
    }

    public bool IsGameFinished()
    {
        return GetTime() > 60 * GameDuration;
    }

    public void Restart()
    {
        SceneManager.LoadScene("Menu");
    }

    public void Back()
    {
        SceneManager.LoadScene("Menu");
    }

    // Start is called before the first frame update

    public void ButtonStartClick()
    {
        hasStarted = true;
        isPaused = false;
        _startTime = DateTime.Now;
    }

    // Update is called once per frame
    private void Update()
    {
        if (!hasStarted) return;
        if (IsGameFinished())
        {
            endSplashScreen.SetActive(true);
            if (Player1Score > Player2Score) player1WinText.SetActive(true);
            else if (Player1Score < Player2Score) player2WinText.SetActive(true);
            else equalityText.SetActive(true);
        }


       if(!isPaused)
        {
            int minutes = GetTime() / 60, seconds = GetTime() % 60;
            timeTextMesh.GetComponent<TextMeshProUGUI>().SetText($"{minutes}:{seconds}");
            p1ScoreTextMesh.GetComponent<TextMeshProUGUI>().SetText(GetPlayer1Score().ToString());
            p2ScoreTextMesh.GetComponent<TextMeshProUGUI>().SetText(GetPlayer2Score().ToString());
            if (GetTime() >= _nextGem)
                SpawnGem();
        }
    }

    private void SpawnGem()
    {
        var position = new Vector3(UnityEngine.Random.Range(0, 30), 0, UnityEngine.Random.Range(-20, 0));
        var rotation = gem.transform.rotation;
        Instantiate(gem, position, rotation).SetActive(true);
        print($"Gem spawned at position {position}");
        _nextGem += DelayBetweenGem;
    }

    public void setGameDuration_1() { GameDuration = 1; }

    public void setGameDuration_2() { GameDuration = 2; }

    public void setGameDuration_5() { GameDuration = 5; }

    public void setGameDuration_10() { GameDuration = 10; }

}