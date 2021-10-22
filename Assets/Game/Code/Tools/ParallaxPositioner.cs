using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[ExecuteInEditMode]
public class ParallaxPositioner : MonoBehaviour
{
    public bool update = false;

    [Header("Config")]
    public List<Transform> targets;
    public List<Transform> parallax;
    public List<float> parallaxStep;
    public List<SpriteRenderer> sprites;

    private void Update()
    {
        if (update)
        {
            FixAll();
            update = false;
        }
    }

    [ContextMenu("Fix All")]
    private void FixAll()
    {
        RefreshSprites();
        FixParallaxDepth();
        FixSpritesSortingOrder();
    }

    [ContextMenu("Refresh Sprites")]
    private void RefreshSprites()
    {
        sprites = sprites.Where(x => x).ToList();

        foreach (var target in targets)
        {
            foreach (var sprite in target.GetComponentsInChildren<SpriteRenderer>())
            {
                if (!sprites.Contains(sprite))
                    sprites.Add(sprite);
            }
        }

    }

    [ContextMenu("Adjust Parallax Depth")]
    private void FixParallaxDepth()
    {
        while (parallaxStep.Count < parallax.Count)
            parallaxStep.Add(0);

        int index = 0;
        foreach (var p in parallax)
        {
            p.transform.position = new Vector3(
                p.transform.position.x,
                p.transform.position.y,
                parallaxStep[index++]);
        }
    }

    [ContextMenu("Fix Sprites Sorting Order")]
    private void FixSpritesSortingOrder()
    {
        int index = 0;
        foreach (var sprite in sprites)
        {
            sprite.sortingOrder = index++;
        }
    }
}