using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using SimpleJSON;
using Random = UnityEngine.Random;

[DefaultExecutionOrder(-2)]
public class WordGenerator : MonoBehaviour
{
    private string _pokeName;
    
    public List<string> pokemons = new List<string>();

    public static bool IsReady;
    /*private static string[] _words =
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
    }*/
    public string GetRandomName()
    {
        int randomIndex = Random.Range(0, pokemons.Count);
        string randomWord = pokemons[randomIndex];
        return randomWord;
    }

    public void LoadPokemon()
    {
        StartCoroutine(GetPokemonAtIndex());
    }

    private readonly string _pokeBaseURL = "https://pokeapi.co/api/v2/";

 
    IEnumerator GetPokemonAtIndex()
    {
        int randomPokemon = Random.Range(1, 899);
        for (int i = 0; i < 50; i++)
        {
            string pokemonURL = _pokeBaseURL + "pokemon/" + randomPokemon;
            UnityWebRequest pokeInfoRequest = UnityWebRequest.Get(pokemonURL);

            yield return pokeInfoRequest.SendWebRequest();

            if (pokeInfoRequest.isNetworkError || pokeInfoRequest.isHttpError)
            {
                Debug.LogError(pokeInfoRequest.error);
                yield break;
            }

            JSONNode pokeInfo = JSON.Parse(pokeInfoRequest.downloadHandler.text);



            yield return StartCoroutine(GetPokemonName(pokeInfo["name"], value => pokemons.Add(value)));
            randomPokemon = Random.Range(1, 899);
        }

        IsReady = true;
    }

    IEnumerator GetPokemonName(string s, Action<string> result)
    {
        yield return new WaitForSeconds(.001f);
        string test = s;
        result(test);
    }
    
    
}
