using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class purple_Enemy : enemyctrl
{
	bool firstflame = true;
	GameObject atackPos;
	Vector3 atackvector;
	public float amplitude=0.3f;
	float t1 = 1;
	public override void Attack()
	{
		if (firstflame)
		{
			//突撃するベクトルを決定
			atackPos = GameObject.Find("player");
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
		this.transform.position += atackvector * Time.deltaTime * attack_Speed;//動きがわからなすぎるのでとりあえずこの状態で
	}
}
