    using UnityEngine;

    public class MovePlatform : MonoBehaviour
    {
        [SerializeField] Transform point1;
        [SerializeField] Transform point2;
        [SerializeField] float timeArrive = 3f;
        float t = 0;
        bool _reverse = false;
        // Update is called once per frame
        void Update()
        {
            if(!_reverse){
                t += 1/ timeArrive * Time.deltaTime;
                if(t >= 1){
                    _reverse = true;
                }
            }else{
                t -= 1/ timeArrive * Time.deltaTime;
                if(t <= 0){
                    _reverse = false;
                }
            }
            transform.position = Vector3.Lerp(point1.transform.position, point2.transform.position, t);
        }
    }
