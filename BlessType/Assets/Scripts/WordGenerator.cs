using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using SimpleJSON;
using Random = UnityEngine.Random;

[DefaultExecutionOrder(-1)]
public class WordGenerator : MonoBehaviour
{
    /*private string _pokeName;

    private SingletonManager _singletonManager;*/
    
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
    /*private void Start()
    {
        _singletonManager = SingletonManager.Instance;

        _singletonManager.EventSystem.GetPokemonName += ctx =>
        {
            _pokeName = ctx;
            Debug.Log(_pokeName);
        };
    }

    private void OnDisable()
    {
        _singletonManager.EventSystem.GetPokemonName -= ctx =>
        {
            _pokeName = ctx;
        };
    }

    private readonly string _pokeBaseURL = "https://pokeapi.co/api/v2/";

    public string GetRandomWord()
    {
        int randomPokemon = Random.Range(1, 899);

        StartCoroutine(GetPokemonAtIndex(randomPokemon)); 
    }

    IEnumerator GetPokemonAtIndex(int randomPokemon)
    {
        string pokemonURL = _pokeBaseURL + "pokemon/" + randomPokemon.ToString();
        UnityWebRequest pokeInfoRequest = UnityWebRequest.Get(pokemonURL);

        yield return pokeInfoRequest.SendWebRequest();

        if (pokeInfoRequest.isNetworkError || pokeInfoRequest.isHttpError)
        {
            Debug.LogError(pokeInfoRequest.error);
            yield break;
        }

        JSONNode pokeInfo = JSON.Parse(pokeInfoRequest.downloadHandler.text);
        
        string retrievedName = pokeInfo["name"];

        _singletonManager.EventSystem.GetName(retrievedName);

        yield return null;
    }
    */
    
    
}
