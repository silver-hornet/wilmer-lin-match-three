using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : GamePiece
{
    public bool clearedByBomb = false;
    public bool clearedAtBottom = false;

    void Start()
    {
        matchValue = MatchValue.None;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
