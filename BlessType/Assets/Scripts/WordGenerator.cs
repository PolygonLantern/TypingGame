using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using SimpleJSON;
using Random = UnityEngine.Random;

/// <summary>
/// Class that handles the generation of the words. Uses Pokemon API that returns full information about given pokemon,
/// then it stores the name in a list and passes it on to the WordManager
/// </summary>
[DefaultExecutionOrder(-2)]
public class WordGenerator : MonoBehaviour
{
    // List that stores all the pokemons that are returned from the api
    public List<string> pokemons = new List<string>();

    // Bool check to see if enough pokemon are returned
    public static bool IsReady;
    
    // Base URL to the api
    private readonly string _pokeBaseURL = "https://pokeapi.co/api/v2/";
     
    /// <summary>
    /// Method that takes a random name from the list and returns it, so it can be used in the WordManager
    /// </summary>
    /// <returns></returns>
    public string GetRandomName()
    {
        // Generates random index between 0 and the amount of entities in the list
        int randomIndex = Random.Range(0, pokemons.Count);
        
        //returns the word that corresponds on the randomIndex
        string randomWord = pokemons[randomIndex];
        return randomWord;
    }
    
    /// <summary>
    /// Method that starts the coroutine GetPokemonAtIndex 
    /// </summary>
    public void LoadPokemon()
    {
        StartCoroutine(GetPokemonAtIndex());
    }
    
    /// <summary>
    /// Coroutine that makes request to the Pokemon API for the information that it needs
    /// </summary>
    IEnumerator GetPokemonAtIndex()
    {
        // Generates random index from 1 to 898, this includes all the pokemons that are currently available
        int randomPokemon = Random.Range(1, 899);
        
        // Loop that will do 50 requests to the api for pokemon
        for (int i = 0; i < 50; i++)
        {   
            // Creating a new URL that includes the pokemon/ keyword and the randomIndex
            string pokemonURL = _pokeBaseURL + "pokemon/" + randomPokemon;
            
            // Create request to the api
            UnityWebRequest pokeInfoRequest = UnityWebRequest.Get(pokemonURL);
            
            // Wait for response
            yield return pokeInfoRequest.SendWebRequest();

            // Check if the response returned networkError or Api error
            if (pokeInfoRequest.isNetworkError || pokeInfoRequest.isHttpError)
            {   
                // Debug the given error
                Debug.LogError(pokeInfoRequest.error);
                
                // Break from the coroutine
                yield break;
            }
            
            // Create a JSON node (from the SimpleJSON file) to parse all the returned JSON string
            JSONNode pokeInfo = JSON.Parse(pokeInfoRequest.downloadHandler.text);

            // Start another coroutine to be able to store the returned value. This was the only way that I could find to store
            // any returned value from a coroutine. The 'value => pokemons.Add(value)' bit is where the pokemon's name is stored.  
            yield return StartCoroutine(GetPokemonName(pokeInfo["name"], value => pokemons.Add(value)));
            
            // Reset the random index
            randomPokemon = Random.Range(1, 899);
        }
        
        // Once out of the loop, set the check variable to true
        IsReady = true;
    }
    
    /// <summary>
    /// Coroutine that helps return the name of the API returned pokemon.
    /// </summary>
    /// <param name="pokemonName">The name that we want to store</param>
    /// <param name="result"></param>
    /// <returns></returns>
    IEnumerator GetPokemonName(string pokemonName, Action<string> result)
    {
        // Waits for 0.001 sec
        yield return new WaitForSeconds(.001f);
        
        // Gets the passed string
        string test = pokemonName;
        
        // Passes the string to the event
        result(test);
    }
    
    
}
