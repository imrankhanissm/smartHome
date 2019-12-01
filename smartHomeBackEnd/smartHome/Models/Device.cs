using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using smartHome.Services;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace smartHome.Models
{
    public class Device
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DeviceId { get; set; }
        public string DeviceName { get; set; }
        public bool RelaySwitch { get; set; }
        [IgnoreDataMember]
        public AnalogDevice AnalogDevice { get; set;}
    }

    public class DeviceRepository
    {
        private AppDbContext AppDbContext;

        private IConfiguration Configuration { get; }

        private DataBaseService dataBaseService;

        public DeviceRepository(AppDbContext appDbContext, IConfiguration Configuration, DataBaseService dataBaseService)
        {
            this.AppDbContext = appDbContext;
            this.Configuration = Configuration;
            this.dataBaseService = dataBaseService;
        }

        public void Switch(int Id)
        {
            LinkedList<MySqlParameter> paramList = new LinkedList<MySqlParameter>();
            string commandText = "call switch(@id)";
            paramList.AddLast(new MySqlParameter("@id", Id));
            dataBaseService.ExecuteNonQuery(commandText, paramList);
        }

        //public void Switch(int Id)
        //{
        //    using (MySqlConnection mycon = new MySqlConnection(Configuration.GetConnectionString("DefaultConnectionString")))
        //    {
        //        mycon.Open();
        //        MySqlCommand command = mycon.CreateCommand();
        //        command.CommandText = "call switch(@id)";
        //        command.Parameters.AddWithValue("@id", Id);
        //        command.ExecuteNonQuery();
        //        mycon.Close();
        //    }
        //}
    }
}
