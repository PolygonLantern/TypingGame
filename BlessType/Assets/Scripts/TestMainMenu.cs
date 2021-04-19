using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TestMainMenu : MonoBehaviour
{
   public Button playButton;
   private bool _readyToLoadLevel;
   private SingletonManager _singletonManager;

   private void Start()
   {
      _singletonManager = SingletonManager.Instance;
      
      if (!_readyToLoadLevel)
      {
         _singletonManager.WordGenerator.LoadPokemon();
      }
      playButton.interactable = false;
      playButton.onClick.AddListener(LoadWords);
   }

   void LoadWords()
   {
      if (_readyToLoadLevel)
      {
         SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
      }
   }

   private void Update()
   {
      _readyToLoadLevel = WordGenerator.IsReady;
      if (_readyToLoadLevel)
      {
         playButton.interactable = true;
      }
   }
}
