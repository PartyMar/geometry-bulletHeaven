using UnityEngine;


namespace GBH
{

    public class CameraSingleton : MonoBehaviour
    {

        public static CameraSingleton instance;

        void Start()
        {
            if (instance == null)
            {
                instance = this;
            }
        }


    }
}
