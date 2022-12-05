using Assets.Scripts_A_General;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

namespace Assets.Scripts
{
    public class planetButton : MonoBehaviour
    {
        [SerializeField] private GameObject childRing;
        private Vector3 childRingScale;
        
        public UnityEvent unityEvent = new UnityEvent();
        public GameObject button;
        private int rotationY = 13;
        private int rotationZ = 9;
        private bool selected;
        private float suggestionDelay = 8.5f;
        
        private GameState gameState;
    
        void Awake()
        {
            GameManager.OnGameStateChanged += GameManagerOnOnGameStateChanged;
        }

        void OnDestroy()
        {
            GameManager.OnGameStateChanged -= GameManagerOnOnGameStateChanged;
        }

        private void GameManagerOnOnGameStateChanged(GameState state)
        {
            gameState = state;
            if (state == GameState.Map)
            {
                suggestionDelay = 8.5f;
            }
        }
        
    
        // Start is called before the first frame update
        void Start()
        {
            button = this.gameObject;
            this.selected = false;
            childRingScale = childRing.transform.localScale;
            childRing.SetActive(false);
        }

        // Update is called once per frame
        void Update()
        {
            transform.Rotate(0, rotationY*Time.deltaTime, rotationZ*Time.deltaTime); //rotates rotationY degrees per second around y axis
            if (gameState != GameState.Map) return;
            suggestionDelay -= Time.deltaTime;
            if (suggestionDelay <= 0 && !selected)
            {
                childRing.SetActive(true);
                childRing.transform.Rotate(0, 0, -rotationZ * Time.deltaTime * 3.1f);
                childRing.transform.localScale = childRingScale + new Vector3(Mathf.Sin(Time.time * 5.3f) / 10f,
                    Mathf.Sin(Time.time * 5.3f) / 10f, 0.0f);
            }

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Input.GetMouseButtonDown(0) && !selected){
                if(Physics.Raycast(ray,out hit) && hit.collider.gameObject == gameObject)
                {
                    buttonAction.selectedPlanet = gameObject.name;
                    selected = true;
                    MenuSuggestionController.planetSelected = selected;
                    childRing.SetActive(false);
                    unityEvent.Invoke();
                }
            }
        }
    }
}
