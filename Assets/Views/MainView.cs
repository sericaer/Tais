using UnityEngine.UI;

public class MainView : View
{
    public Text popNum;

    public MapDialogView mapDialogView;

    public void OnShowMapDialog()
    {
        var dialog = Instantiate(mapDialogView, mapDialogView.transform.parent);
        dialog.model = model;
        dialog.gameObject.SetActive(true);
    }
}
