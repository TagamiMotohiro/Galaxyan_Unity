using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyctrl : MonoBehaviour
{
    public enum STATE//ステータスを定義
    { 
        idle,//待ち
        takeof,//突撃のために隊列から離脱し浮きあがる
        attack//浮き上がったのちに突撃
    }
    public STATE state;
    Vector3[] flyPoint = new Vector3[1];//飛び出しのポイント
    Vector3 Stert_Point;//戻ってくる地点
    Vector3 fly_Terget;//突撃対象地点
    float now_Point;//曲線補完用の数値
    float rudius;//自身の大きさ
    public float fly_Speed;//飛び出しのスピード
    bool flyed = false;//自分が飛び出しているかの判定
    GameObject bulletPrefub;
    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("player");
        rudius = 0.25f;
        Stert_Point = this.transform.position;
        for (int i = 0; i < flyPoint.Length; i++)
        {
            flyPoint[i] = this.transform.GetChild(i).gameObject.transform.position;
        }
        fly_Terget = flyPoint[0];
        //new Vector2(this.transform.localScale.x/2,this.transform.localScale.y/2);
    }

    // Update is called once per frame
    void Update()
    {
        
        if (state==STATE.takeof)
        {
            looktoPlyer();
            Fly();
        }
        if (state == STATE.attack)
        {
            looktoPlyer();
            Attack();
        }
    }
    void Return_HomePos()//突撃が終わり生きていたら所定の位置に戻る
    {

    }
    public float ReturnRudius()
    {
        return rudius;
    }
    private void Hit()//弾が当たったときに呼ばれるコールバック
    {
        Destroy(this.gameObject);
    }
    private void Fly()//所定の立ち位置から離脱し飛び出す
    {
        if (Mathf.Abs(this.transform.position.magnitude - flyPoint[0].magnitude) < 0.1f && !flyed)
        {
            this.state = STATE.attack;
        }
        this.transform.position = Vector3.Slerp(Stert_Point, fly_Terget, now_Point);
        now_Point += 1 * Time.deltaTime * fly_Speed;
    }
    public virtual void Attack()//それぞれのやり方で突撃を行うオーバーライド
    {
        
    }
    private void looktoPlyer()//プレイヤーのほうを向き続ける
    {
        this.transform.rotation = Quaternion.FromToRotation(Vector3.up, player.transform.position);
    }
}
