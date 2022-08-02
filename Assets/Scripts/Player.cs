using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //int[,] move = { { 0,1},{1,0 },{ 0,-1},{-1,0 } };
    //DIRECTION direction = DIRECTION.NORTH;
    public void MoveForward(Vector2 _pos)
    {
        transform.localPosition = _pos;
    }

    public void Turn(Vector3 _rote)
    {
        transform.Rotate(_rote);
    }
}
