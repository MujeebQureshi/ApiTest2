using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestAPI.Models
{
    public static class AppConstants
    {
        #region Key

        public const string AppSecretKeyFormsAuth = "mCUHr0ZYTaQvCPiXocWnSqFtXFA";
        public const string AppSecretKeyPassword  = "mCUHr0ZYTaQvCPiXocWnSqFtXP";

        #endregion

        #region DDLs

        private class DDList
        {
            public DDList(string Text, object Value)
            {
                this.Text = Text;
                this.Value = Value;
            }
            public string Text { get; set; }
            public object Value { get; set; }
            public bool isSelected { get; set; }
        }
        
        
        internal static object getYN()
        {
            return new List<DDList>()
            { { new DDList("--Select an option--", "") },
                { new DDList("Yes", "Y") },
                { new DDList("No", "N") }  
            };
        }

        
        #endregion

    }
}
