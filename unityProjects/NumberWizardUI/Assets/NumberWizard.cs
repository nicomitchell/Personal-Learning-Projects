using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NumberWizard : MonoBehaviour
{
    int max, min , guess;
    int maxGuesses = 11;
    public Text currentGuessBox, guessesLeftBox;
    // Use this for initialization
    void Start()
    {
        startGame();
    }
    void startGame()
    {
        max = 1000;
        min = 0;
        nextGuess();
    }
    // Update is called once per frame
    void Update()
    { 
        currentGuessBox.text = guess.ToString();
        guessesLeftBox.text = maxGuesses.ToString();
        if (Input.GetKeyDown(KeyCode.UpArrow))
            guessHigher();
        else if (Input.GetKeyDown(KeyCode.DownArrow))
            guessLower();
    }

    public void guessHigher()
    {
        min = guess;
        nextGuess();
    }

    public void guessLower()
    {
        max = guess;
        nextGuess();
    }

    void nextGuess()
    {
        maxGuesses--;
        if (maxGuesses < 0)
        {
            SceneManager.LoadScene("Win");
        }
        else
        {
            //Performs better with a true random number as the range approaches 0
            if (max - min < 20)
            {
                guess = Random.Range(min + 1, max);
            }
            //Generates a guess that will be between 1/4 and 3/4 of the range
            else
            {
                guess = Random.Range(0 - ((max - min) / 4), (max - min) / 4) + ((max + min) / 2);
            }
        }
    }
}
