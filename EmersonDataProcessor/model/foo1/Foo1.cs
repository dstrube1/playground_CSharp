namespace EmersonDataProcessor.model.foo1
{
    public class Foo1 : IFoo
    {
        public Foo1()
        {
        }
        public int PartnerId { get; set; }
        public string PartnerName { get; set; }
        public Tracker[] Trackers { get; set; }
    }
}
