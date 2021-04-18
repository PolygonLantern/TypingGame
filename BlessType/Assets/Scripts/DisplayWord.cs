using UnityEngine;
using TMPro;

/// <summary>
/// Script that handles the appearance of the words above the visible characters 
/// </summary>
public class DisplayWord : MonoBehaviour
{
    // Reference to the 3D text
    public TextMeshPro text;

    public int id;
    
    // Index variable that will show at which character the player is
    private int _firstIndex = 0;
    
    // Strings to avoid repetitive typing
    private string _redColour = "<color=red>";
    private string _whiteColour = "<color=white>";
    
    // Reference to the singleton manager so we can access other classes without having to make them singletons
    private SingletonManager _singletonManager;
    
    private void Start()
    {
        // Assigning the singleton manager variable to the instance of the singleton class
        _singletonManager = SingletonManager.Instance;
    }
    
    /// <summary>
    /// Method that will set the 3D text word to the given string
    /// </summary>
    /// <param name="word"> The word that will be passed by the constructor of the Word Class</param>
    public void SetWord(string word)
    {
        text.text = word;
    }
    
    /// <summary>
    /// Method that will move the current character to the next available if the inputted character was correct
    /// </summary>
    /// <param name="index"> The index of the inputted character</param>
    public void RemoveChar(int index)
    {
        // Getting the first character of the word
        char firstChar = text.text[0];  
        
        // Takes the index of the first character of the word and adds the lenght of the red colour string. 
        // Done this way because the colouring is done by inserting the string "<colour=red>" in front of the word.
        // To keep the correct index of the word we need to calculate the index after we have inserted the given string
        int indexFirstChar = text.text.IndexOf(firstChar) + _redColour.Length;
        
        // Check if the first index is 0, then insert the string before the word to colour it and increase the first index
        //value so it is impossible to insert the red colour again in the word
        if (_firstIndex == 0)
        {
            text.text = text.text.Insert(_firstIndex, _redColour);
            ++_firstIndex;
        }
    
        // Find the white colour string and replace it with an empty string, to give the illusion of the word being filled 
        //by the red colour. This will not be executed the first time this runs, when the red colour is inserted
        text.text = text.text.Replace(_whiteColour, "");
        
        // Insert the white colour after the calculated index, which will return the normal index then sum it with the current index
        //that is passed externally and after that insert the white colour string. No calculations are required here, because of the line
        //above, also the colour will fill the rest of the characters in that word, until the index of the red colour is moved.
        text.text = text.text.Insert(indexFirstChar + index, _whiteColour);
        
        // Change the material of the word holder to the colour that represents that is being typed
        ChangeToTypingMaterial(gameObject);
    }
    
    public void RemoveWord()
    {
        if (WordManager.TimesFailedAWord >= 3)
        {
            ChangeToMistakenMaterial(gameObject);
        }
        else
        {
            ChangeToCorrectMaterial(gameObject);
        }
        
        WordManager.TimesFailedAWord = 0;
        Destroy(gameObject, .2f);
    }

    void ChangeToMistakenMaterial(GameObject word)
    {
        GameManager.Mistakes++;
        word.GetComponent<Renderer>().material = _singletonManager.materials[2];
    }
    
    void ChangeToTypingMaterial(GameObject word)
    {
        word.GetComponent<Renderer>().material = _singletonManager.materials[0];
    }
     
    void ChangeToCorrectMaterial(GameObject word)
    {
        GameManager.Score++;
        word.GetComponent<Renderer>().material = _singletonManager.materials[1];
    }
    
}
