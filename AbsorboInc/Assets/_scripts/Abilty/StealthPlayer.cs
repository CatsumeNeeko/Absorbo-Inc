using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.AI;
[CreateAssetMenu(fileName ="PlayerSwap",menuName ="Ability/PlayerSwap")]
public class StealthPlayer : AbilitySO
{
    public GameObject fakePrefab;
    public string goundTag = "Ground";
    private GameObject player;
    public override void ActivateAbility(GameObject owner)
    {
        base.ActivateAbility(owner);

        player = owner;
        player.tag = "FakePlayer";

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit) && hit.collider.CompareTag(goundTag))
        {
            // Instantiate the fake player at the hit point
             GameObject fakePlayer=Instantiate(fakePrefab, hit.point, Quaternion.identity);

            // Set the destination for the fake player
            fakePlayer.GetComponent<FakePlayer>().SetDestination(hit.point);
        }
    }
}
