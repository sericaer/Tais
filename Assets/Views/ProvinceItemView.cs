using System;
using UnityEngine.UI;

public class ProvinceItemView : CollectionItemView
{
    public Text provName;
    public Text popNum;

    public ProvinceDialogView provinceDialogView;

    public void OnShowProvinceDialog()
    {
        var dialog = Instantiate(provinceDialogView, provinceDialogView.transform.parent);
        dialog.model = model;
        dialog.gameObject.SetActive(true);
    }
}
