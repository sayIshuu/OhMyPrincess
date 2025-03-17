using System.Collections;
using UnityEngine;

public class SkillDraggable : MonoBehaviour
{
    public GameObject[] skillSpawnArea;

    private Vector2 originalPosition;
    private Camera mainCamera;
    private SkillDropArea currentDropArea;
    private Skill skill;
    private bool isDragging;

    private void Start()
    {
        mainCamera = Camera.main;
        skillSpawnArea = GameObject.FindGameObjectsWithTag("SkillDropArea");
        skill = GetComponent<Skill>();
        isDragging = false;
        originalPosition = transform.position;
        foreach (GameObject area in skillSpawnArea)
        {
            area.SetActive(false);
        }
    }

    private void OnMouseDown()
    {
        if(PrincessManager.Instance.princessStress < skill.mentalCost)
        {
            return;
        }
        isDragging = true;
        
        foreach (GameObject area in skillSpawnArea)
        {
            area.SetActive(true);
        }

        PrincessManager.Instance.StartDragSkill();
    }

    private void OnMouseDrag()
    {
        if (isDragging)
        {
            transform.position = GetMouseWorldPosition();
        }
    }

    private void OnMouseUp()
    {
        isDragging = false;
        if (currentDropArea != null)
        {
            currentDropArea.SkillDrop(skill);
        }

        transform.position = originalPosition;
        PrincessManager.Instance.EndDragSkill(skill.mentalCost);
        StartCoroutine(usedSkill());
    }

    IEnumerator usedSkill()
    {
        yield return null;
        foreach (GameObject area in skillSpawnArea)
        {
            area.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("SkillDropArea"))
        {
            currentDropArea = other.GetComponent<SkillDropArea>();
        }
    }

    /*
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("SkillDropArea"))
        {
            Debug.Log("나감");
            currentDropArea = null;
        }
    }
    */

    private Vector3 GetMouseWorldPosition()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = -mainCamera.transform.position.z; // 카메라 위치 보정
        return mainCamera.ScreenToWorldPoint(mousePosition);
    }
}
