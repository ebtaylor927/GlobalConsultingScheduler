using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalConsultingScheduler
{
    internal class DatabaseConfig
    {
        public static string ConnectionString { get; } = "server=127.0.0.1;database=client_schedule;uid=sqlUser;pwd=Passw0rd!;port=3306";
    }
}
