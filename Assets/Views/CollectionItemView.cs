public class CollectionItemView : View
{
    public CollectionItemView Clone()
    {
        var itemView = Instantiate(this, this.transform.parent);
        itemView.gameObject.SetActive(true);
        return itemView;
    }
    
    public void Destroy()
    {
        Destroy(this.gameObject);
    }
}