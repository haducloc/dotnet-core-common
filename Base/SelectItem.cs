using NetCore.Common.Utils;

namespace NetCore.Common.Base
{
    public class SelectItem
    {
        public object ItemValue { get; set; }

        public string DisplayName { get; set; }

        public SelectItem()
        {
        }

        public SelectItem(object ItemValue, string displayName)
        {
            this.ItemValue = ItemValue;
            this.DisplayName = displayName;
        }

        public int IntValue
        {
            get
            {
                AssertUtils.AssertNotNull(ItemValue);
                return (int)ItemValue;
            }
        }

        public string StringValue
        {
            get
            {
                AssertUtils.AssertNotNull(ItemValue);
                return (string)ItemValue;
            }
        }

        public static readonly SelectItem Blank = new SelectItem(null, string.Empty);
    }
}
