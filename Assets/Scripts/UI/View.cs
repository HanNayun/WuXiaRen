using UnityEngine;

namespace UI
{
    public interface IViewArgs
    {
    }

    public abstract class View<TArgs> where TArgs : IViewArgs
    {
        public View()
        {
        }

        public void Open()
        {
        }
    }
}