namespace Coders_Zone.Models
{
    public class ErrorViewModel
    {
        public string? RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
    public class TabModel
    {
        public string Title { get; set; }
        public string Content { get; set; }
        //public string FrameContent { get; set; }
    }
    //public class DropdownModel
    //{
    //    public string Name { get; set; }
    //    public List<DropdownItemModel> SubItems { get; set; }
    //}

    //public class DropdownItemModel
    //{
    //    public string Value { get; set; }
    //    public string Text { get; set; }
    //}
}
