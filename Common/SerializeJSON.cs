using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Script.Serialization;

namespace Common
{
    public class SerializeJSON
    {
        /// <summary>
        /// Creates a SerializeJSON by parsing a string.
        /// This is the only correct way to create a SerializeJSON.
        /// </summary>
        public static SerializeJSON CreateFromString(string s)
        {
            object o;
            JavaScriptSerializer js = new JavaScriptSerializer();
            try
            {
                o = js.DeserializeObject(s);
            }
            catch (ArgumentException ex)
            {
                throw new FacebookException("JSONException. Not a valid JSON string.", ex);
            }

            return Create(o);
        }

        /// <summary>
        /// Returns true if this SerializeJSON represents a dictionary.
        /// </summary>
        public bool IsDictionary
        {
            get
            {
                return _dictData != null;
            }
        }

        /// <summary>
        /// Returns true if this SerializeJSON represents an array.
        /// </summary>
        public bool IsArray
        {
            get
            {
                return _arrayData != null;
            }
        }

        /// <summary>
        /// Returns true if this SerializeJSON represents a string value. 
        /// </summary>
        public bool IsString
        {
            get
            {
                return _stringData != null;
            }
        }

        /// <summary>
        /// Returns true if this SerializeJSON represents an integer value.
        /// </summary>
        public bool IsInteger
        {
            get
            {
                Int64 tmp;
                return Int64.TryParse(_stringData, out tmp);
            }
        }

        /// <summary>
        /// Returns true if this SerializeJSON represents a boolean value.
        /// </summary>
        public bool IsBoolean
        {
            get
            {
                bool tmp;
                return bool.TryParse(_stringData, out tmp);
            }
        }

        /// <summary>
        /// Returns this SerializeJSON as a dictionary
        /// </summary>
        public Dictionary<string, SerializeJSON> Dictionary
        {
            get
            {
                return _dictData;
            }
        }

        /// <summary>
        /// Returns this SerializeJSON as an array
        /// </summary>
        public SerializeJSON[] Array
        {
            get
            {
                return _arrayData;
            }
        }

        /// <summary>
        /// Returns this SerializeJSON as a string
        /// </summary>
        public string String
        {
            get
            {
                return _stringData;
            }
        }

        /// <summary>
        /// Returns this SerializeJSON as an integer
        /// </summary>
        public Int64 Integer
        {
            get
            {
                return Convert.ToInt64(_stringData);
            }
        }

        /// <summary>
        /// Returns this SerializeJSON as a boolean
        /// </summary>
        public bool Boolean
        {
            get
            {
                return Convert.ToBoolean(_stringData);
            }
        }


        /// <summary>
        /// Prints the SerializeJSON as a formatted string, suitable for viewing.
        /// </summary>
        public string ToDisplayableString()
        {
            StringBuilder sb = new StringBuilder();
            RecursiveObjectToString(this, sb, 0);
            return sb.ToString();
        }

        #region Private Members

        private string _stringData;
        private SerializeJSON[] _arrayData;
        private Dictionary<string, SerializeJSON> _dictData;

        private SerializeJSON()
        { }

        /// <summary>
        /// Recursively constructs this SerializeJSON 
        /// </summary>
        private static SerializeJSON Create(object o)
        {
            SerializeJSON obj = new SerializeJSON();
            if (o is object[])
            {
                object[] objArray = o as object[];
                obj._arrayData = new SerializeJSON[objArray.Length];
                for (int i = 0; i < obj._arrayData.Length; ++i)
                {
                    obj._arrayData[i] = Create(objArray[i]);
                }
            }
            else if (o is Dictionary<string, object>)
            {
                obj._dictData = new Dictionary<string, SerializeJSON>();
                Dictionary<string, object> dict =
                    o as Dictionary<string, object>;
                foreach (string key in dict.Keys)
                {
                    obj._dictData[key] = Create(dict[key]);
                }
            }
            else if (o != null) // o is a scalar
            {
                obj._stringData = o.ToString();
            }

            return obj;
        }

        private static void RecursiveObjectToString(SerializeJSON obj,
            StringBuilder sb, int level)
        {
            if (obj.IsDictionary)
            {
                sb.AppendLine();
                RecursiveDictionaryToString(obj, sb, level + 1);
            }
            else if (obj.IsArray)
            {
                foreach (SerializeJSON o in obj.Array)
                {
                    RecursiveObjectToString(o, sb, level);
                    sb.AppendLine();
                }
            }
            else // some sort of scalar value
            {
                sb.Append(obj.String);
            }
        }
        private static void RecursiveDictionaryToString(SerializeJSON obj,
            StringBuilder sb, int level)
        {
            foreach (KeyValuePair<string, SerializeJSON> kvp in obj.Dictionary)
            {
                sb.Append(' ', level * 2);
                sb.Append(kvp.Key);
                sb.Append(" => ");
                RecursiveObjectToString(kvp.Value, sb, level);
                sb.AppendLine();
            }
        }

        #endregion
    }
}
