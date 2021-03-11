using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class DisplayWord : MonoBehaviour
{
    public TextMeshProUGUI text;
    

    public void SetWord(string word)
    {
        text.text = word;
    }

}
