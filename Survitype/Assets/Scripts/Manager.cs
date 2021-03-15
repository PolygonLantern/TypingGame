using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public List<Word> words;
    private bool _hasActiveWord;
    private Word _activeWord;
    private Spawner _spawner;
    private void Start()
    {
        _spawner = GetComponent<Spawner>();
        AddWord();
        AddWord();
        AddWord();
    }

    void AddWord()
    {
        Word word = new Word(WordGenerator.GetRandomWord(), _spawner.SpawnWord());
        
        Debug.Log(word.word);
        
        words.Add(word);
    }

    public void TypeLetter(char key)
    {
        if (_hasActiveWord)
        {
            if (_activeWord.GetNextChar() == key)
            {
                _activeWord.InputChar();
            } 
        }
        else
        {
            foreach (Word word in words)
            {
                if (word.GetNextChar() == key)
                {
                    _activeWord = word;
                    _hasActiveWord = true;
                    word.InputChar();
                    break;
                }
            }
        }

        if (_hasActiveWord && _activeWord.WordTyped())
        {
            _hasActiveWord = false;
            words.Remove(_activeWord);
        }
    }
}
