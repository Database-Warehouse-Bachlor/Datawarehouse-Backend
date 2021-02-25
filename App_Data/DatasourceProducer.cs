using Npgsql;

namespace Datawarehouse_Backend.App_Data {
    class DatasourceProducer {
        private static NpgsqlConnection getConnection() {
            return new NpgsqlConnection(@"Server=localhost;Port=5432;User Id=test;Password=test;Database=logindb");
        }
    }
}