using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid
{
    public int xPos;
    public int zPos;
    public bool doHaveLeftWall;
    public bool doHaveRightWall;
    public bool doHaveUpWall;
    public bool doHaveDownWall;
    public List<Grid> neighbors;
    public float gScore;
    public float hScore;
    public float fScore;

    public Grid(int xPos_, int zPos_)
    {
        this.xPos = xPos_;
        this.zPos = zPos_;
        this.doHaveLeftWall = true;
        this.doHaveRightWall = true;
        this.doHaveUpWall = true;
        this.doHaveDownWall = true;
        this.neighbors = new List<Grid>();
        this.gScore = float.PositiveInfinity;
        this.hScore = 0f;
        this.fScore = float.PositiveInfinity;
    }
}
