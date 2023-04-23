using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class playerctrl : MonoBehaviour
{
    [Header("�e�̃v���n�u")]
    [SerializeField]
    GameObject bullet;
    [Header("�����̃G�t�F�N�g")]
    [SerializeField]
    GameObject Exprode_Effect;
    [Header("�e���Ȃ����Ƃ��鎞�̕`�敪��")]
    [SerializeField]
    Sprite bulletEnpty;
    [SerializeField]
    Sprite bulletFull;
    float radius;
    GameObject b;
    public float speed;
    float horizontal=0;
    List<GameObject> enemy_List;
    
    SpriteRenderer myRend;
    
    // Start is called before the first frame update
    void Start()
    {
        myRend = this.gameObject.GetComponent<SpriteRenderer>();
        radius = 0.25f;
        enemy_List = GameObject.FindGameObjectsWithTag("Enemy").ToList();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMove();
        CollisionEnemy();
    }
    void PlayerMove()
    {
        horizontal += Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        if (horizontal < -3.5f) { horizontal = -3.5f; }
        if (horizontal > 3.5f) { horizontal = 3.5f; }
        this.transform.position = new Vector3(horizontal, -4, 0);
        if (Input.GetKeyDown(KeyCode.Space)&&b==null)
        {
            b = Instantiate(bullet, this.transform.position, Quaternion.identity);
            b.GetComponent<bulletctrl>().SetTagString("Enemy");
        }
        if (b == null)
        { myRend.sprite = bulletFull; }
        else
        { myRend.sprite = bulletEnpty; }
    }
    //����̓����蔻��
    //���X�g�������G�̓����蔻������Ԃɔ���
    void CollisionEnemy()
    {
        enemy_List.RemoveAll(a=>a==null);
        foreach (GameObject e in enemy_List)
        {
            var c = e.GetComponent<CollisionCtrl>();
            if (Mathf.Abs(e.transform.position.x - transform.position.x) > (this.radius + c.ReturnRadius()))
            { continue; }
            if (Mathf.Abs(e.transform.position.y - transform.position.y) > (this.radius + c.ReturnRadius()))
            { continue; }
            if (Mathf.Abs(e.transform.position.z - transform.position.z) > (this.radius + c.ReturnRadius()))
            { continue; }
            this.Hit();
        }
    }
    //�G�q�b�g���R�[���o�b�N
    void Hit()
    {
        Instantiate(Exprode_Effect,this.transform.position,Quaternion.identity);
        this.gameObject.SetActive(false);
    }
}
