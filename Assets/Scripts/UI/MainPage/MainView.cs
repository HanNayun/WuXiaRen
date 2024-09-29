using System;
using Load;
using UnityEngine;

namespace UI.MainPage
{
    public interface IMainViewArgs : IViewArgs
    {
    }

    [AssetPath("Main/MainPage")]
    public class MainView : View<IMainViewArgs>
    {
        public override void Open(IMainViewArgs args)
        {
        }

        public override void Close()
        {
        }
    }
}