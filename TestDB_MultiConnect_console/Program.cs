using TestDB_MultiConnect;


string database = "master";
string login = "stud";
string password = "stud";

List<string> hosts = GetIP();



MyDbTestConnect testConnect = new MyDbTestConnect(database , login , password , hosts);
try
{
    string cs = testConnect.GetIsTryAsync().Result;
    Console.WriteLine($"Активная строка; {cs}");
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}



List<string> GetIP() // cлучайные  апи
{
    List<string> hosts = new List<string>();

    for (int i = 0; i < 10; i++)
    {
        Random random = new Random();
        hosts.Add($"192.168.49.{random.Next(1, 256)}");
    }

    hosts.Add("192.168.49.180"); // верный ответ
       return hosts;
}