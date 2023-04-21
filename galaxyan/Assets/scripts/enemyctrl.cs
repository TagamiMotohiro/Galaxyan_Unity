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
        attack//突撃
    }
    [SerializeField]
    GameObject bulletPrefub;
    [SerializeField]
    GameObject effect;
    [SerializeField]
    private STATE state;
    [SerializeField]
    private float attack_Speed;//プレイヤーに突撃するときの速度
    Transform[] flyobfj = new Transform[2];
    Vector3[] flyPoint = new Vector3[2];//飛び出しのポイント
    Vector3 p1;
    Vector3 Stert_Point;//戻ってくる地点
    public float magazine;
    public int late;
    int cool_time;
    float now_Point;//曲線補完用の数値
    float ReturnPosition_Y=0;//戻ってくる際のY座標(どれだけスタート地点から離れているか)
    public float fly_Speed;//飛び出しのスピード
    bool flyed = false;//自分が飛び出しているかの判定
    protected float axis=1;
    GameObject maneger;
    GameObject player;
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        //諸々の取得
        //(最初からsceneに置いたほうが高速でコードとしての収まりもいいことは把握していますが、課題制作の方針からこのような処理をしています)
        maneger = this.transform.root.gameObject;
        anim = GetComponent<Animator>();
        attack_Speed = 3;
        player = GameObject.Find("player");
        p1 = this.transform.position;
        for (int i = 0; i < flyPoint.Length; i++)
        {
            flyobfj[i] = this.transform.GetChild(i).gameObject.transform;
            flyPoint[i] = flyobfj[i].position;
            if (this.transform.position.x < 0)
            {
                flyobfj[i].localPosition = new Vector3(-flyobfj[i].localPosition.x, flyobfj[i].localPosition.y, 0);
                flyPoint[i] = flyobfj[i].position;
                axis = -1;
            }
        }
    }

	void Update()
	{
        //飛び立つポイントの定義
        Stert_Point = new Vector3(p1.x+maneger.transform.position.x,p1.y,p1.z);
        //各状態に合わせた行動の定義
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
	void Return_HomePos()//突撃が終わり生きていたら所定の位置に戻る
    {
        this.transform.position = new Vector3(Stert_Point.x,this.transform.position.y-ReturnPosition_Y,0);
        if (this.transform.position.y < Stert_Point.y)
        {
            //待機状態に戻る
            this.state = STATE.idle;
            ReturnPosition_Y = 0;
        }
        //
        ReturnPosition_Y+=0.1f*Time.deltaTime;
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
            this.transform.rotation = Quaternion.AngleAxis(now_Point*(axis*180), Vector3.forward);
            now_Point+=fly_Speed*Time.deltaTime;
            if (now_Point >= 1)
            {
                //攻撃処理に移行
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

    Vector3 GetCurve(Vector3 a,Vector3 b,float t)//(飛び立つ処理用)スタート地点と2点のポイントからt%地点の曲線補完を出す
    {
        Vector3 result;
        Vector3 p0 = Vector3.Lerp(Stert_Point, a, t);
        Vector3 p1 = Vector3.Lerp(a, b, t);

        result = Vector3.Lerp(p0,p1,t);

        return result;
    }
    protected void BulletFire()
    {
        //弾を生成
        if (cool_time >= late && magazine > 0)
        {
            GameObject g =
            Instantiate(bulletPrefub, this.transform.position, Quaternion.FromToRotation(Vector3.up, this.transform.up));
            //弾インスタンスの当たる対象を設定
            g.GetComponent<bulletctrl>().SetTagString("Player");
            cool_time = 0;
            magazine--;
        }
        cool_time++;
    }
    //ゲッターメソッド
    public STATE GetState()
    {
        return state;
    }
    public float GetAtackSpeed()
    {
        return attack_Speed;
    }
}
