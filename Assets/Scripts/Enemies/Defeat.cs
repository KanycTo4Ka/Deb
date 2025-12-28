using UnityEngine;

public class Defeat : IState
{
    AbstractEnemy enemy;

    public Defeat(AbstractEnemy enemy) => this.enemy = enemy;

    public void enter()
    {
        enemy.stop(true);
    }

    public void exit() {}

    public void update() {}
}
