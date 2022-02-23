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
        private const string pyScriptPath = 
            "C:\\code\\jane_command_centre\\momentum_interface\\data_exchange_client.py";

        public DataTransmitter(){}

        public string sendMomentumState(string mmt_state)
        {
            // Initialising python engine -----------------------------------------------
            var engine = Python.CreateEngine();

            // reading python code from file
            //var python_source = engine.CreateScriptSourceFromFile("C:\\code\\Momentum_data_exchange_server\\data_exchange_server.py");
            var python_source = engine.CreateScriptSourceFromFile(pyScriptPath);

            var python_scope = engine.CreateScope();
            python_source.Execute(python_scope);

            var classDataExchangeClient = python_scope.GetVariable("DataExchangeClient");
            var dataExchange = engine.Operations.CreateInstance(classDataExchangeClient);
            // --------------------------------------------------------------------------

            var python_data = dataExchange.update_mmt_state(mmt_state);
            
            return "ok";
        }

        public string getJaneRobotState(out string JaneRobotState)
        {
            // Initialising python engine -----------------------------------------------
            var engine = Python.CreateEngine();

            // reading python code from file
            //var python_source = engine.CreateScriptSourceFromFile("C:\\code\\Momentum_data_exchange_server\\data_exchange_server.py");
            var python_source = engine.CreateScriptSourceFromFile(pyScriptPath);

            var python_scope = engine.CreateScope();
            python_source.Execute(python_scope);

            var classDataExchangeClient = python_scope.GetVariable("DataExchangeClient");
            var dataExchange = engine.Operations.CreateInstance(classDataExchangeClient);
            // --------------------------------------------------------------------------

            JaneRobotState = dataExchange.get_jane_robot_state();

            return "ok";
        }

    }
}
