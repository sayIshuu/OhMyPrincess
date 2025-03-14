using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UiAnimationManager : MonoBehaviour
{
    //싱글톤 패턴
    public static UiAnimationManager Instance;

    [SerializeField] private float duration = 0.1f;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayAnimation(Image image, Sprite[] sprites)
    {
        StartCoroutine(PlayAnimationCoroutine(image, sprites));
    }

    private IEnumerator PlayAnimationCoroutine(Image image, Sprite[] sprites)
    {
        for (int i = 0; i < sprites.Length; i++)
        {
            image.sprite = sprites[i];
            yield return new WaitForSeconds(duration);
        }
        //idle 상태로 돌아가기
        image.sprite = sprites[0];
    }
}
