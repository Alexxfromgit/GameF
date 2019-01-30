using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BoardF;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MainScript : MonoBehaviour
{
    const int size = 4;
    Game game;
    Sound sound;

    public Text TextMoves;
    
	// Use this for initialization
	void Start ()
    {
        game = new Game(size);
        sound = GetComponent<Sound>();
        HideButtons();
	}

    public void OnStart()
    {
        game.Start(1000 + System.DateTime.Now.DayOfYear);
        ShowButtons();
        sound.PlayStart();
    }

    public void OnClick()
    {
        if (game.Solved())
            return;
        string name = EventSystem.current.currentSelectedGameObject.name;
        int x = int.Parse(name.Substring(0, 1));
        int y = int.Parse(name.Substring(1, 1));
        game.PressAt(x, y);
        if (game.PressAt(x, y) > 0)
        {
            sound.PlayMove();
        }                    
        ShowButtons();
        if (game.Solved())
        {
            TextMoves.text = "Game finished in " + game.moves + " moves";
            sound.PlaySolved();
        }
    }

    void HideButtons()
    {
        for (int x = 0; x < size; x++)
            for (int y = 0; y < size; y++)
                ShowDigitAt(0, x, y);
        TextMoves.text = "Welcome to Game F";
    }

    void ShowButtons()
    {
        for (int x = 0; x < size; x++)
            for (int y = 0; y < size; y++)
                ShowDigitAt(game.GetDigitAt(x,y),x,y);
        TextMoves.text = game.moves + " moves";
    }

    void ShowDigitAt(int digit, int x, int y)
    {
        string name = x + "" + y;
        var button = GameObject.Find(name);
        var text = button.GetComponentInChildren<Text>();
        text.text = DecToHex(digit);
        button.GetComponentInChildren<Image>().color = (digit > 0) ? Color.white : Color.clear;
    }

    string DecToHex(int digit)
    {
        //not use for now
        if (digit == 0) return "";
        if (digit < 10) return digit.ToString();
        return ((char)('A' + digit - 10)).ToString();
    }

    // Update is called once per frame
    void Update ()
    {
		
	}
}
