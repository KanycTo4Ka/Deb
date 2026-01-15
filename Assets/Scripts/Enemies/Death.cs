using UnityEngine;

public class Death : IState
{
    AbstractEnemy enemy;

    public Death(AbstractEnemy enemy)
    {
        this.enemy = enemy;
    }

    public void enter()
    {
        enemy.stop(true);
        enemy.attack(false);
    }

    public void exit()
    {
        enemy.attack(false);
        enemy.stop(false);
    }

    public void update()
    {
        
    }
}
