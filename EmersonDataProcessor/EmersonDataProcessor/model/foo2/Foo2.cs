namespace EmersonDataProcessor.model.foo2
{
    public class Foo2 : IFoo
    {
        public Foo2()
        {
        }
        public int CompanyId { get; set; }
        public string Company { get; set; }
        public Device[] Devices { get; set; }
    }
}
