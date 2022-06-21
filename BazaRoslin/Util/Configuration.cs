using dotenv.net;
using dotenv.net.Utilities;

namespace BazaRoslin.Util {
    public static class Configuration {
        static Configuration() {
            DotEnv.Fluent()
                .WithExceptions()
                .WithEnvFiles(".env.example", ".env")
                .Load();
        }

        public static string ConnectionString => EnvReader.GetStringValue("DATABASE");
    }
}