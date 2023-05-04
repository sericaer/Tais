using UnityEngine.UI;

public class MainView : View
{
    public Text popNum;
    public Text year;
    public Text month;
    public Text day;

    public MapDialogView mapDialogView;

    public Button nextTurnButton;

    public void OnShowMapDialog()
    {
        var dialog = Instantiate(mapDialogView, mapDialogView.transform.parent);
        dialog.model = model;
        dialog.gameObject.SetActive(true);
    }
}
