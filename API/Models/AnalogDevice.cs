using Microsoft.Extensions.Configuration;
using MySqlConnector;
using smartHome.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace smartHome.Models
{
    public class AnalogDevice
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AnalogDeviceId { get; set; }
        public int Value { get; set; }
        public int DeviceId { get; set; }
        [IgnoreDataMember]
        public Device Device { get; set; }
    }

    public class AnalogDeviceRepository
    {
        private AppDbContext AppDbContext;

        private IConfiguration Configuration { get; }

        private DataBaseService dataBaseService;

        public AnalogDeviceRepository(AppDbContext appDbContext, IConfiguration Configuration, DataBaseService dataBaseService)
        {
            this.AppDbContext = appDbContext;
            this.Configuration = Configuration;
            this.dataBaseService = dataBaseService;
        }


        public void ChangeAnalogDeviceValue(int Id, int Value)
        {
            string commandText = "call changeAnalogDeviceValue(@id, @value)";
            LinkedList<MySqlParameter> paramList = new LinkedList<MySqlParameter>();
            paramList.AddLast(new MySqlParameter("@id", Id));
            paramList.AddLast(new MySqlParameter("@value", Value));
            dataBaseService.ExecuteNonQuery(commandText, paramList);
        }

        //public void changeAnalogDeviceValue(int Id, int Value)
        //{
        //    using (MySqlConnection mycon = new MySqlConnection(Configuration.GetConnectionString("DefaultConnectionString")))
        //    {
        //        mycon.Open();
        //        MySqlCommand command = mycon.CreateCommand();
        //        command.CommandText = "call changeAnalogDeviceValue(@id, @value)";
        //        command.Parameters.AddWithValue("@id", Id);
        //        command.Parameters.AddWithValue("@value", Value);
        //        command.ExecuteNonQuery();
        //        mycon.Close();
        //    }
        //}
    }
}
