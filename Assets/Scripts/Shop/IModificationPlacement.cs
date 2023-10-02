using System;

public interface IModificationPlacement
{
    event Action<Type, int> Upgrading;
}
