using UnityEngine;

public class Selectable : MonoBehaviour
{
    private bool isSelected = false;

    [SerializeField]
    Material unselected;
    [SerializeField]
    Material selected;

    [SerializeField]
    MeshRenderer meshRenderer;

    void Start()
    {

    }

    void Update()
    {
        if (isSelected)
        {
            meshRenderer.material = selected;
        }
        else
        {
            meshRenderer.material = unselected;
        }
    }


    public void SetIsSelected(bool b)
    {
        isSelected = b;
    }

    public void Select()
    {
        isSelected = true;
    }

    public void Deselect()
    {
        isSelected = false;
    }
}
