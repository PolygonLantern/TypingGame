using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordGenerator : MonoBehaviour
{
    private static string[] _words =
    {
        "prick", "blush", "wiry", "needy", "general", "impress",
        "odd", "button", "beg", "enjoy", "beginner", "confused", "strong", "lace", "festive",
        "scent", "scissors", "excuse", "encouraging", "funny", "callous", "fragile", "afternoon",
        "develop", "accidental", "magical", "crazy", "ashamed", "sky", "blow", "tricky", "wrist",
        "fretful", "homely", "beef", "detect", "jolly", "furry", "fit", "perfect", "development", "cabbage",
        "knowledgeable", "receive", "reflect", "fire", "fish", "unequaled", "berserk", "door"

    };
    
    public static string GetRandomWord()
    {
        int randomIndex = Random.Range(0, _words.Length);
        string randomWord = _words[randomIndex];
        return randomWord;
    }
}
