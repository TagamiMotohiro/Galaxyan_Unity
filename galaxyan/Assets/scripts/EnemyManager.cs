using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    List<GameObject> blueEnemy_List;
    List<GameObject> redEnemy_List;
    List<GameObject> Boss_List;
    List<GameObject> purpleEnemy_List;
    public int interval=120;
    float formation_Speed;
    int n;
    int coolTime=0;
    // Start is called before the first frame update
    void Start()
    {
        //諸々の取得
        //(最初からsceneに置いたほうが高速でコードとしての収まりもいいことは把握していますが、課題制作の方針からこのような処理をしています)
        //(詳細はギャラクシアン模倣概要をご覧ください)
        formation_Speed = 0.4f;
        blueEnemy_List = new List<GameObject>();
        purpleEnemy_List = new List<GameObject>();
        redEnemy_List = new List<GameObject>();
        Boss_List = new List<GameObject>();
        //すべての敵を別々のリストに取得
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
        Debug.Log("取得完了");
    }

    // Update is called once per frame
    void Update()
    {
        coolTime++;
        if (coolTime > interval)
        {
            //百分率で出す敵の分岐
            switch (n=Random.Range((int)0,(int)3))
            {
                case 0:
                blueEnemy();
                    break;
                case 1:
                PurpleEnemy();
                    break;
                case 2:
                BossEnemy();
                    break;
                default:
                    break;
            }
            coolTime = 0;
        }
        if (this.transform.position.x < 0.6f)
        { formation_Speed = -(formation_Speed); }
		if (this.transform.position.x > -0.6f)
		{ formation_Speed = -(formation_Speed); }
		this.transform.position += new Vector3(formation_Speed*Time.deltaTime,0,0);
    }
    //分岐に応じて敵を離陸させる
    void blueEnemy()
    {
        if (blueEnemy_List == null) { return; }
        blueEnemy_List.RemoveAll(a=>a==null);
        for (int i = 0; i < blueEnemy_List.Count; i++)
        {
            if (blueEnemy_List[i] == null)
            {
                continue;
            } else if(blueEnemy_List[i].GetComponent<enemyctrl>().GetState()==enemyctrl.STATE.idle)
            {
                blueEnemy_List[i].SendMessage("TakeOf");
                break;
            }
        }
    }
    void PurpleEnemy()
    {
        if (purpleEnemy_List == null) { return; }
        purpleEnemy_List.RemoveAll(a => a == null);
        for (int i = 0; i < purpleEnemy_List.Count; i++)
        {
            if (purpleEnemy_List[i] == null)
            {
                continue;
            }
            else if (purpleEnemy_List[i].GetComponent<enemyctrl>().GetState() == enemyctrl.STATE.idle)
            {
                purpleEnemy_List[i].SendMessage("TakeOf");
                break;
            }
        }
    }
    //ボスを出すときは取り巻きも出す
    void BossEnemy()
    {
        if (Boss_List == null) {
            RedEnemy();
            return; 
        }
        int r = Random.Range((int)0, (int)2);
        GameObject e = Boss_List[r];
        if (e != null)
        {
            if (e.GetComponent<enemyctrl>().GetState() == enemyctrl.STATE.idle)
            {
                e.SendMessage("TakeOf");
            }
        }
        if (r == 0)
        {
            for (int i = 0; i < 2; i++)
            {
                if (redEnemy_List[i]!=null) {
                    if (redEnemy_List[i].GetComponent<enemyctrl>().GetState() == enemyctrl.STATE.idle)
                    {
                        redEnemy_List[i].SendMessage("TakeOf");
                    }
                }
            }
        }
        if (r == 1)
        {
            for (int i = redEnemy_List.Count-2; i < redEnemy_List.Count; i++)
            {
                if (redEnemy_List[i] != null)
                {
                    if (redEnemy_List[i].GetComponent<enemyctrl>().GetState() == enemyctrl.STATE.idle)
                    {
                        redEnemy_List[i].SendMessage("TakeOf");
                    }
                }
            }
        }
        
    }
    void RedEnemy()
    {
        
        if (redEnemy_List == null) { return; }
        redEnemy_List.RemoveAll(a => a == null);
        for (int i = 0; i < redEnemy_List.Count; i++)
        {
            if (redEnemy_List[i] == null)
            {
                continue;
            }
            else if (redEnemy_List[i].GetComponent<enemyctrl>().GetState() == enemyctrl.STATE.idle)
            {
                redEnemy_List[i].SendMessage("TakeOf");
                break;
            }
        }
    }
}
