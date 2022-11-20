using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerConfigurationManager : MonoBehaviour
{
    private List<PlayerConfiguration> playerConfigs;

    //Tenemos un numero maximo de jugadores
    [SerializeField] private int MaxPlayers = 2;

    //Hacemos que sea SingleTone
    public static PlayerConfigurationManager Instance { get; private set; }
    

    private void Awake()
    {
        if(Instance != null)
        {
            Debug.Log("Singleton = Trying to create another instance of singleton!");
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(Instance);
            playerConfigs = new List<PlayerConfiguration>();
        }
    }


    public List<PlayerConfiguration> GetPlayerConfigs()
    {
        return playerConfigs;
    }

    public void setPlayerColor(int index,Color color)
    {
        playerConfigs[index].PlayerColor = color;
    }
    public void readyPlayer(int index)
    {
        playerConfigs[index].IsReady = true;
        //Miramos si todos los jugadores estan para jugar
        
        if(playerConfigs.Count == MaxPlayers && playerConfigs.All(p => p.IsReady == true))
        {
            Destroy(Instance);
            var cuantos =this.GetComponentsInChildren<PlayerInput>();
            /*for (var i = 0; i < cuantos.Length; i++)
            {
                Destroy(cuantos[i]);
            }
            Destroy(this.GetComponentInChildren<PlayerInput>());*/
            
            FindObjectOfType<LevelLoader>().LoadNextLevel(5);
        }
    }
    public void NotreadyPlayer(int index)
    {
        playerConfigs[index].IsReady = false;
       
    }
    public void HandlePlayerJoin(PlayerInput pi)
    {
        Debug.Log("PlayerJoin" + pi.playerIndex);
        //Miramos si no lo hemos añadido ya
        if(!playerConfigs.Any(p => p.PlayerIndex == pi.playerIndex))
        {
            //Hacemos que sea hijo del PlayerConfigurationManager
            pi.transform.SetParent(transform);
            //Añadimos el jugador
            playerConfigs.Add(new PlayerConfiguration(pi));

        }
    }
}


public class PlayerConfiguration
{
    public PlayerConfiguration(PlayerInput pi)
    {
        PlayerIndex = pi.playerIndex;
        Input = pi;
    }
    public PlayerInput Input { get; set; }  
    public int PlayerIndex { get; set; }
    public bool IsReady { get; set; }
    public Color PlayerColor { get; set; }
}
