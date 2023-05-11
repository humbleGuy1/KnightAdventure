using UnityEngine;

namespace CodeBase.Services.Input
{
    public class TouchInputService : InputService
    {
        public override Vector2 Axis => SimpleInputAxis();
    }
}
