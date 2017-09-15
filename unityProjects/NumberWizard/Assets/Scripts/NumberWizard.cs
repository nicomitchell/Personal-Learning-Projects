using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberWizard : MonoBehaviour
{
    int max, min , guess;
    // Use this for initialization
    void Start()
    {
        startGame();
    }
    void startGame()
    {
        max = 1000;
        min = 0;
        guess = 500;
        print("Welcome to Number Wizard");
        print("Pick a number between " + min + " and " + max + " in your head.");
        max += 1;
        print("Press up if your number is higher than my guess, down if your number \n" +
            "is lower than my guess, and enter if they are equal.");
        print("Is your number " + guess + "?");
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        { 
            min = guess;
            nextGuess();
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            max = guess;
            nextGuess();
        }
        else if (Input.GetKeyDown(KeyCode.Return))
        {
            print("I won!");
            startGame();
        }
    }
    void nextGuess()
    {
        guess = (max + min) / 2;
        print("Is your number " + guess + "?");
    }
}
