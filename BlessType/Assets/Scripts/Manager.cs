using System;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    private SingletonManager _singletonManager;
    public static int TimesFailedAWord;
    public List<Word> words;
    private bool _hasActiveWord;
    private Word _activeWord;
    private Spawner _spawner;
    private EventSystem _eventSystem;
    private void Start()
    {
        _singletonManager = SingletonManager.i;
        _eventSystem = EventSystem.Instance;

        _eventSystem.SpawnWords += SpawnWords;
        
        TimesFailedAWord = 0;
        _spawner = GetComponent<Spawner>();
        _eventSystem.SpawnWord();
    }

    void AddWord(int currentID)
    {
        Word word = new Word(WordGenerator.GetRandomWord(), _spawner.SpawnWord(currentID), currentID);
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
            else
            {
                ++TimesFailedAWord;
            }

            if (TimesFailedAWord >= 3)
            {
                _eventSystem.WordMistaken(_activeWord.id);
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
                    _eventSystem.WordIsBeingTyped(word.id);
                    word.InputChar();
                    //word.material = _spawner.SpawnWord(word.id).typingMaterial;
                    break;
                }
                
            }
        }

        if (_hasActiveWord && _activeWord.WordTyped())
        {
            _hasActiveWord = false;
            _eventSystem.WordTyped(_activeWord.id);
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
