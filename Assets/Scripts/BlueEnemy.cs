using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueEnemy : enemyctrl
{
	
	bool firstflame=true;
	GameObject atackPos;
	Vector3 atackvector;
	public override void Attack()
	{
		if (this.GetState() != STATE.attack) {
			firstflame = true;
			return; }
		if (firstflame)
		{	
			//“ËŒ‚‚·‚éƒxƒNƒgƒ‹‚ðŒˆ’è
			atackPos = GameObject.FindGameObjectWithTag("Player");
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
