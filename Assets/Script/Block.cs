using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : Observer
{
    SmallBlock[] blocks;
    BlockManager manager;

    private void Awake()
    {
        blocks = GetComponentsInChildren<SmallBlock>();
    }

    void Start()
    {
        EventBus.Subscribe(EventType.BlockStack, Stack);
    }

    void Stack()
    {              
        foreach (var block in blocks)
        {
            int x = block.width;
            int y = block.height;

            if (y >= 20 || y == 19 && x == 4)
            {
                GameManager.Instance.GameOver();
                return;
            }          
            manager.blocks[y, x].Init(block);
            block.transform.SetParent(null);
        }

        EventBus.Publish(EventType.BlockSpawn);
        EventBus.Unsubscribe(EventType.BlockStack, Stack);
        EventBus.Publish(EventType.AfterBlockStack);
        Destroy(gameObject);
    }
    public override void Notify(Subject subject)
    {
        manager = subject.GetComponent<BlockManager>();
        foreach (var block in blocks)
        {
            block.height = Mathf.RoundToInt(block.transform.position.y - subject.transform.position.y);
            block.width = Mathf.RoundToInt(block.transform.position.x - subject.transform.position.x);
        }
    }

    public bool DownMovable
    {
        get
        {
            foreach (var block in blocks)
            {
                int x = block.width;
                int y = block.height - 1;
                if (x > 9 || x < 0 || y > 19) continue;

                if (y < 0 || manager.blocks[y, x].IsStacked)
                {
                    return false;
                }
            }

            return true;
        }
    }

    public bool LeftMovable
    {
        get
        {
            foreach (var block in blocks)
            {
                int x = block.width- 1;
                int y = block.height;
                if (x > 9 || y < 0 || y > 19) continue;

                if (x < 0 || manager.blocks[y, x].IsStacked)
                {
                    return false;
                }
            }

            return true;
        }
    }

    public bool RightMovable
    {
        get
        {
            foreach (var block in blocks)
            {
                int x = block.width + 1;
                int y = block.height;

                if (x < 0 || y < 0 || y > 19) continue;

                if (x > 9 || manager.blocks[y, x].IsStacked)
                {
                    return false;
                }
            }
            return true;
        }
    }    

    public bool IsPos
    {
        get
        {
            foreach (var block in blocks)
            {
                int x = block.width;
                int y = block.height;

                if (x < 0 || x > 9 || y < 0 || y > 19) return false;
                if(manager.blocks[y,x].IsStacked) return false;
            }
            return true;
        }
    }
}
