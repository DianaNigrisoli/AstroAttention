using UnityEngine;

namespace Assets.ScriptsDirectionGame
{
    public class laserButtons : MonoBehaviour
    {
        public GameObject button;
        private bool selected;
        
    
        // Start is called before the first frame update
        void Start()
        {
            button = this.gameObject;
            this.selected = false;
            Debug.Log(this.name);
        }

        // Update is called once per frame
        void Update()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Input.GetMouseButtonDown(0) && !selected){
                if(Physics.Raycast(ray,out hit) && hit.collider.gameObject == gameObject)
                {
                    switch(this.name)
                    {
                        case "UpperRightButtonQuad":
                            CannonBehavior.upRightShot = true;
                            break;
                        case "UpperLeftButtonQuad":
                            CannonBehavior.upLeftShot = true;
                            break;
                        case "LowerRightButtonQuad":
                            CannonBehavior.downRightShot = true;
                            break;
                        case "LowerLeftButtonQuad":
                            CannonBehavior.downLeftShot = true;
                            break;
                    }
                }
            }
        }
    }
}
