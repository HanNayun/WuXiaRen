using UnityEngine;

namespace Views
{
    public class ViewManager : MonoBehaviour
    {
        private ViewManager _sInstance;


        private void Awake()
        {
            _sInstance = this;
        }

        public void OpenView<T>() where T : DocumentView
        {
            // 1. laod view res
            // 2. instantiate view
            // 3. add view to view stack
            // 4. store view
        }
    }
}