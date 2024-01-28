
namespace LobaApps.Architecture.State
{
    using System;

    public interface IStateCreator<EType, TType>
        where EType : Enum
    {
        TType Factory(EType type);
    }
}