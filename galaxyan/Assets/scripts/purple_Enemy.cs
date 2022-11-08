using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class purple_Enemy : enemyctrl
{
	bool firstflame = true;
	GameObject atackPos;
	Vector3 atackvector;
	Vector3 curvevector;
	public float amplitude=0.3f;
	float t1 = 1;
	public override void Attack()
	{
		if (firstflame)
		{
			//“ËŒ‚‚·‚éƒxƒNƒgƒ‹‚ðŒˆ’è
			curvevector = Vector3.left;
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
		if (this.transform.position.x < -3.0f)
		{
			curvevector = Vector3.right;
		}
		this.transform.position += (Vector3.down*Time.deltaTime)+(curvevector*Time.deltaTime)*attack_Speed;//“®‚«‚ª‚í‚©‚ç‚È‚·‚¬‚é‚Ì‚Å‚Æ‚è‚ ‚¦‚¸‚±‚Ìó‘Ô‚Å
	}
}
