using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using System.Text;
using System.Threading.Tasks;
using IronPython.Hosting;


namespace JaneDll
{
    public class DataTransmitter 
    {
        public DataTransmitter(){}

        public string SendData(int mmt_data, out int is_outlier, out int temperature)
        {
            // Initialising python engine -----------------------------------------------
            var engine = Python.CreateEngine();

            // reading python code from file
            var python_source = engine.CreateScriptSourceFromFile("C:\\code\\CSL\\Momentum_data_exchange_server\\data_exchange_server.py");

            var python_scope = engine.CreateScope();
            python_source.Execute(python_scope);

            var classDataExchangeServer = python_scope.GetVariable("DataExchangeServer");
            var dataExchange = engine.Operations.CreateInstance(classDataExchangeServer);
            // --------------------------------------------------------------------------

            var python_data = dataExchange.send_data(mmt_data);

            is_outlier = python_data[0];
            temperature = python_data[1];

            return "ok";
        }



    }
}
