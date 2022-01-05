using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP_Link_Router_Auto_Restart
{
    internal static class RouterElements
    {
        public static List<string> Links = new List<string>()
        {
            "192.168.0.1",
            "192.168.1.1",
            "192.168.1.0",
            "192.168.10.1",
            "192.168.10.0",
            "192.168.0.10",
            "192.168.0.2", 
            "192.168.0.0"
        };
        public static List<string> ElementsLoginField = new List<string>()
        {
            "/html/body/div[2]/div[2]/div/ul/li[1]/input"
        };
        public static List<string> ElementsPasswordField = new List<string>()
        {
            "/html/body/div[2]/div[2]/div/ul/li[3]/input",
            "/html/body/div[3]/div[2]/div/ul/li/input"
        };
        public static List<string> ElementsEnterButton = new List<string>()
        {
            "/html/body/div[2]/div[2]/div/label",
            "/html/body/div/div[1]/ul/li[17]",
            "/html/body/div[3]/div[2]/div/label",
            "/html/body/div[3]/div[2]/div/label/span"
        };
        public static List<string> ElementsSettingsField = new List<string>()
        {
            "/html/body/menu/ol[45]",
            "/html/body/menu/ol[51]/a",
            "/html/body/div/div[1]/ul/li[17]/ul/li[7]/a/span",
            "/html/body/div/div[1]/ul/li[17]/a/span",
            "/html/body/div/div[1]/ul/li[17]"
        };
        public static List<string> ElementsRestartField = new List<string>()
        {
             "/html/body/menu/ol[51]/a",
             "/html/body/div[1]/div/div/div[1]/p[3]/input",
             "/html/body/div/div[1]/ul/li[17]/ul/li[7]/a/span"
        };
        public static List<string> ElementsRestartButton = new List<string>()
        {
            "/html/body/center/form/table/tbody/tr[4]/td/input",
            "/html/body/div[1]/div/div/div[1]/p[3]/input"
        };
    }
}
