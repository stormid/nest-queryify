namespace Nest.Queryify.Tests.TestData
{
    [ElasticType(IdProperty = "Id")]
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }

    public class PersonModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }
}