using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "TagSwap", menuName = "Ability/Tag Swap")]
public class ChangeTag : AbilitySO
{
    public string newTag = "Untagged"; // New tag for the player during the ability duration
    public float duration = 5f; // Duration of the tag change in seconds

    private string originalTag; // Original tag of the player
    private GameObject player; // Reference to the player GameObject

    public override void ActivateAbility(GameObject owner)
    {
        base.ActivateAbility(owner);

        player = owner;

        originalTag = player.tag;

        player.tag = newTag;
        owner.GetComponent<PlayerStats>().canAuto = false;
        owner.GetComponent<MonoBehaviour>().StartCoroutine(RevertTagChange(owner));
    }

    private IEnumerator RevertTagChange(GameObject owner)
    {
        // Wait for the duration of the tag change
        yield return new WaitForSeconds(duration);

        // Revert the tag of the player to the original tag
        if (player != null)
        {
            owner.GetComponent<PlayerStats>().canAuto = true;
            player.tag = originalTag;
        }
    }
}
