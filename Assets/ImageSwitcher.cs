using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class ImageSwitcher : MonoBehaviour, IPointerClickHandler
{
    public Sprite image1;  // �o�O�A���Ĥ@�i�Ϥ�
    public Sprite image2;  // �o�O�A���ĤG�i�Ϥ�
    public Sprite image3;  // �o�O�A���ĤT�i�Ϥ�

    private Image imgComponent;  // �s��o�Ӫ��� Image component

    private void Awake()
    {
        imgComponent = GetComponent<Image>();
        if (imgComponent == null)
        {
            Debug.LogError("ImageSwitcher �}���ݭn���[�즳 Image component ������W�C");
        }
    }

    // ��o�Ӫ���Q�I���ɡA�o�Ӥ�k�|�Q�I�s
    public void OnPointerClick(PointerEventData eventData)
    {
        if (imgComponent.sprite == image1)
        {
            imgComponent.sprite = image2;
        }
        else if (imgComponent.sprite == image2)
        {
            imgComponent.sprite = image3;
        }
        else if (imgComponent.sprite == image3)
        {
            // ���J "Scene_���x" ����
            SceneManager.LoadScene("Scene_���d�@_���x");
        }
    }
}
