namespace Attestation
{
    class Zona
    {
        public string Name { set; get; }
        public int Id { set; get; }
        public Zona(int id, string name)
        {
            this.Id = id;
            this.Name = name;
        }
    }
}
