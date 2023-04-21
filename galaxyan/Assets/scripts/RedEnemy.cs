using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedEnemy : enemyctrl//�{�X�̎�芪���̌p���N���X
{
	bool firstflame = true;
	Transform atackPos;
	Vector3 atackvector;
	public override void Attack()
	{
		if (this.GetState() != STATE.attack)
		{
			firstflame = true;
			return;
		}
		if (firstflame)
		{
			var id = this.gameObject.name.Substring(13).ToCharArray();
			//�ˌ�����x�N�g��������
			atackPos = GameObject.FindGameObjectWithTag("Player").transform.position;
			if (atackPos != null)
			{
				atackvector = (atackPos.transform.position - this.transform.position).normalized;
			}
			else
			{
				atackvector = Vector3.down;
			}
			firstflame = false;
		}
		this.transform.position += atackvector * Time.deltaTime * GetAtackSpeed();
	}
}
