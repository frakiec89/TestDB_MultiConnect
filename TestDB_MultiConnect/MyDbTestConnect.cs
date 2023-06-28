using Microsoft.Data.SqlClient;

namespace TestDB_MultiConnect
{
    public class MyDbTestConnect
    {

        private List<string> _hosts; // ip
        private string cs = string.Empty; // строка  соединения 

        public MyDbTestConnect(string nameDb,  string login , string password, List<string> hosts)
        {
             _hosts = hosts;
            cs = $"Database={nameDb}; User Id= {login}; password={password};TrustServerCertificate=true; Connect Timeout=2;";



        }

        /// <summary>
        /// соединяет строку  адрес  сервера  и  остальное 
        /// </summary>
        /// <param name="host"></param>
        /// <returns></returns>
        private string GetConnectString (string host)
        {
            return $"server={host};{cs}";
        }

        /// <summary>
        /// Возвращает  строку соединения
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<string> GetIsTryAsync ()
        {
            for (int i = 0; i < _hosts.Count; i++)
            {
                string h = _hosts[i];
                string connect = GetConnectString (h);
                bool status = await CheckDBAsync(connect); 
                if (status) 
                    return connect; // если успешно
            }
            throw new Exception("Сервер не доступен по любым адресам");
        }


        /// <summary>
        /// проверяет  сервер 
        /// </summary>
        /// <param name="connectString"></param>
        /// <returns></returns>
        private  async Task<bool> CheckDBAsync(string connectString)
        {
            using (var connection = new SqlConnection(connectString))
            try
            {
                connection.OpenAsync().Wait(500);
                if(connection.ServerVersion != null)
                return true;
                else
                return false;
            }
            catch 
            {
                return false;
            }
        }


    }
}