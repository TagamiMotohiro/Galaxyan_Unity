using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class purple_Enemy : enemyctrl
{
	bool firstflame = true;
	Vector3 atackPos;
	Vector3 atackvector;
	public float attack_Speed;//�v���C���[�ɓˌ�����Ƃ��̑��x
	public override void Attack()
	{
		if (firstflame)
		{
			//�ˌ�����x�N�g��������
			atackPos = GameObject.Find("player").transform.position;
			atackvector = (atackPos - this.transform.position).normalized;
			firstflame = false;
		}
		this.transform.position += atackvector * Time.deltaTime * attack_Speed;
	}
}
