using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Static instance of this game manager allows us to easily grab it from anywhere in game
    public static GameManager Instance; // Static means something which cannot be instantiated. Cannot create an object of a static class anda cannot access static members using an object
    public GameState GameState; // Variable of time game state corresponds to enum

    void Awake()    // Awake called when script is being loaded. Called before Start()
    {
        Instance = this;
    }

    void Start()
    {
        ChangeState(GameState.GenerateGrid);    // In start function we call first game state
    }

    public void ChangeState(GameState newState) // Method to change state of game. Takes in game state called 'newState'
    {
        GameState = newState;   // If need to run logic for specific state can add methods just above break;
        switch (newState)
        {
            case GameState.GenerateGrid:
                GridManager.Instance.GenerateGrid();    // Calling GenerateGrid function from GridManager script when game state is 'GenerateGrid'
                break;
            case GameState.SpawnHeroes:
                UnitManager.Instance.SpawnHeroes();
                break;
            case GameState.SpawnEnemies:
                UnitManager.Instance.SpawnEnemies();
                break;
            case GameState.HeroesTurn:
                break;
            case GameState.EnemiesTurn:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }
    }
}

public enum GameState   // List of different game states
{
    GenerateGrid = 0,
    SpawnHeroes = 1,
    SpawnEnemies = 2,
    HeroesTurn = 3,
    EnemiesTurn = 4
}