namespace NetCore.Common.Base
{
    public class SelectItem<T>
    {
        public T ItemValue { get; private set; }

        public string DisplayName { get; private set; }

        public SelectItem(T ItemValue, string displayName)
        {
            this.ItemValue = ItemValue;
            this.DisplayName = displayName;
        }

        public static readonly SelectItem<int?> Blank = new SelectItem<int?>(null, string.Empty);
    }
}
