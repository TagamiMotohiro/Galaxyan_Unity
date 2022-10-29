using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField]
    List<GameObject> blueEnemy_List;
    List<GameObject> redEnemy_List;
    List<GameObject> Boss_List;
    List<GameObject> purpleEnemy_List;
    public int interval=120;
    int coolTime=0;
    // Start is called before the first frame update
    void Start()
    {
        blueEnemy_List = new List<GameObject>();
        purpleEnemy_List = new List<GameObject>();
        redEnemy_List = new List<GameObject>();
        Boss_List = new List<GameObject>();
        for (int i = 0; i < 30; i++)
        {
            GameObject g = GameObject.Find("Enemy(Blue) (" + i.ToString("D2") + ")");
            blueEnemy_List.Add(g);
        }
        for (int i = 0; i < 8; i++)
        {
            GameObject g = GameObject.Find("Enemy(purple) ("+i.ToString()+")");
            purpleEnemy_List.Add(g);
        }
        for (int i = 0; i < 6; i++)
        {
            GameObject g = GameObject.Find("Enemy(Red) (" + i.ToString() + ")");
            redEnemy_List.Add(g);
        }
        for (int i = 0; i < 2; i++)
        {
            GameObject g = GameObject.Find("Enemy(Boss) (" + i.ToString() + ")");
            Boss_List.Add(g);
        }
        Debug.Log("Žæ“¾Š®—¹");
    }

    // Update is called once per frame
    void Update()
    {
        coolTime++;
        if (coolTime > interval)
        {
            blueEnemy();
            coolTime = 0;
        }
    }
    void blueEnemy()
    {
        if (blueEnemy_List == null) { return; }
        blueEnemy_List.RemoveAll(a=>a==null);
        GameObject b = blueEnemy_List[Random.Range(0, blueEnemy_List.Count)];
        if (b.GetComponent<enemyctrl>().state == enemyctrl.STATE.idle)
        {
            b.SendMessage("TakeOf");
        }
    }
}
