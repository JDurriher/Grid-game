using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassTile : Tile   // Derives from our base tile, not MonoBehaviour
{
    [SerializeField] private Color baseColor, offsetColor;

    public override void Init(int x, int y)
    {
        // Changing colour of alternating tiles
        var isOffset = (x + y) % 2 == 1; // Is (x + y)/2 not even. If so colour them separate colour
        tileRenderer.color = isOffset ? offsetColor : baseColor;    // Tile renderer color, if we are offset, set equal to offset color, otherwise set to base color.
    }

}
