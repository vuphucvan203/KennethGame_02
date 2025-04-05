using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowing : KennMonoBehaviour
{
    [SerializeField] protected Soldier soldier;
    [SerializeField, Range(1, 10)] protected int smoothSeed = 5; 
    protected Vector3 offset = new Vector3(0, 0, -10);

    private void Update()
    {
        FollowSoldier();
    }

    protected void FollowSoldier()
    {
        if (soldier == null) return;
        Vector3 targetPositon = soldier.transform.position + offset;
        transform.position = Vector3.Lerp(transform.position, targetPositon, Time.deltaTime * smoothSeed);
    }    
}
