using System.Collections;

public class GolemController : EnemyController
{
    public void GolemEndAbility()
    {
        StartCoroutine(Attack());
    }

    private IEnumerator Attack()
    {
        attackColiderL.enabled = true;
        attackColiderR.enabled = true;
        audioSources[0].Play();
        
        yield return null;
        yield return null; 

        attackColiderL.enabled = false;
        attackColiderR.enabled = false;
    }
}
