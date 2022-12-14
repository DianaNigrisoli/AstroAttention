using Assets.Scripts_A_General;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Assets.ScriptsDirectionGame;
    
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
            var textObject = canvas.transform.GetChild(4).GetComponent<TextMeshProUGUI>();
            textObject.text = GameManager.instance.Language == "ITA"? "Seleziona il settore con la cometa" : "Select the sector with the comet";
            var IntroManager = GameObject.Find("IntroManager");
            Destroy(IntroManager);
            for (int i = 1; i < 3; i++)
            {
                canvas.transform.GetChild(i).gameObject.SetActive(false);
            }
            canvas.transform.GetChild(5).gameObject.SetActive(false);
            
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
            //musicManager.sound.Stop();
        }
    }
}
