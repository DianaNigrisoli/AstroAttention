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
        private int rotationZ = 9;
        private bool selected;
        
    
        // Start is called before the first frame update
        void Start()
        {
            button = this.gameObject;
            this.selected = false;
        }

        // Update is called once per frame
        void Update()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Input.GetMouseButtonDown(0) && !selected){
                if(Physics.Raycast(ray,out hit) && hit.collider.gameObject == gameObject)
                {
                    buttonAction.selectedPlanet = gameObject.name;
                    selected = true;
                    unityEvent.Invoke();
                }
            }
            transform.Rotate(0, rotationY*Time.deltaTime, rotationZ*Time.deltaTime); //rotates rotationY degrees per second around y axis
        }
    }
}
