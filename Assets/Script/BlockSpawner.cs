using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSpawner : Observer
{
    Transform spawnPos;
    Block curBlock;

    public override void Notify(Subject subject)
    {
        BlockManager blockManager = subject.GetComponent<BlockManager>();
        spawnPos = blockManager.BlockSpawnPos;
        curBlock = blockManager.CurBlock;
    }    
    
}
