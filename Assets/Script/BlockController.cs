using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockController : Observer
{
    public Block currentBlock;

    float time = 0;
    private void Awake()
    {
        EventBus.Subscribe(EventType.BlockStack, Cancel);
    }
    public override void Notify(Subject subject)
    {
        BlockManager blockManager = subject.GetComponent<BlockManager>();
        currentBlock = blockManager.CurBlock;
    }

    void Update()
    {
        if (currentBlock == null) return;

        if (Input.GetKeyDown(KeyCode.Z))
        {
            Rotate();           
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow) && currentBlock.LeftMovable)
        {
            MoveLeft();
        }
        if (Input.GetKeyDown(KeyCode.RightArrow) && currentBlock.RightMovable)
        {
            MoveRight();
        }
        time += Time.deltaTime;

        if (Input.GetKey(KeyCode.DownArrow))
        {            
            if (time > 0.05f)
            {
                time = 0;
                if (currentBlock.DownMovable)
                {
                    MoveDown();
                }
                else
                {
                    EventBus.Publish(EventType.BlockStack);
                }
            }
        }
        else
        {
            if (time > 1)
            {
                time = 0;
                if (currentBlock.DownMovable)
                {
                    MoveDown();
                }
                else
                {
                    EventBus.Publish(EventType.BlockStack);
                }
            }
        }
    }
    void MoveDown()
    {
        currentBlock.transform.Translate(new Vector2(0, -1), Space.World);
        EventBus.Publish(EventType.AfterMove);
    }

    void MoveLeft()
    {
        currentBlock.transform.Translate(new Vector2(-1, 0), Space.World);
        EventBus.Publish(EventType.AfterMove);
    }
    void MoveRight()
    {
        currentBlock.transform.Translate(new Vector2(1, 0), Space.World);
        EventBus.Publish(EventType.AfterMove);
    }

    void Rotate()
    {
        
        currentBlock.transform.Rotate(new Vector3(0, 0, 90));
        EventBus.Publish(EventType.AfterMove);

        if (!currentBlock.IsPos)
        {
            currentBlock.transform.Rotate(new Vector3(0, 0, -90));
            EventBus.Publish(EventType.AfterMove);

        }       
    }
    void Cancel()
    {
        currentBlock = null;
    }
}
