using System;
using System.Collections.Generic;
using UnityEngine;

public class WordManager : MonoBehaviour
{
    public static int TimesFailedAWord;
    public List<Word> words;
    public static bool HasActiveWord;
    public Word activeWord;
    private Spawner _spawner;
    private SingletonManager _singletonManager;
    private List<GameObject> _wordsGameObjects = new List<GameObject>();
    private void Start()
    {
        _singletonManager= SingletonManager.Instance;

        _singletonManager.EventSystem.SpawnWords += SpawnWords;
        _singletonManager.EventSystem.UpdateTextHeights += UpdateText;
        _singletonManager.EventSystem.DeleteTextObjects += () =>
        {
            foreach (var word in _wordsGameObjects)
            {
                if (word.GetComponent<DisplayWord>().text.text == activeWord.word)
                    _wordsGameObjects.Remove(word);
                
                Debug.Log(word.GetComponent<DisplayWord>().text.text);
            }
            
            
        };
        
        
        TimesFailedAWord = 0;
        
        _spawner = GetComponent<Spawner>();
        
        _singletonManager.EventSystem.SpawnWord();
        _singletonManager.EventSystem.UpdateTextHeight();
        
    }

    void AddWord(int currentID)
    {
        GameObject wordObject = _spawner.SpawnWord();
        Word word = new Word(WordGenerator.GetRandomWord(), _spawner.GetDisplayWord(currentID, wordObject), currentID);
        words.Add(word);
        _wordsGameObjects.Add(wordObject);
    }
    
    public void TypeLetter(char key)
    {
        if (HasActiveWord)
        {
            if (activeWord.GetNextChar() == key)
            {
                activeWord.InputChar();
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
                    activeWord = word;
                    HasActiveWord = true;
                    word.InputChar();
                    break;
                }
            }
        }

        if (HasActiveWord && activeWord.WordTyped())
        {
            HasActiveWord = false;
            _singletonManager.EventSystem.DeleteTextObject();
            words.Remove(activeWord);
            _singletonManager.EventSystem.UpdateTextHeight();
        }
    }
    void SpawnWords()
    {
        for (int i = 0; i < 5; i++)
        {
            AddWord(i);
        }
    }

    void UpdateText()
    {
        TestUpdate(_wordsGameObjects);
    }
    
    void TestUpdate(List<GameObject> testWords)
    {
        _singletonManager.GameManager.ClearPositionList();
        
        foreach (var word in testWords)
        {
            _singletonManager.GameManager.AddTextHeight(word);
            _singletonManager.GameManager.OrderTextHeight(word);
        }
    }
}
