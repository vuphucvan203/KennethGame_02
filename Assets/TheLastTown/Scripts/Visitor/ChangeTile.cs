using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ChangeTile : KennMonoBehaviour
{
    [SerializeField] protected Tilemap tilemap;
    [SerializeField] protected List<Collider2D> colliders;
    [SerializeField] protected TileBase tileBase;

    protected void Update()
    {
        ActiveChangeTile();
    }

    protected override void LoadComponent()
    {
        base.LoadComponent();
        tilemap = GetComponentInParent<Tilemap>();
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);
            if (child.TryGetComponent<Collider2D>(out Collider2D collider))
            {
                colliders.Add(collider);
            }
        }    
    }

    public void ActiveChangeTile()
    {
        foreach (var collider in colliders)
        {
            if (!collider.enabled)
            {
                Vector3Int cellPos = tilemap.WorldToCell(collider.transform.position);
                tilemap.SetTile(cellPos, tileBase);
            } 
        }
    }

}
