using System.Collections.Generic;
using System.Linq;
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
    public static bool test;
    private void Start()
    {
        _singletonManager= SingletonManager.Instance;

        _singletonManager.EventSystem.SpawnWords += SpawnWords;
        _singletonManager.EventSystem.UpdateTextHeights += UpdateText;
        _singletonManager.EventSystem.DeleteTextObjects += DeleteTextObjects;

        TimesFailedAWord = 0;
        
        _spawner = GetComponent<Spawner>();
      
    }

    private void OnDisable()
    {
        _singletonManager.EventSystem.SpawnWords -= SpawnWords;
        _singletonManager.EventSystem.UpdateTextHeights -= UpdateText;
    }

    void AddWord(int currentID)
    {
        GameObject wordObject = _spawner.SpawnWord();
        Word word = new Word(WordGenerator.GetRandomWord(), _spawner.GetDisplayWord(currentID, wordObject), currentID);
        wordObject.GetComponent<DisplayWord>().id = currentID;
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
            words.Remove(activeWord);
            _singletonManager.EventSystem.DeleteTextObject(activeWord.id);
            _singletonManager.EventSystem.UpdateTextHeight();
        }
    }
    void SpawnWords(int amount)
    {
       AddWord(amount);
    }

    void UpdateText()
    {
        TestUpdate(_wordsGameObjects);
    }
    
    void TestUpdate(List<GameObject> testWords)
    {
        foreach (var word in testWords)
        {
            _singletonManager.GameManager.AddEnemyPosition(word);
            _singletonManager.GameManager.OrderTextHeight(word);
        }
    }
    
    private void DeleteTextObjects(int id)
    {
        foreach (var word in _wordsGameObjects.ToList())
        {
            if (word.GetComponent<DisplayWord>().id == id)
            {
                _wordsGameObjects.Remove(word);
            }
        }
    }

    public void RemoveWordFromList(GameObject word)
    {
        _wordsGameObjects.Remove(word);
    }
}
