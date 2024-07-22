using UnityEngine;

namespace Views
{
    public class ViewManager : MonoBehaviour
    {
        private ViewManager s_instance;


        private void Awake()
        {
            s_instance = this;
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