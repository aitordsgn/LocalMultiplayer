using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InitializeLevel : MonoBehaviour
{

    [SerializeField]
    private Transform[] playerSpawns;

    [SerializeField]
    private GameObject playerPrefab;

    [SerializeField]
    private List<PlayerConfiguration> playerConfigurations;

    // Start is called before the first frame update
    void Start()
    {
        var playerConfigs = PlayerConfigurationManager.Instance.GetPlayerConfigs().ToArray();
        playerConfigurations = playerConfigs.ToList<PlayerConfiguration>();
        for (int i = 0; i < playerConfigs.Length; i++)
        {
            var player = Instantiate(playerPrefab, playerSpawns[i].position, playerSpawns[i].rotation, gameObject.transform);
            player.GetComponent<PlayerInputHandler>().InitializePlayer(playerConfigs[i]);
        }
    }
}
    
