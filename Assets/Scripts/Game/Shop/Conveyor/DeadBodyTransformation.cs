using System;

public class DeadBodyTransformation : StackableTransformation
{
    public override StackableType Type => StackableType.DeadBody;

    public override Stackable Transform(Stackable removedItem)
    {
        if (removedItem is Coffin coffin)
            return coffin.UrnTemplate;
        else
            throw new InvalidOperationException("Invalid parameter");
    }
}