using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts
{
    public class planetButton : MonoBehaviour
    {
        public UnityEvent unityEvent = new UnityEvent();
        public GameObject button;
        private int rotationY = 13;
    
        // Start is called before the first frame update
        void Start()
        {
            button = this.gameObject;

        }

        // Update is called once per frame
        void Update()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Input.GetMouseButtonDown(0)){
                if(Physics.Raycast(ray,out hit) && hit.collider.gameObject == gameObject)
                {
                    buttonAction.selectedPlanet = gameObject.name;
                    unityEvent.Invoke();
                }
            }
            transform.Rotate(0, rotationY*Time.deltaTime, 0); //rotates rotationY degrees per second around y axis
        }
    }
}
