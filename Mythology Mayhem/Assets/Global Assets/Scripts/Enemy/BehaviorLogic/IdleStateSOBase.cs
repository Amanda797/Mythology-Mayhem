using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//
public class IdleStateSOBase : ScriptableObject {
    protected Enemy enemy2D; //for 2D
    protected EnemyAI3D enemy3D; //for 3D
    protected GameObject gameObject;
    protected Transform transform;

    public virtual void Initialize2D(Enemy enemy, GameObject gameObject) {
        this.gameObject = gameObject;
        enemy2D = enemy;
        this.transform = gameObject.transform;
    }

    public virtual void Initialize3D(EnemyAI3D enemy, GameObject gameObject) {
        this.gameObject = gameObject;
        enemy3D = enemy;
        this.transform = gameObject.transform;
    }

    public virtual void DoStatelogic() {
        //Enter conditions for changing states here followed by enemy.SwitchState(enemy.EnemyStates.State), separated by 2D and 3D

        /*
        if(enemy2D != null) {
            if(enemy2D.idleTimer > enemy2D.idleDuration) {
                enemy2D.idleTimer = 0;
                enemy2D.SwitchState(enemy2D.EnemyState.Patrol);
            }
        }

        else if (enemy3D != null) {
            if(enemy3D.idleTimer > enemy3D.idleDuration) {
                enemy3D.idleTimer = 0f;
                enemy3D.agent.speed = patrolSpeed;
                enemy3D.isIdle = false;
                enemy3D.agent.isStopped = false;
                enemy3D.SwitchState(enemy3D.EnemyState.Patrol);
            }
        }
        */
    }
}