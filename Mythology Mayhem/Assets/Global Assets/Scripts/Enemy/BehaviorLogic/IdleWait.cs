using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "IdleStateSOBase", menuName = "Mythology Mayhem/IdleStateSOBase", order = 0)]
public class IdleWait : IdleStateSOBase
{
   public override void DoStatelogic() {
    base.DoStatelogic();

    // Custom Code
    /*
        if(enemy2D != null) {
            gameObject.transform.position = gameObject.transform.position;
        }

        else if (enemy3D != null) {
            enemy3D.anim.SetBool(walkingBool, false);
	        enemy3D.agent.speed = 0f;
	        enemy3D.idleTimer += Time.deltaTime;
        }
    */
   }
}
