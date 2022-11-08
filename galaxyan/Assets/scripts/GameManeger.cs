using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManeger : MonoBehaviour
{
    private GameObject player;
    public GameObject player_Prefub;
    public GameObject enemy_Formation;
	// Start is called before the first frame update
	private void Awake()
	{
        player = Instantiate(player_Prefub);
        player.name = ("player");
		Instantiate(enemy_Formation,Vector3.zero,Quaternion.identity);
	}
    // Update is called once per frame
    void Update()
    {
        if (!player.activeSelf)
        {
            Time.timeScale = 0.3f;
        }
    }
}
