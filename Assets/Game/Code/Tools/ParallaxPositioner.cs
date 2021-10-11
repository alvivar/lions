using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ParallaxPositioner : MonoBehaviour
{
    public List<Transform> parallax;
    public List<float> parallaxStep;
    public List<SpriteRenderer> sprites;

    [ContextMenu("Update Sprites")]
    void UpdateSprites()
    {
        sprites = sprites.Where(x => x).ToList();
        foreach (var sprite in FindObjectsOfType<SpriteRenderer>())
        {
            if (!sprites.Contains(sprite))
                sprites.Add(sprite);
        }
    }

    [ContextMenu("Adjust Parallax Depth")]
    void FixParallaxDepth()
    {
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
    void FixSprites()
    {
        int index = 0;
        foreach (var sprite in sprites)
        {
            sprite.sortingOrder = index++;
        }
    }
}