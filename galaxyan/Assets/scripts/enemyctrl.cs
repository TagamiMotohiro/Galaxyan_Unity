using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyctrl : MonoBehaviour
{
    public enum STATE//ステータスを定義
    { 
        back,//自分のポジションへ帰還中
        idle,//待ち
        takeof,//突撃のために隊列から離脱し浮きあがる
        attack//浮き上がったのちに突撃
    }
    public STATE state;
    protected float attack_Speed;//プレイヤーに突撃するときの速度
    Vector3[] flyPoint = new Vector3[2];//飛び出しのポイント
    Vector3 p1;
    Vector3 Stert_Point;//戻ってくる地点
    public float magazine;
    public int late;
    int cool_time;
    float now_Point;//曲線補完用の数値
    float t=0;
    public float fly_Speed;//飛び出しのスピード
    bool flyed = false;//自分が飛び出しているかの判定
    public GameObject bulletPrefub;
    public GameObject effect;
    GameObject maneger;
    GameObject player;
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        maneger = this.transform.root.gameObject;
        anim = GetComponent<Animator>();
        attack_Speed = 3;
        player = GameObject.Find("player");
        p1 = this.transform.position;
        for (int i = 0; i < flyPoint.Length; i++)
        {
            flyPoint[i] = this.transform.GetChild(i).gameObject.transform.position;
        }
    }

	void Update()
	{
        Stert_Point = new Vector3(p1.x+maneger.transform.position.x,p1.y,p1.z);
        
        if (this.state == STATE.attack && this.transform.position.y<-5)
        {
            this.state = STATE.back;
            this.transform.position = new Vector3(Stert_Point.x, 5, 0);
            this.transform.rotation = Quaternion.identity;
        }
        if (this.state == STATE.back)
        {
            this.magazine = 2;
            now_Point = 0;
            Return_HomePos();
        }
        if (state == STATE.takeof)
        {
            looktoPlyer();
            Fly();
        }
        if (state == STATE.attack)
        {
            looktoPlyer();
            BulletFire();
        }
        if (state == STATE.idle)
        {
            anim.SetBool("Idle", true);
        }
        else
        {
            anim.SetBool("Idle", false);
        }
        Attack();
    }
	// Update is called once per frame
	void Return_HomePos()//突撃が終わり生きていたら所定の位置に戻る
    {
        this.transform.position = new Vector3(Stert_Point.x,this.transform.position.y-t,0);
        if (this.transform.position.y < Stert_Point.y)
        {
            this.state = STATE.idle;
            t = 0;
        }
        t+=0.1f*Time.deltaTime;
    }
    private void Hit()//弾が当たったときに呼ばれるコールバック
    {
        Instantiate(effect, this.transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }
    protected void Fly()//所定の立ち位置から離脱し飛び出す
    {
        if (this.state == STATE.takeof)
        {
            this.transform.position = GetCurve(flyPoint[0], flyPoint[1], now_Point);
            now_Point+=fly_Speed*Time.deltaTime;
            if (now_Point >= 1)
            {
                this.state = STATE.attack;
            }
        }
    }
    void TakeOf()
    {
        this.state = STATE.takeof;
    }
    public virtual void Attack()//それぞれのやり方で突撃を行うオーバーライド
    {
        
    }
    protected void looktoPlyer()//プレイヤーのほうを向き続ける
    {
        this.transform.rotation = Quaternion.FromToRotation(Vector3.up, player.transform.position-this.transform.position);
    }
    Vector3 GetCurve(Vector3 a,Vector3 b,float t)
    {
        Vector3 result;
        Vector3 p0 = Vector3.Lerp(Stert_Point, a, t);
        Vector3 p1 = Vector3.Lerp(a, b, t);

        result = Vector3.Lerp(p0,p1,t);

        return result;
    }
    protected void BulletFire()
    {
        if (cool_time >= late && magazine > 0)
        {
            GameObject g =
            Instantiate(bulletPrefub, this.transform.position, Quaternion.FromToRotation(Vector3.up, this.transform.up));
            g.GetComponent<bulletctrl>().SetTagString("Player");
            cool_time = 0;
            magazine--;
        }
        cool_time++;
    }
}
