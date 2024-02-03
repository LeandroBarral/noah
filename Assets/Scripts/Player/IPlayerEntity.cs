

namespace LobaApps
{

    public interface IPlayerEntity
    {
        void Attack();
        void Jump();
        void Move();

        PlayerAnimation Animations { get; }
    }
}