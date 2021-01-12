using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PrivacyTools
{
    class MacroManager
    {
        public List<Macro> Macros = new List<Macro>();

        public List<Keys> GetAllKeys()
        {
            List<Keys> k = new List<Keys>();
            foreach(Macro m in Macros)
            {
                //k.AddRange(m.Combination);
            }
            return k.Distinct().ToList();
        }

        public Macro GetMacroFromKeys(List<Keys> combination)
        {
            /*foreach(Macro m in Macros)
            {
                if (!combination.Except(m.Combination).Any())//check if a contains all elements of b
                {
                    return m;
                }
            }*/
            return null;
        }

        public void SaveMacros(string fileName)
        {
            System.IO.File.WriteAllText(fileName, Newtonsoft.Json.JsonConvert.SerializeObject(Macros, Newtonsoft.Json.Formatting.Indented));
        }

        public void ImportMacros(string fileName)
        {
            Macros = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Macro>>(System.IO.File.ReadAllText(fileName));
        } 
    }
}
