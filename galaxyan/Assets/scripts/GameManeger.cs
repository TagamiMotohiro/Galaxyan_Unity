using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManeger : MonoBehaviour
{
    private GameObject player;
    [Header ("各種prefub")]
    [SerializeField]
    GameObject player_Prefub;
    [SerializeField]
    GameObject enemy_Formation;
    [SerializeField]
    TMPro.TextMeshProUGUI text;
    [SerializeField]
    TMPro.TextMeshProUGUI text2;
	// Start is called before the first frame update
	private void Awake()
    {
        Time.timeScale = 1;
        player = Instantiate(player_Prefub);
        player.name = ("player");
		Instantiate(enemy_Formation,Vector3.zero,Quaternion.identity);
	}
    // Update is called once per frame
    void Update()
    {
        if (!player.activeSelf)
        {
            //自機が死亡したらゲームオーバー表示
            Time.timeScale = 0.3f;
            text.gameObject.SetActive(true);
            text2.gameObject.SetActive(true);
            if (Input.GetKey(KeyCode.R))
            {
                //Rキーでリトライ
                SceneManager.LoadScene("MainGame");
            }
        }
    }
}
