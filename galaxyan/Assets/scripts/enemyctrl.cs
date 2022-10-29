using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyctrl : MonoBehaviour
{
    public enum STATE//�X�e�[�^�X���`
    { 
        back,//�����̃|�W�V�����֋A�Ғ�
        idle,//�҂�
        takeof,//�ˌ��̂��߂ɑ��񂩂痣�E������������
        attack//�����オ�����̂��ɓˌ�
    }
    public STATE state;
    protected float attack_Speed;//�v���C���[�ɓˌ�����Ƃ��̑��x
    Vector3[] flyPoint = new Vector3[2];//��яo���̃|�C���g
    Vector3 Stert_Point;//�߂��Ă���n�_
    public float magazine;
    public int late;
    int cool_time;
    float now_Point;//�Ȑ��⊮�p�̐��l
    public float fly_Speed;//��яo���̃X�s�[�h
    bool flyed = false;//��������яo���Ă��邩�̔���
    public GameObject bulletPrefub;
    Renderer my_Rend;
    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        attack_Speed = 3;
        my_Rend = GetComponent<Renderer>();
        player = GameObject.Find("player");
        Stert_Point = this.transform.position;
        for (int i = 0; i < flyPoint.Length; i++)
        {
            flyPoint[i] = this.transform.GetChild(i).gameObject.transform.position;
        }
    }

	void Update()
	{
        //Debug.Log(renderer.isVisible);
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
            Attack();
            BulletFire();
        }
    }
	// Update is called once per frame
	void Return_HomePos()//�ˌ����I��萶���Ă����珊��̈ʒu�ɖ߂�
    {
        this.transform.position += Vector3.down*Time.deltaTime;
        if (this.transform.position.y < Stert_Point.y)
        {
            this.state = STATE.idle;
        }
    }
    private void Hit()//�e�����������Ƃ��ɌĂ΂��R�[���o�b�N
    {
        Destroy(this.gameObject);
    }
    protected void Fly()//����̗����ʒu���痣�E����яo��
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
    public virtual void Attack()//���ꂼ��̂����œˌ����s���I�[�o�[���C�h
    {
        
    }
    protected void looktoPlyer()//�v���C���[�̂ق�������������
    {
        this.transform.rotation = Quaternion.FromToRotation(Vector3.up, player.transform.position-this.transform.position);
    }
    Vector3 GetCurve(Vector3 a,Vector3 b,float t)
    {
        Vector3 result;
        Vector3 v0 = Vector3.Lerp(Stert_Point, a, t);
        Vector3 v1 = Vector3.Lerp(a, b, t);

        result = Vector3.Lerp(v0,v1,t);

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
