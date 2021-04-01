using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class DisplayWord : MonoBehaviour
{
    public static bool HasCompletedSuccessfully;
    public TextMeshPro text;
    private int _firstIndex = 0;
    private string _redColour = "<color=red>";
    private string _whiteColour = "<color=white>";
    //private Renderer _renderer;
    private SingletonManager _singletonManager;
    
    private void Start()
    {
        _singletonManager = SingletonManager.i;
        //_renderer = GetComponent<Renderer>();
    }

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
        ChangeToTypingMaterial(gameObject);
    }

    public void RemoveWord()
    {
        //Destroy(gameObject);
        if (Manager.TimesFailedAWord >= 3)
        {
            ChangeToMistakenMaterial(gameObject);
        }
        else
        {
            ChangeToCorrectMaterial(gameObject);
        }
        HasCompletedSuccessfully = true;
        Manager.TimesFailedAWord = 0;


    }

    Material ChangeToMistakenMaterial( GameObject word)
    {
        return word.GetComponent<Renderer>().material = _singletonManager.materials[2];
    }
    
    Material ChangeToTypingMaterial(GameObject word)
    {
        return word.GetComponent<Renderer>().material = _singletonManager.materials[0];
    }
     
    Material ChangeToCorrectMaterial(GameObject word)
    {
        return word.GetComponent<Renderer>().material = _singletonManager.materials[1];
    }
    
}
