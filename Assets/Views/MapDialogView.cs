public class MapDialogView : DialogView
{
    public ProvinceItemView provinceItemView;
}

public class DialogView : View
{
    public void OnClose()
    {
        Destroy(this.gameObject);
    }
}