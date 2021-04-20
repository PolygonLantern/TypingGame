[System.Serializable]

public class Word
{
   // Id of the word
   public int id;
   
   // The word itself
   public string word;
   
   // The index of the current character
   private int charIndex;
   
   // The display word of the word
   private DisplayWord displayWord;
   
   /// <summary>
   /// Constructor for a single word
   /// </summary>
   /// <param name="_word"> The word itself</param>
   /// <param name="_displayWord"> The display word that is attached to the gameObject that represents the word</param>
   /// <param name="_id"> Id of the word</param>
   public Word(string _word, DisplayWord _displayWord, int _id)
   {
      word = _word;
      charIndex = 0;
      displayWord = _displayWord;
      displayWord.SetWord(word);
      id = _id;
   }
   
   /// <summary>
   /// Method that returns the word that is selected next character
   /// </summary>
   /// <returns></returns>
   public char GetNextChar()
   {
      return word[charIndex];
   }
   
   /// <summary>
   /// If there is active word and the player inputs a character that is the next character of the active word, increase the characterIndex and
   /// change the colour of the character
   /// </summary>
   public void InputChar()
   {
      charIndex++;
      displayWord.RemoveChar(charIndex);
   }
   
   /// <summary>
   /// Method that returns whether the player has made 3 typos or completed the word. If the player has completed the word
   /// the colour of the object underneath them will turn green and resets the TimesFailedAWord counter 
   /// </summary>
   /// <returns></returns>
   public bool WordTyped()
   {
      bool wordTyped = charIndex >= word.Length || WordManager.TimesFailedAWord >= 3;

      if (wordTyped)
      {  
         
         displayWord.RemoveWord();
         WordManager.TimesFailedAWord = 0;
      }
      
      return wordTyped;
   }
   
}
