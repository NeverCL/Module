namespace Module.Application
{
    public class PageRequest
    {
        public int Index { get; set; }
        public int Size { get; set; }

        public PageRequest()
        {
            Index = 1;
            Size = 10;
        }
    }
}
