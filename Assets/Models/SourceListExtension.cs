using DynamicData;
using System;

public static class SourceListExtension
{
    public static void OnChange<T>(this SourceList<T> sourceList, IChangeSet<T> changeSet)
    {
        foreach (var changed in changeSet)
        {
            switch(changed.Reason)
            {
                case ListChangeReason.Add:
                    sourceList.Add(changed.Item.Current);
                    break;
                case ListChangeReason.Remove:
                    sourceList.Remove(changed.Item.Current);
                    break;
                case ListChangeReason.AddRange:
                    sourceList.AddRange(changed.Range);
                    break;
                case ListChangeReason.RemoveRange:
                    sourceList.RemoveMany(changed.Range);
                    break;
                default:
                    throw new Exception();
            }
        }
    }
}
