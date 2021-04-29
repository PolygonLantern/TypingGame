using UnityEngine;
using System.Collections;
using TMPro;

/// <summary>
/// Script that handles the appearance of the words above the visible characters 
/// </summary>
public class DisplayWord : MonoBehaviour
{
    // A variable to hold all the sounds used for typing functionalities
    public AudioClip UIrightSound;
    public AudioClip UIwrongSound;
    public AudioClip DoneSound;

    // This object is used to hold a particle system and activate every time when the player inputs the right letter
    public GameObject holder;

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
    
    /// <summary>
    /// Method that is called whenever a word is removed. Once the player has made 3 typos the colour will turn into black
    /// and will destroy the object, if however the player types the word correctly the colour will turn green and will reset the typo counter 
    /// </summary>
    public void RemoveWord()
    {
        if (WordManager.TimesFailedAWord >= 3)
        {
            // Changes the colour to black
            ChangeToMistakenMaterial(gameObject);
        }
        else
        {
            // Changes the colour to greeen
            ChangeToCorrectMaterial(gameObject);
        }
        
        // Reset the typo counter
        WordManager.TimesFailedAWord = 0;

        // Destroy the object that word has been typed or mistaken
        Destroy(gameObject, .4f);
    }

    /// <summary>
    /// The method that will turn the colour of the object into black and will increase the mistakes counter which is displayed on the ui
    /// </summary>
    /// <param name="word"></param>
    void ChangeToMistakenMaterial(GameObject word)
    {
        GameManager.Mistakes++; 
        word.GetComponent<Renderer>().material = _singletonManager.materials[2];
    }
    
    /// <summary>
    /// The method that changes the colour of the currently input word to yellow/orange or whatever colour is set to the 0th slot of the array 
    /// </summary>
    /// <param name="word"></param>
    void ChangeToTypingMaterial(GameObject word)
    {
        word.GetComponent<Renderer>().material = _singletonManager.materials[0];
    }
     
    /// <summary>
    /// The method that will change the colour of the object that word is being input to green and will increase the score counter
    /// </summary>
    /// <param name="word"></param>
    void ChangeToCorrectMaterial(GameObject word)
    {
        GameManager.Score++;
        word.GetComponent<Renderer>().material = _singletonManager.materials[1];
        AudioSource source = GetComponent<AudioSource>();
        source.clip = DoneSound;
        source.Play();
    }

    // Plays the particle after the player inputs the right letter
    public void PlayParticle()
    {
        StartCoroutine(PlayParticleSystem(holder));

    }

    // Activates the holder particle that contains the particle effect to emit
    IEnumerator PlayParticleSystem(GameObject holder)
    {
        GameObject particle = Instantiate(holder,gameObject.transform);
        ParticleSystem particleSystem = particle.GetComponentInChildren<ParticleSystem>();

        particleSystem.Play();

        yield return new WaitForSeconds(particleSystem.time);
        Destroy(particle,1f);

    }

    // A public method to be used whenever the sound for the UI utilized on a canvas
    public void playUISound()
    {
        AudioSource source = GetComponent<AudioSource>();
        source.clip = UIrightSound;
        source.Play();
    }
}
