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
        //���X�̎擾
        //(�ŏ�����scene�ɒu�����ق��������ŃR�[�h�Ƃ��Ă̎��܂���������Ƃ͔c�����Ă��܂����A�ۑ萧��̕��j���炱�̂悤�ȏ��������Ă��܂�)
        //(�ڍׂ̓M�����N�V�A���͕�T�v��������������)
        formation_Speed = 0.4f;
        blueEnemy_List = new List<GameObject>();
        purpleEnemy_List = new List<GameObject>();
        redEnemy_List = new List<GameObject>();
        Boss_List = new List<GameObject>();
        //���ׂĂ̓G��ʁX�̃��X�g�Ɏ擾
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
        Debug.Log("�擾����");
    }

    // Update is called once per frame
    void Update()
    {
        coolTime++;
        if (coolTime > interval)
        {
            //�S�����ŏo���G�̕���
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
    //����ɉ����ēG�𗣗�������
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
    //�{�X���o���Ƃ��͎�芪�����o��
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
