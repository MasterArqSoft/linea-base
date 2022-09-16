using microservice.dll.conection.Entities;
using microservice.dll.conection.Setting;
using microservice.domain.Exceptions;
using Newtonsoft.Json;
using System.Text;

namespace microservice.dll.conection.Extensions;

public static class Connection
{
    public static string GetPartnerConnection(int id)
    {

        string user, password, database, server = string.Empty;

        var conn = new PartnerManagementContext();
        var partner = conn.Partners.Where(x => x.PartnerId == id).ToList();
        user = partner.Select(n => n.UserBd).ToList()[0].ToString();
        password = partner.Select(n => n.PasswordBd).ToList()[0].ToString();
        database = partner.Select(n => n.PartnerDatabase).ToList()[0].ToString();
        server = partner.Select(n => n.ServerBd).ToList()[0].ToString();
        string stringConn = "server=" + server + ";" + "database=" + database + ";" + "Trusted_Connection=false; MultipleActiveResultSets=true;" + "User Id= " + user + ";" + "Password=" + password;
        return stringConn;
    }

    public static string GetConnection()
    {
        StreamReader r = new(@"C:\Microservices\connection.json");
        string jsonString = r.ReadToEnd();
        ConnectionModel connectioModel = JsonConvert.DeserializeObject<ConnectionModel>(jsonString)!;

        if (connectioModel == null) throw new ConectionException(
                nameof(connectioModel),
                $"El parametro connectioModel no puede ser null");

        string user = Decrypt(connectioModel.User!);
        string password = Decrypt(connectioModel.Password!);
        string database = Decrypt(connectioModel.Database!);
        string server = Decrypt(connectioModel.Server!);

        string cadenaGeneral = "server=" + server + ";" + "database=" + database + ";" + "Trusted_Connection=false; MultipleActiveResultSets=true;" + "User Id= " + user + ";" + "Password=" + password;

        return cadenaGeneral;
    }

    private static string Decrypt(string stringDecrypt)
    {
        try
        {
            byte[] decryted = Convert.FromBase64String(stringDecrypt);
            string result = Encoding.UTF8.GetString(decryted);
            return result;
        }
        catch (Exception)
        {
            throw;
        }
    }

}

