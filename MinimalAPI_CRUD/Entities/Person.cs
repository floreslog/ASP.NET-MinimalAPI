namespace MinimalAPI_CRUD.Entities
{
    public class Person
    {
        public int IDPerson { get; set; }
        public required string Name { get; set; }
        public required string Last_name { get; set; }
        public required string CURP { get; set; }
    }
}
