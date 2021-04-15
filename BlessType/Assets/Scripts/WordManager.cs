using System;
using System.Collections.Generic;
using UnityEngine;

public class WordManager : MonoBehaviour
{
    public static int TimesFailedAWord;
    public List<Word> words;
    public static bool HasActiveWord;
    private Word _activeWord;
    private Spawner _spawner;
    private SingletonManager _singletonManager;
    private void Start()
    {
        _singletonManager= SingletonManager.Instance;

        _singletonManager.EventSystem.SpawnWords += SpawnWords;
        TimesFailedAWord = 0;
        _spawner = GetComponent<Spawner>();
        _singletonManager.EventSystem.SpawnWord();
    }

    void AddWord(int currentID)
    {
        GameObject wordObject = _spawner.SpawnWord();
        Word word = new Word(WordGenerator.GetRandomWord(), _spawner.GetDisplayWord(currentID, wordObject), currentID);
        _singletonManager.GameManager.AddTextHeight(wordObject);
        words.Add(word);
        _singletonManager.GameManager.OrderTextHeight(wordObject);
    }

    public void TypeLetter(char key)
    {
        if (HasActiveWord)
        {
            if (_activeWord.GetNextChar() == key)
            {
                _activeWord.InputChar();
            }
            else
            {
                ++TimesFailedAWord;
            }
        }
        else
        {
            foreach (Word word in words)
            {
                if (word.GetNextChar() == key)
                {
                    _activeWord = word;
                    HasActiveWord = true;
                    word.InputChar();
                    break;
                }
                
            }
        }

        if (HasActiveWord && _activeWord.WordTyped())
        {
            HasActiveWord = false;
            words.Remove(_activeWord);
            
        }
    }
    void SpawnWords()
    {
        for (int i = 0; i < 5; i++)
        {
            AddWord(i);
        }
        
    }
}
