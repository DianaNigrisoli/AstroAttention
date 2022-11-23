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
                            if (phase0Manager.cases == 2 || phase1Manager.cases == 1){
                                CannonBehavior.upRightShot = true;
                                phase0Manager.touch = true;}
                            break;
                        case "UpperLeftButtonQuad":
                            if (phase0Manager.cases==0 || phase1Manager.cases == 3){
                            CannonBehavior.upLeftShot = true;
                            phase0Manager.touch = true;}
                            break;
                        case "LowerRightButtonQuad":
                            if (phase0Manager.cases == 3 || phase1Manager.cases == 0){
                                CannonBehavior.downRightShot = true;
                                phase0Manager.touch = true;}
                            break;
                        case "LowerLeftButtonQuad":
                            if (phase0Manager.cases == 1 || phase1Manager.cases == 2){
                                CannonBehavior.downLeftShot = true;
                                phase0Manager.touch = true;}
                            break;
                        default:
                            break;
                    }
                }
            }
        }
    }
}
