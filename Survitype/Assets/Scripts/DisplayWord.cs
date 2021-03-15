using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class DisplayWord : MonoBehaviour
{
    public TextMeshPro text;
    private int _firstIndex = 0;
    private string _redColour = "<color=red>";
    private string _whiteColour = "<color=white>";
    public void SetWord(string word)
    {
        text.text = word;
    }

    public void RemoveChar(int index)
    {
        char firstChar = text.text[0];  
        int indexFirstChar = text.text.IndexOf(firstChar) + _redColour.Length;
        
        if (_firstIndex == 0)
        {
            text.text = text.text.Insert(_firstIndex, _redColour);
            ++_firstIndex;
        }

        text.text = text.text.Replace(_whiteColour, "");
        text.text = text.text.Insert(indexFirstChar + index, _whiteColour);
    }

    public void RemoveWord()
    {
        Destroy(gameObject);
    }

}
