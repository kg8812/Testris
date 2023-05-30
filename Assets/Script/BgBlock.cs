using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BgBlock : MonoBehaviour
{
    SmallBlock curBlock;
    
    public void Init(SmallBlock block)
    {
        curBlock = block;
    }

    public void Remove()
    {
        Destroy(curBlock.gameObject);
        curBlock = null;
    }   

    public bool IsStacked
    {
        get { return curBlock != null; }
    }  

    public SmallBlock MoveDown()
    {
        curBlock.transform.Translate(new Vector2(0, -1),Space.World);
        curBlock.height--;
        SmallBlock bl = curBlock;
        curBlock = null;
        return bl;
    }
}
