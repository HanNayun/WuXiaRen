using UnityEngine;

namespace UI
{
    public interface IViewArgs
    {
    }

    [DisallowMultipleComponent]
    public abstract class View<TArgs> : MonoBehaviour
        where TArgs : IViewArgs
    {
        public UILayers Layer { get; private set; } = UILayers.Basic;
        public abstract void Open(TArgs args);
        public abstract void Close();
    }
}