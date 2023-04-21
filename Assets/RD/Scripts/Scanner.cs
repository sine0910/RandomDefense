using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scanner : MonoBehaviour
{
    SlimeTower tower;

    private float range;
    private int count;

    private LayerMask[] target_layers;
    private List<RaycastHit2D> target_list;
    public List<Enemy> target;

    public void Init(SlimeTower t, float r, int c, LayerMask[] l)
    {
        tower = t;

        range = r;
        count = c;
        target_layers = l;
    }

    void FixedUpdate()
    {
        if(tower.on)
        {
            target_list = GetTargetList(target_layers);

            target = GetNearestTargetList();
        }
    }

    List<RaycastHit2D> GetTargetList(LayerMask[] target_layers)
    {
        List<RaycastHit2D> targets = new List<RaycastHit2D>();

        foreach (LayerMask target in target_layers)
        {
            targets.AddRange(Physics2D.CircleCastAll(transform.position, range, Vector2.zero, 0, target));
        }

        return targets;
    }

    List<Enemy> GetNearestTargetList()
    {
        List<Enemy> reasult = new List<Enemy>();

        float diff = 100f;

        for(int i = 0; i < count; i++)
        {
            reasult.Add(GetNearestTarget(reasult));
        }

        return reasult;
    }

    Enemy GetNearestTarget(List<Enemy> list)
    {
        Enemy reasult = null;

        float diff = 100f;

        foreach (RaycastHit2D target in target_list)
        {
            Enemy e = target.transform.GetComponent<Enemy>();

            if (!list.Contains(e))
            {
                float cur_diff = e.disToGoal;

                if (cur_diff < diff)
                {
                    diff = cur_diff;

                    reasult = e;
                }
            }
        }

        return reasult;
    }
}
