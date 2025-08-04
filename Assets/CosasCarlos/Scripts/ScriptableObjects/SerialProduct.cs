
using System;

[Serializable]
public class SerialProduct : InventoryItem<ProductsSO>
{

    public int stackSize;


    public SerialProduct(ProductsSO product) : base(product)
    {
        AddToStack();
    }

    public void AddToStack()
    {
        stackSize++;
    }
    public void RemoveFromStack()
    {
        stackSize--;
    }

    public void AddStack(int stack)
    {
        stackSize += stack;
    }

    public void RemoveStack(int stack)
    {
        if(stack < stackSize)
        {
            stackSize -= stack;
        }
        else
        {
            stackSize = 0;
        }
    }
}

