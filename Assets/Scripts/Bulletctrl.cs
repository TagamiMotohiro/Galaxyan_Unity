using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bulletctrl : MonoBehaviour//弾の当たり判定を返すクラス(敵味方共通)
{
    public float speed;
    [SerializeField]
    string TargetTag;//当たり判定を返す対象のタグの文字列
    float bullet_alive = 0f;
    [SerializeField]
    SpriteRenderer MySpriteRenderer;
    //敵の当たり判定を返すクラスのリスト
    List<CollisionCtrl> enemyCollision_List=new List<CollisionCtrl>();

    // Start is called before the first frame update
    void Start()
    {
        //敵の当たり判定を返すクラスを取得
        GameObject[] g = GameObject.FindGameObjectsWithTag(TargetTag);
        foreach (var Collision_Item in g)
        {
            enemyCollision_List.Add(Collision_Item.GetComponent<CollisionCtrl>());
        }
    }

    // Update is called once per frame
    void Update()
    {
        BulletTransForm();
        ColisionDirection();
    }
    void BulletTransForm()
    {
        if (TargetTag == "Player")//ターゲットがプレイヤーか敵かで弾の動きを分岐
        {
            this.transform.position += Vector3.down * speed * Time.deltaTime;
        }
        else
        {
            this.transform.position += this.transform.up.normalized * speed * Time.deltaTime;
        }
        bullet_alive++;
        if (!MySpriteRenderer.isVisible&&bullet_alive>5f)
        {
            Destroy(this.gameObject);
        }
        
    }
    void ColisionDirection()//代入された対象に弾が当たっているかの判定
    {
        if (enemyCollision_List == null) { return; }
        foreach (CollisionCtrl Collision in enemyCollision_List)
        {
            BulletColision(Collision);
        }
    }
    void BulletColision(CollisionCtrl enemy)//自作当たり判定
    {
        if (Mathf.Abs(this.transform.position.x - enemy.transform.position.x) < enemy.ReturnRadius() &&
           Mathf.Abs(this.transform.position.y - enemy.transform.position.y) < enemy.ReturnRadius())
        {
            enemy.gameObject.GetComponent<enemyctrl>().Hit();
            Destroy(this.gameObject);
        }
    }
    public void SetTarget(string tag)//当たる対象をtagを用いて設定する際に使用する文字列の変更
    {
        TargetTag = tag;
    }
}
