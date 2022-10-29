using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletctrl : MonoBehaviour
{
    public float speed;
    [SerializeField]
    string tagstring;
    float bullet_alive = 0f;
    SpriteRenderer thissprite;
    
    // Start is called before the first frame update
    void Start()
    {
        thissprite = this.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        BulletTransForm();
        ColisionDirection();
    }
    void BulletTransForm()
    {
        if (tagstring == "Player")
        {
            this.transform.position += Vector3.down * speed * Time.deltaTime;
        }
        else
        {
            this.transform.position += this.transform.up.normalized * speed * Time.deltaTime;
        }
        bullet_alive++;
        if (!thissprite.isVisible&&bullet_alive>5f)
        {
            Destroy(this.gameObject);
        }
        
    }
    void ColisionDirection()//������ꂽ�Ώۂɒe���������Ă��邩�̔���
    {
        GameObject[] enemy_List = GameObject.FindGameObjectsWithTag(tagstring);
        if (enemy_List == null) { return; }
        foreach (GameObject obj in enemy_List)
        {
            BulletColision(obj);
        }
    }
    void BulletColision(GameObject enemy)//���쓖���蔻��
    {
        var e = enemy.GetComponent<CollisionCtrl>();
        if (Mathf.Abs(this.transform.position.x - enemy.transform.position.x) < e.ReturnRadius()+0.1 &&
           Mathf.Abs(this.transform.position.y - enemy.transform.position.y) < e.ReturnRadius()+0.1)
        {
            enemy.SendMessage("Hit");
            Destroy(this.gameObject);
        }
    }
    public void SetTagString(string tag)//������Ώۂ�tag��p���Đݒ肷��ۂɎg�p���镶����̕ύX
    {
        tagstring = tag;
    }
}
