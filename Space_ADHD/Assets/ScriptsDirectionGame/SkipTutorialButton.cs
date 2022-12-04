using Assets.Scripts_A_General;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.ScriptsDirectionGame
{
    public class SkipTutorialButton : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            this.GetComponent<Button>().onClick.AddListener(TaskOnClick);
        }

        private void TaskOnClick()
        {
            MiniGameManager.instance.UpdateMiniGameState(MiniGameState.WaitForNext);
            var canvas = GameObject.Find("CanvasIntro");
            var textObject = canvas.transform.GetChild(3).GetComponent<TextMeshProUGUI>();
            textObject.text = "Select the sector with the comet";
            var IntroManager = GameObject.Find("IntroManager");
            Destroy(IntroManager);
            for (int i = 0; i < 2; i++)
            {
                canvas.transform.GetChild(i).gameObject.SetActive(false);
            }
            canvas.transform.GetChild(4).gameObject.SetActive(false);
            
            Destroy(GameObject.Find("Diver(Clone)"));

            GameObject[] ringTarget;
            ringTarget = GameObject.FindGameObjectsWithTag("RingTutorial");
            GameObject.Find("shootingStar").transform.position += Vector3.right * 30;
            GameObject.Find("notShootingStar").transform.position += Vector3.right * 30; 
            

            foreach (var ring in ringTarget)
            {
                Destroy(ring);
            }
            gameObject.SetActive(false);
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
