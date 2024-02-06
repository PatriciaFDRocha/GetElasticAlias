namespace ElasticAliases
{
    using System.Text.Json;

    internal class Program
    {
        static void Main(string[] args) => Data.GetCommandToUpdateAliases();
    }

    // Uncomment one at a time and run the app
    public class Actions
    {
        //public Dictionary<string, string>? add { get; set; }

        public Dictionary<string, string>? remove { get; set; }
    }

    public class Data
    {
        public List<Actions>? Actions { get; set; }
        
        public static void GetCommandToUpdateAliases()
        {
            var listOfTenantIds = new HashSet<int>
            {
                10000,
                16000,
            };

            var data = new List<Actions>();

            foreach (var tenantId in listOfTenantIds)
            {
                // update values to the index, version and alias
                var valuesAdd = new Dictionary<string, string>
                {
                    { "index", $"merchants_{tenantId}_v3" },
                    { "alias", $"merchants_{tenantId}" },
                };

                var valuesRemove = new Dictionary<string, string>
                {
                    { "index", $"merchants_{tenantId}_v2" },
                    { "alias", $"merchants_{tenantId}" },
                };

                data.Add(new Actions
                {
                    //add = valuesAdd,
                    remove = valuesRemove
                });
            }

            SerializeToJsonAndWriteFile(data, @"C:\Users\user\Desktop\remove-aliases.json");
        }

        private static void SerializeToJsonAndWriteFile<T>(T data, string filePath)
        {
            var actions = new Dictionary<string, T>
            {
                { "actions", data }
            };

            var json = JsonSerializer.Serialize(actions);

            File.WriteAllText(filePath, json);
        }
    }
}