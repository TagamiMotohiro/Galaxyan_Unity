using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedEnemy : enemyctrl
{
	bool firstflame = true;
	GameObject atackPos;
	Vector3 atackvector;
	public override void Attack()
	{
		if (this.state != STATE.attack)
		{
			firstflame = true;
			return;
		}
		if (firstflame)
		{
			var id = this.gameObject.name.Substring(13).ToCharArray();
			//“ËŒ‚‚·‚éƒxƒNƒgƒ‹‚ðŒˆ’è
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
		this.transform.position += atackvector * Time.deltaTime * attack_Speed;
	}
}
