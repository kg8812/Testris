using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockManager : Subject
{
    public BgBlock bgBlockPrefab;

    readonly int vertical = 10;
    readonly int horizontal = 20;

    public Transform BlockSpawnPos { get; private set; }
    [SerializeField] Block[] blockPrefabs;

    public Block CurBlock { get; private set; }
    public readonly BgBlock[,] blocks = new BgBlock[21, 11];
    BlockController controller;
    private void Awake()
    {
        for (int i = 0; i < vertical; i++)
        {
            for (int j = 0; j < horizontal; j++)
            {
                BgBlock block = Instantiate(bgBlockPrefab, transform);
                block.transform.localPosition = new Vector2(i, j);
                blocks[j, i] = block;
            }
        }

        BlockSpawnPos = blocks[19, 4].transform;
        EventBus.Subscribe(EventType.BlockSpawn, SpawnBlock);
        if (!TryGetComponent(out controller))
        {
            controller = gameObject.AddComponent<BlockController>();
        }
        Attach(controller);
        EventBus.Subscribe(EventType.AfterMove, NotifyAll);
        EventBus.Subscribe(EventType.AfterBlockStack, CheckLine);
    }
    void Start()
    {
        EventBus.Publish(EventType.BlockSpawn);
    }

    void SpawnBlock()
    {
        Detach(CurBlock);
        if (BlockSpawnPos.GetComponent<BgBlock>().IsStacked)
        {            
            return;
        }
        int rand = Random.Range(0, blockPrefabs.Length);
        CurBlock = Instantiate(blockPrefabs[rand]);
        CurBlock.transform.position = BlockSpawnPos.position;
        Attach(CurBlock);
        NotifyAll();
    }

    void CheckLine()
    {
        for (int i = 0; i < horizontal; i++)
        {
            bool isFull = true;

            for (int j = 0; j < vertical; j++)
            {
                if (!blocks[i, j].IsStacked)
                {
                    isFull = false;
                    break;
                }
            }

            if (isFull)
            {
                RemoveLine(i);
                i--;
            }
        }
    }

    void RemoveLine(int height)
    {
        for (int i = 0; i < vertical; i++)
        {
            blocks[height, i].Remove();
        }

        for (int i = height + 1; i < 20; i++)
        {
            for (int j = 0; j < vertical; j++)
            {
                if (blocks[i, j].IsStacked)
                {
                    blocks[i - 1, j].Init(blocks[i, j].MoveDown());
                }
            }
        }
    }
}
